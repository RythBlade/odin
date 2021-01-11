// dear imgui: standalone example application for SDL2 + DirectX 11
// If you are new to dear imgui, see examples/README.txt and documentation at the top of imgui.cpp.
// (SDL is a cross-platform general purpose library for handling windows, inputs, OpenGL/Vulkan/Metal graphics context creation, etc.)

#include "imgui.h"
#include "imgui_impl_sdl.h"
#include "imgui_impl_dx11.h"
#include <d3d11.h>
#include <stdio.h>

#include "Application.h"

#include "core/memory/allocator.h"

odin::Application g_application;

// Main code
int main(int, char**)
{
    odin::core::Allocator allocator;
    
    void* someMemory0 = allocator.allocate(10, __FILE__, __LINE__);
    void* someMemory1 = allocator.allocate(55, __FILE__, __LINE__);
    void* someMemory2 = allocator.allocate(22, __FILE__, __LINE__);
    void* someMemory3 = allocator.allocate(67, __FILE__, __LINE__);
    void* someMemory4 = allocator.allocate(99, __FILE__, __LINE__);
    
    allocator.release(someMemory0);
    allocator.release(someMemory1);
    allocator.release(someMemory2);
    allocator.release(someMemory3);
    allocator.release(someMemory4);

    g_application.initialise("");
    g_application.run();
    g_application.shutdown();

    // assert memory leaks
    // display memory profile in an imgui window

    return 0;
}

// Helper functions




