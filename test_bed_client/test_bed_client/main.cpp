#include <stdio.h>
#include <iostream>

#include "Particle.h"
#include "DataServer.h"

#include <Windows.h>

int const numberOfParticles = 10;

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

            updateParticles( &particles, frameTime );

            server.sendData( reinterpret_cast< char* >( &particles ), sizeof( ParticlePacket ) );
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
            particles->particles[ i ].m_velocity[ comp ] *= dampingConstant;

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