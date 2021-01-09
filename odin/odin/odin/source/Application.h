#ifndef APPLICATION_H
#define APPLICATION_H

#include <string>

#include <SDL.h>
#include <SDL_syswm.h>

class ID3D11Device;
class ID3D11DeviceContext;
class IDXGISwapChain;
class ID3D11RenderTargetView;

namespace odin
{
    class Application
    {
    public:
        Application();
        ~Application();

        bool initialise(std::string const& windowTitle);

        void run();

        void shutdown();

    private:
        bool CreateDeviceD3D(HWND hWnd);
        void CleanupDeviceD3D();
        void CreateRenderTarget();
        void CleanupRenderTarget();

    private:
        // Data
        ID3D11Device* g_pd3dDevice = nullptr;
        ID3D11DeviceContext* g_pd3dDeviceContext = nullptr;
        IDXGISwapChain* g_pSwapChain = nullptr;
        ID3D11RenderTargetView* g_mainRenderTargetView = nullptr;

        SDL_Window* window = nullptr;
    };
}

#endif