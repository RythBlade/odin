#include <stdio.h>
#include <iostream>
#include <string>
#include <vector>

#include "Particle.h"
#include "DataServer.h"

#include <Windows.h>
#include "ObbShape.h"
#include "ConvexHull.h"
#include "TetrahedronShape.h"

#include "proto.generated/rigid_body.pb.h"
#include "proto.generated/message_header.pb.h"
#include "proto.generated/shapes.pb.h"

unsigned int const numberOfParticles = 10;

unsigned int const c_defaultPacketSize = 2048;

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

int addObb(int nextShapeId, Particle& particle, DataServer& server, bool hasLocalMatrix, float offsetX, float offsetY, float offsetZ)
{
    ObbShape* obbShape = new ObbShape();

    obbShape->m_id = nextShapeId;
    obbShape->m_halfExtents[0] = 1.0f;
    obbShape->m_halfExtents[1] = 1.0f;
    obbShape->m_halfExtents[2] = 1.0f;
    obbShape->m_hasLocalMatrix = hasLocalMatrix;

    particle.m_collisionShapes.push_back(obbShape);

    PhysicsTelemetry::ObbShapePacket obbCreated;

    obbCreated.mutable_halfextents()->set_x(obbShape->m_halfExtents[0]);
    obbCreated.mutable_halfextents()->set_y(obbShape->m_halfExtents[1]);
    obbCreated.mutable_halfextents()->set_z(obbShape->m_halfExtents[2]);

    obbCreated.mutable_base()->set_id(nextShapeId);
    obbCreated.mutable_base()->set_shapetype(PhysicsTelemetry::OBB);
    obbCreated.mutable_base()->set_haslocalmatrix(hasLocalMatrix);

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

    obbCreated.mutable_base()->mutable_localmatrix()->set_m41(offsetX);
    obbCreated.mutable_base()->mutable_localmatrix()->set_m42(offsetY);
    obbCreated.mutable_base()->mutable_localmatrix()->set_m43(offsetZ);
    obbCreated.mutable_base()->mutable_localmatrix()->set_m44(1.0f);

    size_t shapeSize = obbCreated.ByteSizeLong();

    PhysicsTelemetry::ShapeCreatedMessage shapeCreated;

    shapeCreated.set_shapetype(obbCreated.mutable_base()->shapetype());
    shapeCreated.set_shapesize(shapeSize);

    size_t baseShapeSize = shapeCreated.ByteSizeLong();

    PhysicsTelemetry::MessageHeaderMessage messageHeader;
    messageHeader.set_frameid(frameCounter);
    messageHeader.set_messagetype(PhysicsTelemetry::MessageHeaderMessage_MessageType_ShapeCreated);
    messageHeader.set_datasize(baseShapeSize);

    char networkPacket[c_defaultPacketSize];
    char* bufferPointer = networkPacket;
    int remainingBytes = c_defaultPacketSize;

    int headerLength = messageHeader.ByteSizeLong();
    memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

    messageHeader.SerializePartialToArray(bufferPointer, c_defaultPacketSize);
    bufferPointer += headerLength;
    remainingBytes -= headerLength;

    shapeCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    obbCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    ++nextShapeId;

    server.sendData(reinterpret_cast<char*>(&networkPacket), c_defaultPacketSize);

    return nextShapeId;
}

int addConvexHull(int nextShapeId, Particle& particle, DataServer& server, bool hasLocalMatrix, float offsetX, float offsetY, float offsetZ)
{
    ConvexHull* convexHull = loadMesh();
    
    convexHull->m_id = nextShapeId;

    convexHull->m_hasLocalMatrix = hasLocalMatrix;
    
    particle.m_collisionShapes.push_back(convexHull);

    PhysicsTelemetry::ConvexHullShapePacket convexHullCreated;

    convexHullCreated.mutable_base()->set_id(nextShapeId);
    convexHullCreated.mutable_base()->set_shapetype(PhysicsTelemetry::ConvexHull);
    convexHullCreated.mutable_base()->set_haslocalmatrix(hasLocalMatrix);

    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m11(1.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m12(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m13(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m14(0.0f);

    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m21(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m22(1.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m23(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m24(0.0f);

    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m31(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m32(0.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m33(1.0f);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m34(0.0f);

    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m41(offsetX);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m42(offsetY);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m43(offsetZ);
    convexHullCreated.mutable_base()->mutable_localmatrix()->set_m44(1.0f);

    for (int i = 0; i < convexHull->faces.size(); ++i)
    {
        PhysicsTelemetry::ConvexHullShapePacket_Face* face = convexHullCreated.add_faces();
        face->set_vert0(convexHull->faces[i].m_vertexIds[0]);
        face->set_vert1(convexHull->faces[i].m_vertexIds[1]);
        face->set_vert2(convexHull->faces[i].m_vertexIds[2]);
    }

    for (int i = 0; i < convexHull->vertices.size(); ++i)
    {
        PhysicsTelemetry::ConvexHullShapePacket_Vertex* vertexCreated = convexHullCreated.add_vertices();
        vertexCreated->mutable_position()->set_x(convexHull->vertices[i].m_position[0]);
        vertexCreated->mutable_position()->set_y(convexHull->vertices[i].m_position[1]);
        vertexCreated->mutable_position()->set_z(convexHull->vertices[i].m_position[2]);

        vertexCreated->mutable_normal()->set_x(convexHull->vertices[i].m_normal[0]);
        vertexCreated->mutable_normal()->set_y(convexHull->vertices[i].m_normal[1]);
        vertexCreated->mutable_normal()->set_z(convexHull->vertices[i].m_normal[2]);
    }

    size_t shapeSize = convexHullCreated.ByteSizeLong();

    PhysicsTelemetry::ShapeCreatedMessage shapeCreated;

    shapeCreated.set_shapetype(convexHullCreated.mutable_base()->shapetype());
    shapeCreated.set_shapesize(shapeSize);

    size_t baseShapeSize = shapeCreated.ByteSizeLong();

    PhysicsTelemetry::MessageHeaderMessage messageHeader;
    messageHeader.set_frameid(frameCounter);
    messageHeader.set_messagetype(PhysicsTelemetry::MessageHeaderMessage_MessageType_ShapeCreated);
    messageHeader.set_datasize(baseShapeSize);

    char networkPacket[c_defaultPacketSize];
    char* bufferPointer = networkPacket;
    int remainingBytes = c_defaultPacketSize;

    int headerLength = messageHeader.ByteSizeLong();
    memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

    messageHeader.SerializePartialToArray(bufferPointer, c_defaultPacketSize);
    bufferPointer += headerLength;
    remainingBytes -= headerLength;

    shapeCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    convexHullCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    ++nextShapeId;

    server.sendData(reinterpret_cast<char*>(&networkPacket), c_defaultPacketSize);

    return nextShapeId;
}

int addTetrahedron(int nextShapeId, Particle& particle, DataServer& server, bool hasLocalMatrix, float offsetX, float offsetY, float offsetZ)
{
    TetrahedronShape* tetraShape = new TetrahedronShape();

    tetraShape->m_id = nextShapeId;

    particle.m_collisionShapes.push_back(tetraShape);

    PhysicsTelemetry::TetrahedronShapePacket tetraCreated;

    tetraCreated.mutable_base()->set_id(nextShapeId);
    tetraCreated.mutable_base()->set_shapetype(PhysicsTelemetry::Tetrahedron);
    tetraCreated.mutable_base()->set_haslocalmatrix(hasLocalMatrix);

    tetraCreated.mutable_base()->mutable_localmatrix()->set_m11(1.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m12(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m13(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m14(0.0f);

    tetraCreated.mutable_base()->mutable_localmatrix()->set_m21(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m22(1.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m23(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m24(0.0f);

    tetraCreated.mutable_base()->mutable_localmatrix()->set_m31(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m32(0.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m33(1.0f);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m34(0.0f);

    tetraCreated.mutable_base()->mutable_localmatrix()->set_m41(offsetX);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m42(offsetY);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m43(offsetZ);
    tetraCreated.mutable_base()->mutable_localmatrix()->set_m44(1.0f);

    size_t shapeSize = tetraCreated.ByteSizeLong();

    PhysicsTelemetry::ShapeCreatedMessage shapeCreated;

    shapeCreated.set_shapetype(tetraCreated.mutable_base()->shapetype());
    shapeCreated.set_shapesize(shapeSize);

    size_t baseShapeSize = shapeCreated.ByteSizeLong();

    PhysicsTelemetry::MessageHeaderMessage messageHeader;
    messageHeader.set_frameid(frameCounter);
    messageHeader.set_messagetype(PhysicsTelemetry::MessageHeaderMessage_MessageType_ShapeCreated);
    messageHeader.set_datasize(baseShapeSize);

    char networkPacket[c_defaultPacketSize];
    char* bufferPointer = networkPacket;
    int remainingBytes = c_defaultPacketSize;

    int headerLength = messageHeader.ByteSizeLong();
    memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

    messageHeader.SerializePartialToArray(bufferPointer, c_defaultPacketSize);
    bufferPointer += headerLength;
    remainingBytes -= headerLength;

    shapeCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    tetraCreated.SerializePartialToArray(bufferPointer, remainingBytes);
    bufferPointer += baseShapeSize;
    remainingBytes -= baseShapeSize;

    ++nextShapeId;

    server.sendData(reinterpret_cast<char*>(&networkPacket), c_defaultPacketSize);            
    
    return nextShapeId;
}

void main()
{
    printf( "I'm just testing\n" );
    printf( "Enter something to continue...\n" );

    DataServer server;

    server.initialiseServer();

    int nextShapeId = 0;

    for ( int i = 0; i < numberOfParticles; ++i )
    {
        Particle& particle = particles.particles[i];

        for ( int comp = 0; comp < 3; ++comp )
        {
            float random = static_cast<float>( rand() ) / static_cast<float>(RAND_MAX);
            particle.m_position[ comp ] = -worldAABB[ comp ] + particleRadius + random * ( worldAABB[ comp ] * 2.0f - 2.0f * particleRadius );
        }

        nextShapeId = addObb(nextShapeId, particle, server, false, 0.0f, 0.0f, 0.0f);

        // extra collision shapes on some of the objects
        if (i % 2 == 0)
        {
            nextShapeId = addObb(nextShapeId, particle, server, false, 0.0f, 1.0f, 1.0f);
        }
        
        if (i % 5 == 0)
        {
            nextShapeId = addTetrahedron(nextShapeId, particle, server, true, 1.0f, 0.0f, 1.0f);
        
        }
        
        if (i % 6 == 0)
        {
            nextShapeId = addConvexHull(nextShapeId, particle, server, true, 1.0f, 2.0f, 1.0f);
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
            totalTime += frameTime;

            updateParticles( &particles, frameTime );

            PhysicsTelemetry::RigidBodyListPacket bodyList;
            
            for (int i = 0; i < numberOfParticles; ++i)
            {
                PhysicsTelemetry::RigidBodyPacket* rigidBody = bodyList.add_rigidbodies();

                rigidBody->set_id(i);

                PhysicsTelemetry::Vector4Packet position;
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

                PhysicsTelemetry::Vector4Packet velocity;
                rigidBody->mutable_velocity()->set_x(particles.particles[i].m_velocity[0]);
                rigidBody->mutable_velocity()->set_y(particles.particles[i].m_velocity[1]);
                rigidBody->mutable_velocity()->set_z(particles.particles[i].m_velocity[2]);

                for (int shapeIndex = 0; shapeIndex < particles.particles[i].m_collisionShapes.size(); ++shapeIndex)
                {
                    rigidBody->add_collisionshapes(particles.particles[i].m_collisionShapes[shapeIndex]->m_id);
                }
            }

            size_t bodyListPacketSize = bodyList.ByteSizeLong();

            PhysicsTelemetry::MessageHeaderMessage messageHeader;
            messageHeader.set_frameid(frameCounter);
            messageHeader.set_messagetype(PhysicsTelemetry::MessageHeaderMessage_MessageType_RigidBodyUpdate);
            messageHeader.set_datasize(bodyListPacketSize);

            char networkPacket[c_defaultPacketSize];
            char* bufferPointer = networkPacket;

            int headerLength = messageHeader.ByteSizeLong();
            memcpy(bufferPointer, &headerLength, sizeof(int)); bufferPointer += sizeof(int);

            messageHeader.SerializePartialToArray(bufferPointer, c_defaultPacketSize);
            bufferPointer += headerLength;

            bodyList.SerializePartialToArray(bufferPointer, c_defaultPacketSize - headerLength);

            server.sendData(reinterpret_cast<char*>(&networkPacket), c_defaultPacketSize);

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
        printf( "Frame id: %d,     particle position: %.2f, %.2f, %.2f     Velocity: %.2f, %.2f, %.2f\n"
            , frameCounter
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

    float randomOffset = 0.5f + (static_cast<float>(rand()) / static_cast<float>(RAND_MAX) ) * 2.0f;

    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f)); // Front
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f)); // BACK
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f)); // Top
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f,  1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, 1.0f, -1.0f, /**/ 0.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));

    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f)); // Bottom
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f, -1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,-1.0f,  1.0f, /**/ 1.0f, 1.0f, 0.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));

    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 1.0f)); // Left
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f, -1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex(-randomOffset, -randomOffset, -randomOffset, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f,  1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex(-1.0f,  1.0f, -1.0f, /**/ 1.0f, 0.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));

    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 0.0f, 1.0f, 1.0f)); // Right
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f, -1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
    convexHull->vertices.push_back(ConvexHull::Vertex( randomOffset, -randomOffset, -randomOffset, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f, -1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->vertices.push_back(ConvexHull::Vertex( 1.0f,  1.0f,  1.0f, /**/ 0.0f, 1.0f, 1.0f));
    convexHull->faces.push_back(ConvexHull::Face(convexHull->vertices.size() - 3, convexHull->vertices.size() - 2, convexHull->vertices.size() - 1));
        
    return convexHull;
}