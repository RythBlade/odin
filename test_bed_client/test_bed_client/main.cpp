#include <stdio.h>
#include <iostream>
#include <Windows.h>

struct Particle
{
public:
    Particle() 
    { 
        reset();
    }

    void reset()
    {
        for ( int i = 0; i < 3; ++i )
        {
            m_position[ i ] = 0.0f;
            m_velocity[ i ] = 0.0f;
        }
    }

    float m_position[ 3 ];
    float m_velocity[ 3 ];
};

void updateParticles( Particle* particles, float timeStep );
unsigned int getNextTimeStep();

int const numberOfParticles = 1;
Particle particles[ numberOfParticles ];

float const worldAABB[] = { 10.0f, 10.0f, 10.0f };
float const particleRadius = 0.5f;

float const nextNudge = 5.0f;
float const dampingConstant = 1.0f;
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
            particles[ i ].m_position[ comp ] = -worldAABB[ comp ] + particleRadius + ( rand() / RAND_MAX ) * ( worldAABB[ comp ] * 2.0f - 2.0f * particleRadius );
        }
    }

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

            updateParticles( particles, frameTime );
        }
                
        currentTime = nextTimeStep;
    }

    return;
}

void updateParticles( Particle* particles, float timeStep )
{
    nextNudgeTimer -= timeStep;

    // random nudges!!
    if ( nextNudgeTimer <= 0.0f )
    {
        nextNudgeTimer = nextNudge;

        for ( int i = 0; i < numberOfParticles; ++i )
        {
            for ( int comp = 0; comp < 3; ++comp )
            {
                particles[ i ].m_velocity[ comp ] += ( rand() / RAND_MAX ) * 4.0f - 1.0f;
            }
        }
    }

    // particle updates!
    for ( int i = 0; i < numberOfParticles; ++i )
    {
        for ( int comp = 0; comp < 3; ++comp )
        {
            particles[ i ].m_velocity[ comp ] *= dampingConstant * timeStep;

            // integrate
            particles[ i ].m_position[ comp ] += particles[ i ].m_velocity[ comp ] * timeStep;

            // bounds check
            if ( particles[ i ].m_position[ comp ] > worldAABB[ comp ] )
            {
                particles[ i ].m_position[ comp ] = worldAABB[ comp ];
                particles[ i ].m_velocity[ comp ] *= -1.0f;
            }
            else if ( particles[ i ].m_position[ comp ] < -worldAABB[ comp ] )
            {
                particles[ i ].m_position[ comp ] = -worldAABB[ comp ];
                particles[ i ].m_velocity[ comp ] *= -1.0f;
            }
        }
    }

    if ( particles[ 0 ].m_velocity[ 0 ] != 0.0f || particles[ 0 ].m_velocity[ 1 ] != 0.0f || particles[ 0 ].m_velocity[ 2 ] != 0.0f )
    {
        printf( "particle position: %.2f, %.2f, %.2f     Velocity: %.2f, %.2f, %.2f\n"
            , particles[ 0 ].m_position[ 0 ]
            , particles[ 0 ].m_position[ 1 ]
            , particles[ 0 ].m_position[ 2 ]
            , particles[ 0 ].m_velocity[ 0 ]
            , particles[ 0 ].m_velocity[ 1 ]
            , particles[ 0 ].m_velocity[ 2 ] );
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