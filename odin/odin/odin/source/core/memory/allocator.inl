#include <stdio.h>

namespace odin {
namespace core {

// need to protect against double deletes
// need to protext invalid linked list entries/pointers
// general pass for asserts.
// check for wrap around in the allocation counters in case "shananigans"
// check integrity of corruption markers in the headers


Allocator::Allocator()
    : m_totalAllocation( 0 )
    , m_numberOfAllocations( 0 )
    , m_allocationHead( nullptr )
{
}

template< typename Type>
inline void* Allocator::allocate( char const* sourceFileName, int sourceFileLineNumber )
{
    return allocate( sizeof( Type ), sourceFileName, sourceFileLineNumber );
}

inline void* Allocator::allocate( size_t size, char const* sourceFileName, int sourceFileLineNumber )
{
    printf( "Memory allocated! Size: %llu, Line num: %d, Filename: %s\n", size, sourceFileLineNumber, sourceFileName );

    // we decorate allocations with some extra information, that prefixes the clients memory allocation
    size_t const totalAllocationSize = size + sizeof( AllocationInfo );
    void* const allocation = malloc( totalAllocationSize );

    AllocationInfo* info = new( allocation ) AllocationInfo( sourceFileName, sourceFileLineNumber, totalAllocationSize, allocation );

    addToAllocationList(info);

    return static_cast<char*>( info->m_startOfAllocation ) + sizeof( AllocationInfo );
}

inline void Allocator::release( void* mem )
{
    AllocationInfo* const info = reinterpret_cast<AllocationInfo*>( static_cast<char*>( mem ) - sizeof( AllocationInfo ) );

    printf( "About to delete some memory! Size: %llu, Line num: %d, Filename: %s\n", info->m_sizeOfAllocation, info->m_lineNumber, info->m_sourceFileName );

    removeFromAllocationList(info);

    // the pointer passed in - isn't the ACTUAL memory. Need to delete the START of allocation
    free( info->m_startOfAllocation);
}

inline void Allocator::addToAllocationList( AllocationInfo* allocation )
{
    if ( m_allocationHead )
    {
        m_allocationHead->m_previous = allocation;
        allocation->m_next = m_allocationHead;
        allocation->m_previous = nullptr;

        m_allocationHead = allocation;
    }
    else
    { 
        m_allocationHead = allocation;
        allocation->m_previous = nullptr;
        allocation->m_next = nullptr;
    }

    m_totalAllocation += allocation->m_sizeOfAllocation;
    ++m_numberOfAllocations;
}

inline void Allocator::removeFromAllocationList( AllocationInfo* allocation )
{
    AllocationInfo* next = allocation->m_next;
    AllocationInfo* prev = allocation->m_previous;

    if (next)
    {
        next->m_previous = prev;
    }

    if (prev)
    {
        prev->m_next = next;
    }

    if (allocation == m_allocationHead)
    {
        m_allocationHead = next;
    }

    allocation->m_next = nullptr;
    allocation->m_previous = nullptr;

    m_totalAllocation -= allocation->m_sizeOfAllocation;
    --m_numberOfAllocations;
}

}
}