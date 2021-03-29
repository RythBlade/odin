#ifndef WORLD_VIEWPORT_H
#define WORLD_VIEWPORT_H

// forward declarations
struct ID3D11Device;
struct ID3D11ShaderResourceView;

namespace odin
{
    class WorldViewport
    {
    public:
        WorldViewport();
        ~WorldViewport();

        void init(ID3D11Device* device);

    public:
        ID3D11ShaderResourceView* m_viewportTextureView = nullptr;
    };
}

#endif