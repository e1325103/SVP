#include "RungeKuttaIntegrator.h"

void RungeKuttaIntegrator::simulate() {

	int currentX = 0;
	int currentY = 0;

	float timePerStep = vectorField->steps() / steps;

	seedGenerator->start();

	while (!seedGenerator->isFinished()) {

		Vector3 startPoint = seedGenerator->getNextPoint();
		float x = (float)startPoint.x();
		float y = (float)startPoint.y();
		float s = 0;
		float direction = (float)startPoint.z();

		std::list<Vector3> points;
		int lastX = -1;
		int lastY = -1;

		bool outside = false;

		for (int j = 0; j < steps && !outside && !seedGenerator->isFinished(); j++) {

			Vector3 tempV = Integrator::interpolateBilinear(x, y);
			tempV.normaliseXY();

			float tempX = x + tempV.x() * delta / 2.0f * direction;
			float tempY = y + tempV.y() * delta / 2.0f * direction;

			outside = tempX < 0 || tempY < 0 || ((int)tempX + 1) >= vectorField->width() || ((int)tempY + 1) >= vectorField->height();

			if (!outside) {

				Vector3 v = Integrator::interpolateBilinear(tempX, tempY);
				v.normaliseXY();

				x = x + v.x() * delta * direction;
				y = y + v.y() * delta * direction;

				outside = x < 0 || y < 0 || ((int)x + 1) >= vectorField->width() || ((int)y + 1) >= vectorField->height();

				if (((int)x != lastX) && ((int)y != lastY)) {
					lastX = (int)x;
					lastY = (int)y;
					if (seedGenerator->update(Vector2(lastX, lastY))) {
						points.push_back(Vector3(x, y, v.z()));
					}
					else {
						outside = true;
					}
				}
			}
			s += timePerStep;
		}
		if (points.size() > 0) {
			lines.push_back(points);
		}
	}

}