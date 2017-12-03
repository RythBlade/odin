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

cbuffer PerRenderConstantBuffer : register( b0 )
{
    matrix ViewProjection;
}

cbuffer PerObjectConstantBuffer : register( b1 )
{
    matrix World;
}

PS_IN VS( VS_IN input )
{
    PS_IN output = ( PS_IN ) 0;

    output.pos = mul( input.pos, mul( World, ViewProjection ) );
    output.col = input.col;

    return output;
}

float4 PS( PS_IN input ) : SV_Target
{
    return input.col;
}