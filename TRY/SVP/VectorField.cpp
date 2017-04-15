#include "VectorField.h"

#include <sstream>
#include <fstream>

#include <iostream>
#include <string>---------------------------------------------------------------------

VectorField::VectorField()
{
}

VectorField::~VectorField()
{
}

const Vector2& VectorField::vector(const int i) const
{
	return m_Vectors[i];
}

const Vector2& VectorField::vector(const int x, const int y, const int s) const
{
	return m_Vectors[x + y * m_Width + s * m_Size];
}

const int VectorField::width() const
{
	return m_Width;
}

const int VectorField::height() const
{
	return m_Height;
}

const int VectorField::size() const
{
	return m_Size;
}

float VectorField::swapFloat(float f)
{
	union
	{
		float f;
		unsigned char b[4];
	} dat1, dat2;

	dat1.f = f;
	dat2.b[0] = dat1.b[3];
	dat2.b[1] = dat1.b[2];
	dat2.b[2] = dat1.b[1];
	dat2.b[3] = dat1.b[0];
	return dat2.f;
}

bool VectorField::import()
{
	float a, b;
	int c = 0;

	m_Width = 500;
	m_Height = 500;
	m_Steps = 48;
	m_Size = m_Width * m_Height * m_Steps;
	m_Vectors = new Vector2[m_Size];
	for (int i = 1; i <= 48; i++) {
		string s = to_string(i);
		if (i < 10) {
			s = "0" + s;
		}
		std::ifstream f("D:\\WindData\\Uf" + s + ".bin", std::ios::binary);
		std::ifstream g("D:\\WindData\\Vf" + s + ".bin", std::ios::binary);
			for (int y = 0; y < 500; y++) {
				for (int x = 0; x < 500; x++) {
					f.read((char*)&a, sizeof(float));
					g.read((char*)&b, sizeof(float));
					m_Vectors[c] = Vector2(swapFloat(a), swapFloat(b));
					c++;
				}
			}
	}
	return true;
}
