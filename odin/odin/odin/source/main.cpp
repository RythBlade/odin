// dear imgui: standalone example application for SDL2 + DirectX 11
// If you are new to dear imgui, see examples/README.txt and documentation at the top of imgui.cpp.
// (SDL is a cross-platform general purpose library for handling windows, inputs, OpenGL/Vulkan/Metal graphics context creation, etc.)

#include "imgui.h"
#include "imgui_impl_sdl.h"
#include "imgui_impl_dx11.h"
#include <d3d11.h>
#include <stdio.h>

#include "Application.h"

odin::Application g_application;

// Main code
int main(int, char**)
{
    g_application.initialise("");
    g_application.run();
    g_application.shutdown();
    return 0;
}

// Helper functions




