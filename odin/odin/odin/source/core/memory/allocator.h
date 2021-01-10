#ifndef ALLOCATOR_H
#define ALLOCATOR_H

namespace odin {
namespace core {

    static_assert( sizeof( char ) == 1, "we're using the type char to measure a single byte, and sizeof(char) != 1 in the current compilation environment" );

    struct AllocationInfo
    {
        AllocationInfo( char const* sourceFilename, int lineNumber, size_t sizeOfAllocation, void* startOfAllocation )
            : m_sourceFileName( sourceFilename )
            , m_lineNumber( lineNumber )
            , m_sizeOfAllocation( sizeOfAllocation )
            , m_startOfAllocation( startOfAllocation )
        {
        }

        unsigned char const m_corruptionValueHeader = 0xEF;

        char const* const m_sourceFileName;
        int const m_lineNumber;
        size_t const m_sizeOfAllocation;
        void* const m_startOfAllocation;

        // put allocations in a linked list so we can also track memory leaks
        AllocationInfo* m_next = nullptr;
        AllocationInfo* m_previous = nullptr;

        unsigned char const m_corruptionValueFooter = 0xFE;
    };

    class Allocator
    {
    public:
        Allocator();

        template< typename Type>
        inline void* allocate(
#ifdef ODIN_DEBUG
            char const* sourceFileName
            , int sourceFileLineNumber
#endif
        );

        inline void* allocate(
            size_t size
#ifdef ODIN_DEBUG
            , char const* sourceFileName
            , int sourceFileLineNumber
#endif
        );
        
        inline void release( void* mem );

        inline size_t getTotalAllocatedMemory() const { return m_totalAllocation; }
        inline size_t getNumberOfAllocations() const { return m_numberOfAllocations; }

        AllocationInfo const* getAllocationListHead() const { return m_allocationHead; }

    private:
        inline void addToAllocationList(AllocationInfo* allocation);
        inline void removeFromAllocationList(AllocationInfo* allocation);


    private:
        size_t m_totalAllocation;
        size_t m_numberOfAllocations;
        AllocationInfo* m_allocationHead;
    };

}
}

#include "allocator.inl"

#endif
