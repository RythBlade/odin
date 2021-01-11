#include "imgui_wrappers.h"
#include "imgui.h"

#include "global_allocators.h"

void* odin::ImguiWrappers::MallocWrapper(size_t size, void* user_data)
{
    return GlobalAllocators::g_imguiAllocator.allocate(size, __FILE__, __LINE__);
}

void odin::ImguiWrappers::FreeWrapper(void* ptr, void* user_data)
{
    if (ptr)
    {
        IM_UNUSED(user_data);
        GlobalAllocators::g_imguiAllocator.release(ptr);
    }
}
