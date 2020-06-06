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
    uint objectId : SV_Target1;
};

cbuffer PerRenderConstantBuffer : register( b0 )
{
    matrix ViewProjection;

    uint SelectedId;
    uint perRender_padding1;
    uint perRender_padding2;
    uint perRender_padding3;
}

cbuffer PerObjectConstantBuffer : register( b1 )
{
    matrix World;

    uint objectId;
    float padding1;
    float padding2;
    float padding3;
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
    float4 colourModifier = SelectedId == objectId ? float4(0.5f, 0.5f, 0.5f, 1.0f) : input.col;

    PS_OUT output;
    output.backBufferColour = colourModifier * colourModifier;

    output.objectId = objectId;

    return output;
}