#include "RandomSeedGenerator.h"

RandomSeedGenerator::RandomSeedGenerator(int _width, int _height, int _count) : SeedGenerator(_width, _height), count(_count), current(0)
{
	srand((int)time(NULL));
}

RandomSeedGenerator::~RandomSeedGenerator()
{
}

Vector3 RandomSeedGenerator::getNextPoint() {
	if (direction > 0.0f) {
		direction = -1.0f;
		return Vector3(lastPoint, direction);
	}
	else {
		direction = 1.0f;
	}

	current++;
	float x = 0 + (float)(rand() % (int)(SeedGenerator::width));
	float y = 0 + (float)(rand() % (int)(SeedGenerator::height));
	lastPoint = Vector2(x, y);
	return Vector3(lastPoint, direction);
}

void RandomSeedGenerator::start() {
	current = 0;
}

bool RandomSeedGenerator::update(Vector2 point) {
	return true;
}

bool RandomSeedGenerator::isFinished() {
	return current >= count && direction < 0.0f;
}