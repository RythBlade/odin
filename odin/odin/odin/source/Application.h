#ifndef APPLICATION_H
#define APPLICATION_H

#include <string>

#include <SDL.h>
#include <SDL_syswm.h>

#include "world_viewport.h"

struct ID3D11Device;
struct ID3D11DeviceContext;
struct IDXGISwapChain;
struct ID3D11RenderTargetView;

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
        ID3D11Device* m_device = nullptr;
        ID3D11DeviceContext* m_deviceContext = nullptr;
        IDXGISwapChain* m_swapChain = nullptr;
        ID3D11RenderTargetView* m_mainRenderTargetView = nullptr;

        SDL_Window* m_window = nullptr;
        WorldViewport m_viewport;
    };
}

#endif