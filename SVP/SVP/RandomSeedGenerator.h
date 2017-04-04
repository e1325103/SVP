#pragma once
#include "SeedGenerator.h"
#include <time.h> 

class RandomSeedGenerator : public SeedGenerator
{
public:
	RandomSeedGenerator(int _width, int _height, int _count);
	~RandomSeedGenerator();

	Vector3 getNextPoint();
	void start();
	bool update(Vector2 point);
	bool isFinished();
private:
	int count;
	int current;
};