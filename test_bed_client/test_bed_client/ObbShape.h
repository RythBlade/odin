#pragma once

#include "Shape.h"

struct ObbShape : public Shape
{
    float m_halfExtents[3];
};