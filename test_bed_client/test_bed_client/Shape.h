#pragma once

struct Shape
{
    Shape()
        : m_hasLocalMatrix( false )
    {
        for (int i = 0; i < 16; ++i)
        {
            m_localMatrix[i] = 0.0f;
        }
    }

    bool m_hasLocalMatrix;
    float m_localMatrix[16];
};