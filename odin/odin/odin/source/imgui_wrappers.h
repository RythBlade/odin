#ifndef IMGUI_WRAPPERS_H
#define IMGUI_WRAPPERS_H

namespace odin
{
    class ImguiWrappers
    {
    public:
        static void* MallocWrapper(size_t size, void* user_data);
        static void FreeWrapper(void* ptr, void* user_data);
    };
}

#endif