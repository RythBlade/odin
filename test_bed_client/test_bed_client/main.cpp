#include <stdio.h>
#include <iostream>
#include <string>
#include <vector>

#include "Particle.h"
#include "DataServer.h"

#include <Windows.h>
#include "ObbShape.h"
#include "ConvexHull.h"

#include "proto.generated/rigid_body.pb.h"
#include "proto.generated/message_header.pb.h"
#include "proto.generated/shapes.pb.h"

unsigned int const numberOfParticles = 10;

struct ParticlePacket
{
    Particle particles[ numberOfParticles ];

public:
    ParticlePacket()
    {
    }
};

ConvexHull* loadMesh();
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

    DataServer server;

    server.initialiseServer();

    for ( int i = 0; i < numberOfParticles; ++i )
    {
        Particle& particle = particles.particles[i];

        for ( int comp = 0; comp < 3; ++comp )
        {
            float random = static_cast<float>( rand() ) / static_cast<float>(RAND_MAX);
            particle.m_position[ comp ] = -worldAABB[ comp ] + particleRadius + random * ( worldAABB[ comp ] * 2.0f - 2.0f * particleRadius );
        }

        ObbShape* obbShape = new ObbShape();

        obbShape->m_halfExtents[0] = 1.0f;
        obbShape->m_halfExtents[1] = 1.0f;
        obbShape->m_halfExtents[2] = 1.0f;

        particle.m_collisionShapes.push_back(obbShape);

        //ConvexHull* convexHull = loadMesh();
        //
        //convexHull->m_hasLocalMatrix = false;
        //
        //particle.m_collisionShapes.push_back(convexHull);

        PhysicsTelemetry::OBBShape obbCreated;

        obbCreated.mutable_halfextents()->set_x(obbShape->m_halfExtents[0]);
        obbCreated.mutable_halfextents()->set_y(obbShape->m_halfExtents[1]);
        obbCreated.mutable_halfextents()->set_z(obbShape->m_halfExtents[2]);

        obbCreated.mutable_base()->set_id(i);
        obbCreated.mutable_base()->set_shapetype(PhysicsTelemetry::OBB);
        obbCreated.mutable_base()->set_haslocalmatrix(true);
        
        obbCreated.mutable_base()->mutable_localmatrix()->set_m11(1.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m12(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m13(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m14(0.0f);

        obbCreated.mutable_base()->mutable_localmatrix()->set_m21(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m22(1.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m23(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m24(0.0f);

        obbCreated.mutable_base()->mutable_localmatrix()->set_m31(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m32(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m33(1.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m34(0.0f);

        obbCreated.mutable_base()->mutable_localmatrix()->set_m41(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m42(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m43(0.0f);
        obbCreated.mutable_base()->mutable_localmatrix()->set_m44(1.0f);

        size_t shapeSize = obbCreated.ByteSizeLong();

        PhysicsTelemetry::ShapeCreated shapeCreated;

        shapeCreated.set_shapetype(PhysicsTelemetry::OBB);
        shapeCreated.set_shapesize(shapeSize);

        size_t baseShapeSize = shapeCreated.ByteSizeLong();

        PhysicsTelemetry::MessageHeader messageHeader;
        messageHeader.set_frameid(frameCounter);
        messageHeader.set_messagetype(PhysicsTelemetry::MessageHeader_MessageType_ShapeCreated);
        messageHeader.set_datasize(baseShapeSize);

        int const sizeOfNetworkPacket = 1024;
        char networkPacket[sizeOfNetworkPacket];
        char* bufferPointer = networkPacket;
        int remainingBytes = sizeOfNetworkPacket;

        int headerLength = messageHeader.ByteSizeLong();
        memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

        messageHeader.SerializePartialToArray(bufferPointer, sizeOfNetworkPacket);
        bufferPointer += headerLength;
        remainingBytes -= headerLength;

        shapeCreated.SerializePartialToArray(bufferPointer, remainingBytes);
        bufferPointer += baseShapeSize;
        remainingBytes -= baseShapeSize;

        obbCreated.SerializePartialToArray(bufferPointer, remainingBytes);
        bufferPointer += baseShapeSize;
        remainingBytes -= baseShapeSize;

        server.sendData(reinterpret_cast<char*>(&networkPacket), sizeOfNetworkPacket);
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
            totalTime += frameTime;

            updateParticles( &particles, frameTime );

            PhysicsTelemetry::RigidBodyList bodyList;
            
            for (int i = 0; i < numberOfParticles; ++i)
            {
                PhysicsTelemetry::RigidBody* rigidBody = bodyList.add_rigidbodies();

                rigidBody->set_id(i);

                PhysicsTelemetry::Vector4 position;
                rigidBody->mutable_position()->set_m11(1.0f);
                rigidBody->mutable_position()->set_m12(0.0f);
                rigidBody->mutable_position()->set_m13(0.0f);
                rigidBody->mutable_position()->set_m14(0.0f);

                rigidBody->mutable_position()->set_m21(0.0f);
                rigidBody->mutable_position()->set_m22(1.0f);
                rigidBody->mutable_position()->set_m23(0.0f);
                rigidBody->mutable_position()->set_m24(0.0f);

                rigidBody->mutable_position()->set_m31(0.0f);
                rigidBody->mutable_position()->set_m32(0.0f);
                rigidBody->mutable_position()->set_m33(1.0f);
                rigidBody->mutable_position()->set_m34(0.0f);

                rigidBody->mutable_position()->set_m41(particles.particles[i].m_position[0]);
                rigidBody->mutable_position()->set_m42(particles.particles[i].m_position[1]);
                rigidBody->mutable_position()->set_m43(particles.particles[i].m_position[2]);
                rigidBody->mutable_position()->set_m44(particles.particles[i].m_position[3]);

                PhysicsTelemetry::Vector4 velocity;
                rigidBody->mutable_velocity()->set_x(particles.particles[i].m_velocity[0]);
                rigidBody->mutable_velocity()->set_y(particles.particles[i].m_velocity[1]);
                rigidBody->mutable_velocity()->set_z(particles.particles[i].m_velocity[2]);
            }

            size_t bodyListPacketSize = bodyList.ByteSizeLong();

            PhysicsTelemetry::MessageHeader messageHeader;
            messageHeader.set_frameid(frameCounter);
            messageHeader.set_messagetype(PhysicsTelemetry::MessageHeader_MessageType_RigidBodyUpdate);
            messageHeader.set_datasize(bodyListPacketSize);

            int const sizeOfNetworkPacket = 1024;
            char networkPacket[sizeOfNetworkPacket];
            char* bufferPointer = networkPacket;

            int headerLength = messageHeader.ByteSizeLong();
            memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

            messageHeader.SerializePartialToArray(bufferPointer, sizeOfNetworkPacket);
            bufferPointer += headerLength;

            bodyList.SerializePartialToArray(bufferPointer, sizeOfNetworkPacket - headerLength);

            server.sendData(reinterpret_cast<char*>(&networkPacket), sizeOfNetworkPacket);








            /*int const sizeOfNetworkPacket = 1024;
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
            server.sendData( reinterpret_cast< char* >( &networkPacket), sizeOfNetworkPacket);*/

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

ConvexHull* loadMesh()
{
    ConvexHull* convexHull = new ConvexHull();

    float randomOffset = static_cast<float>(rand()) / static_cast<float>(RAND_MAX);

    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f)); // Front
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f)); // BACK
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f)); // Top
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f)); // Bottom
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));

    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 1.0f)); // Left
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));

    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 0.0f, 1.0f, 1.0f)); // Right
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 1, convexHull->vertices.size() - 2, convexHull->vertices.size() - 3));
    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
        
    return convexHull;
}