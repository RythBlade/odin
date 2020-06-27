#pragma once

#include "Shape.h"

#include <vector>

struct Particle
{
public:
    Particle()
    {
        reset();
    }

    void reset()
    {
        m_dampingConstant = 1.0f;

        for ( int i = 0; i < 3; ++i )
        {
            m_position[ i ] = 0.0f;
            m_velocity[ i ] = 0.0f;
        }
    }

    float m_position[ 3 ];
    float m_velocity[ 3 ];

    float m_dampingConstant = 1.0f;

    std::vector<Shape*> m_collisionShapes;
};

