#include "RungeKuttaIntegrator.h"

void RungeKuttaIntegrator::simulate() {


	std::list<Vector2> points;

	for (int i = 0; i < 5; i++) {
		for (int x = 0; x < 500; x++) {
			points.push_back(Vector2(x, (x * 0.5 + (i * 10))));
		}

		lines.push_back(points);
		points.clear();
	}

	for (int i = 0; i < 5; i++) {
		for (int x = 0; x < 500; x++) {
			points.push_back(Vector2(x, 500 - (x * 0.5 + (i * 10))));
		}

		lines.push_back(points);
		points.clear();
	}

	/*int currentX = 0;
	int currentY = 0;

	float timePerStep = vectorField->steps() / steps;

	seedGenerator->start();

	while (!seedGenerator->isFinished()) {

		Vector3 startPoint = seedGenerator->getNextPoint();
		float x = (float)startPoint.x();
		float y = (float)startPoint.y();
		float t = 0;
		float direction = (float)startPoint.z();

		std::list<Vector2> points;
		int lastX = -1;
		int lastY = -1;

		bool outside = false;

		for (int j = 0; j < steps && !outside && !seedGenerator->isFinished(); j++) {

			Vector2 tempV = Integrator::interpolateBilinear(x, y, (int)round(t));
			tempV.normalise();

			float tempX = x + tempV.x() * delta / 2.0f * direction;
			float tempY = y + tempV.y() * delta / 2.0f * direction;

			outside = tempX < 0 || tempY < 0 || ((int)tempX + 1) >= vectorField->width() || ((int)tempY + 1) >= vectorField->height();

			if (!outside) {

				Vector2 v = Integrator::interpolateBilinear(tempX, tempY, (int)round(t));
				v.normalise();

				x = x + v.x() * delta * direction;
				y = y + v.y() * delta * direction;

				outside = x < 0 || y < 0 || ((int)x + 1) >= vectorField->width() || ((int)y + 1) >= vectorField->height();

				if (((int)x != lastX) && ((int)y != lastY)) {
					lastX = (int)x;
					lastY = (int)y;
					if (seedGenerator->update(Vector2(lastX, lastY))) {
						points.push_back(Vector2(x, y));
					}
					else {
						outside = true;
					}
				}
			}
			t += timePerStep;
		}
		if (points.size() > 0) {
			lines.push_back(points);
		}
	}*/

}