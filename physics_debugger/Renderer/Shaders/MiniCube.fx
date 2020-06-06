struct VS_IN
{
    float4 pos : POSITION;
    float4 normal : NORMAL;
};

struct PS_IN
{
    float4 pos : SV_POSITION;
    float4 normal : NORMAL;
    float4 worldPos : TEXCOORD0;
};

struct PS_OUT
{
    float4 backBufferColour : SV_Target0;
    uint userDataValue : SV_Target1;
};

cbuffer PerRenderConstantBuffer : register( b0 )
{
    matrix ViewProjection;

    float4 ViewPosition;

    float4 LightDirection;

    float4 LightColour;

    float AmbientLightStrength;
    float SpecularLightStrength;
    float perrender_padding2;
    float perrender_padding3;
}

cbuffer PerObjectConstantBuffer : register( b1 )
{
    matrix World;

    uint UserDataValue;
    float perobject_padding1;
    float perobject_padding2;
    float perobject_padding3;

    float4 ColourTint;
}

PS_IN VS( VS_IN input )
{
    PS_IN output = ( PS_IN ) 0;

    output.worldPos = mul(input.pos, World);
    output.pos = mul(output.worldPos, ViewProjection );
    
    output.normal = mul(input.normal, World);

    return output;
}

PS_OUT PS(PS_IN input)
{
    float specularShininess = 1;

    ////////////////////////////////////////////////////////////
    // Ambient Lights
    ////////////////////////////////////////////////////////////
    float4 ambientLighting = AmbientLightStrength * LightColour;

    ////////////////////////////////////////////////////////////
    // Diffuse Lighting
    ////////////////////////////////////////////////////////////
    float4 diffuseLighting = saturate(dot(input.normal, -LightDirection))* LightColour;

    ////////////////////////////////////////////////////////////
    // Specular Lighting
    ////////////////////////////////////////////////////////////
    float4 viewDirection = normalize(ViewPosition - input.worldPos);
    float4 reflectedDirection = reflect(LightDirection, input.normal);
    float specularFactor = pow(saturate(dot(viewDirection, reflectedDirection)), specularShininess);
    float specular = specularFactor* SpecularLightStrength* LightColour;

    ////////////////////////////////////////////////////////////
    // Final lighting
    ////////////////////////////////////////////////////////////
    float4 finalLighting = (ambientLighting + diffuseLighting + specular) * ColourTint;

    ////////////////////////////////////////////////////////////
    // Write to back buffers
    ////////////////////////////////////////////////////////////
    PS_OUT output;
    output.backBufferColour = finalLighting;

    output.userDataValue = UserDataValue;

    return output;
}