#pragma once
#include "Vector.h"

#include <list>

class SeedGenerator
{
public:
	SeedGenerator(int _width, int _height);
	~SeedGenerator();

	virtual Vector3 getNextPoint() = 0;
	virtual void start() = 0;
	virtual bool update(Vector2 point) = 0;
	virtual bool isFinished() = 0;
protected:
	int width;
	int height;
	Vector2 lastPoint;
	float direction = -1.0f;
};