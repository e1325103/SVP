#pragma once

#include "Vector.h"

#include <vector>
#include <string>
#include <iostream>

using namespace std;


class VectorField
{

public:

	VectorField();
	~VectorField();

	const Vector2&					vector(const int i) const;
	const Vector2&					vector(const int x, const int y, const int s) const;
	const Vector2*					vectors() const;

	const int						width() const;
	const int						height() const;
	const int						steps() const;
	const int						size() const;

	float							swapFloat(float f);
	bool							import();


private:

	int								m_Width;
	int								m_Height;
	int								m_Steps;
	int								m_Size;

	Vector2*						m_Vectors;

};
