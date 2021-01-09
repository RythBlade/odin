#pragma once

#include "Shape.h"

#include <vector>

struct ConvexHull : public Shape
{
    struct Vertex
    {
        float m_position[3];
        float m_normal[3];

        Vertex()
        {
            m_position[0] = 0.0f;
            m_position[1] = 0.0f;
            m_position[2] = 0.0f;

            m_normal[0] = 0.0f;
            m_normal[1] = 0.0f;
            m_normal[2] = 0.0f;
        }

        Vertex(float xp, float yp, float zp, float xn, float yn, float zn)
        {
            m_position[0] = xp;
            m_position[1] = yp;
            m_position[2] = zp;

            m_normal[0] = xn;
            m_normal[1] = yn;
            m_normal[2] = zn;
        }
    };

    struct Face
    {
        int m_vertexIds[3];

        Face()
        {
            m_vertexIds[0] = 0;
            m_vertexIds[1] = 0;
            m_vertexIds[2] = 0;
        }

        Face(int zero, int one, int two)
        {
            m_vertexIds[0] = zero;
            m_vertexIds[1] = one;
            m_vertexIds[2] = two;
        }
    };

    ConvexHull()
    {
    }

    std::vector<Face> faces;
    std::vector<Vertex> vertices;
};