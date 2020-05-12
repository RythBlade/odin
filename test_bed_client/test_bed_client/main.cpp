#include <stdio.h>
#include <iostream>

#include "Particle.h"
#include "DataServer.h"

#include <Windows.h>

unsigned int const numberOfParticles = 10;

struct ParticlePacket
{
    int packetStart;
    Particle particles[ numberOfParticles ];
    int  packetEnd;

public:
    ParticlePacket()
    {
        packetStart = 999999;
        packetEnd = -999999;
    }
};

void updateParticles( ParticlePacket* particles, float timeStep );
unsigned int getNextTimeStep();

float const worldAABB[] = { 10.0f, 10.0f, 10.0f };
float const particleRadius = 0.5f;

ParticlePacket particles;

float const nextNudge = 5.0f;
float const dampingConstant = 1.0f;
float const restitution = 0.8f;
float const frameTime = 1.0f / 60.0f;

// frame parameters
float nextNudgeTimer = 0.0f;

float timeAccumulator = 0.0f;

int frameCounter = 0;

float totalTime = 0.0f;

void main()
{
    printf( "I'm just testing\n" );
    printf( "Enter something to continue...\n" );

    for ( int i = 0; i < numberOfParticles; ++i )
    {
        for ( int comp = 0; comp < 3; ++comp )
        {
            float random = static_cast<float>( rand() ) / static_cast<float>(RAND_MAX);
            particles.particles[ i ].m_position[ comp ] = -worldAABB[ comp ] + particleRadius + random * ( worldAABB[ comp ] * 2.0f - 2.0f * particleRadius );
        }
    }

    DataServer server;

    server.initialiseServer();

    unsigned int currentTime = getNextTimeStep();
    
    while ( true )
    {
        unsigned int nextTimeStep = getNextTimeStep();
        float timeDelta = ( nextTimeStep - currentTime ) / 1000000.0f;

        if ( timeDelta <= 0.0f )
        {
            continue;
        }

        timeAccumulator += timeDelta;

        while ( timeAccumulator > frameTime )
        {
            timeAccumulator -= frameTime;
            totalTime += frameTime;

            updateParticles( &particles, frameTime );

            int const sizeOfNetworkPacket = 1024;
            char networkPacket[sizeOfNetworkPacket];
            char* bufferPointer = networkPacket;

            enum class PacketType : int
            {
                eRigidBodies
            };

            PacketType type = PacketType::eRigidBodies;

            int dataSize = 12 + 4 + (4 + 16 * sizeof(float)) * numberOfParticles;

            // base packet
            memcpy(bufferPointer, &frameCounter, sizeof(float)); bufferPointer += sizeof(float);
            memcpy(bufferPointer, &type, sizeof(PacketType)); bufferPointer += sizeof(PacketType);
            memcpy(bufferPointer, &dataSize, sizeof(int)); bufferPointer += sizeof(int);

            // rigid body packet
            memcpy(bufferPointer, &numberOfParticles, sizeof(unsigned int)); bufferPointer += sizeof(unsigned int);

            for (int i = 0; i < numberOfParticles; ++i)
            {
                memcpy(bufferPointer, &i, sizeof(int)); bufferPointer += sizeof(int);
                
                float dummy = 1.0f;
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
                
                memcpy(bufferPointer, &particles.particles[i].m_position[0], sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &particles.particles[i].m_position[1], sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &particles.particles[i].m_position[2], sizeof(float)); bufferPointer += sizeof(float);
                memcpy(bufferPointer, &dummy, sizeof(float)); bufferPointer += sizeof(float);
            }

            //server.sendData( reinterpret_cast< char* >( &particles ), sizeof( ParticlePacket ) );
            server.sendData( reinterpret_cast< char* >( &networkPacket), sizeOfNetworkPacket);

            ++frameCounter;
        }
                
        currentTime = nextTimeStep;
    }

    server.closeServer();

    return;
}

void updateParticles( ParticlePacket* particles, float timeStep )
{
    nextNudgeTimer -= timeStep;

    // random nudges!!
    if ( nextNudgeTimer <= 0.0f )
    {
        nextNudgeTimer = nextNudge;

        for ( int i = 0; i < numberOfParticles; ++i )
        {
            particles->particles[ i ].m_velocity[ 1 ] += 200.0f;

            for ( int comp = 0; comp < 3; ++comp )
            {
                particles->particles[ i ].m_velocity[ comp ] += ( rand() / RAND_MAX ) * 20.0f - 10.0f;
            }
        }
    }

    // particle updates!
    for ( int i = 0; i < numberOfParticles; ++i )
    {
        particles->particles[ i ].m_velocity[ 1 ] -= 9.81f;

        for ( int comp = 0; comp < 3; ++comp )
        {
            if (i == 0)
            {
                if (comp == 1)
                {
                    particles->particles[i].m_velocity[comp] = sinf(totalTime) * 10.0f;
                }
                else
                {
                    particles->particles[i].m_velocity[comp] = 0.0f;
                }
            }
            else
            {
                particles->particles[i].m_velocity[comp] *= dampingConstant;
            }

            // integrate
            particles->particles[ i ].m_position[ comp ] += particles->particles[ i ].m_velocity[ comp ] * timeStep;

            // bounds check
            if ( particles->particles[ i ].m_position[ comp ] > worldAABB[ comp ] )
            {
                particles->particles[ i ].m_position[ comp ] = worldAABB[ comp ];
                particles->particles[ i ].m_velocity[ comp ] *= -1.0f * restitution;
            }
            else if ( particles->particles[ i ].m_position[ comp ] < -worldAABB[ comp ] )
            {
                particles->particles[ i ].m_position[ comp ] = -worldAABB[ comp ];
                particles->particles[ i ].m_velocity[ comp ] *= -1.0f * restitution;
            }
        }
    }

    if ( particles->particles[ 0 ].m_velocity[ 0 ] != 0.0f || particles->particles[ 0 ].m_velocity[ 1 ] != 0.0f || particles->particles[ 0 ].m_velocity[ 2 ] != 0.0f )
    {
        printf( "particle position: %.2f, %.2f, %.2f     Velocity: %.2f, %.2f, %.2f\n"
            , particles->particles[ 0 ].m_position[ 0 ]
            , particles->particles[ 0 ].m_position[ 1 ]
            , particles->particles[ 0 ].m_position[ 2 ]
            , particles->particles[ 0 ].m_velocity[ 0 ]
            , particles->particles[ 0 ].m_velocity[ 1 ]
            , particles->particles[ 0 ].m_velocity[ 2 ] );
    }
}

unsigned int getNextTimeStep()
{
    LARGE_INTEGER StartingTime;
    LARGE_INTEGER Frequency;

    QueryPerformanceFrequency( &Frequency );
    QueryPerformanceCounter( &StartingTime );
    StartingTime.QuadPart *= 1000000;
    StartingTime.QuadPart /= Frequency.QuadPart;

    return ( unsigned int ) StartingTime.QuadPart;
}