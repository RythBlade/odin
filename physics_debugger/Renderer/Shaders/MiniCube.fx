struct VS_IN
{
    float4 pos : POSITION;
    float4 col : NORMAL;
};

struct PS_IN
{
    float4 pos : SV_POSITION;
    float4 col : NORMAL;
};

struct PS_OUT
{
    float4 backBufferColour : SV_Target0;
    uint userDataValue : SV_Target1;
};

cbuffer PerRenderConstantBuffer : register( b0 )
{
    matrix ViewProjection;
}

cbuffer PerObjectConstantBuffer : register( b1 )
{
    matrix World;

    uint UserDataValue;
    float padding1;
    float padding2;
    float padding3;

    float4 ColourTint;
}

PS_IN VS( VS_IN input )
{
    PS_IN output = ( PS_IN ) 0;

    output.pos = mul( input.pos, mul( World, ViewProjection ) );
    output.col = input.col;

    return output;
}

PS_OUT PS(PS_IN input)
{
    PS_OUT output;
    output.backBufferColour = ColourTint + input.col;

    output.userDataValue = UserDataValue;

    return output;
}