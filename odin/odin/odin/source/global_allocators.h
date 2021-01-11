#ifndef GLOBAL_ALLOCATORS_H
#define GLOBAL_ALLOCATORS_H

#include "core/memory/allocator.h"

namespace odin
{
    class GlobalAllocators
    {
    public:
        static core::Allocator g_imguiAllocator;
    };
}

#endif
