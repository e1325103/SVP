#include "EulerIntegrator.h"

void EulerIntegrator::simulate() {

	int currentX = 0;
	int currentY = 0;

	seedGenerator->start();

	while (!seedGenerator->isFinished()) {

		Vector3 startPoint = seedGenerator->getNextPoint();
		float x = (float)startPoint.x();
		float y = (float)startPoint.y();
		float direction = (float)startPoint.z();

		std::list<Vector3> points;
		int lastX = -1;
		int lastY = -1;

		bool outside = false;

		for (int j = 0; j < steps && !outside; j++) {

			Vector3 v = Integrator::interpolateBilinear(x, y);
			v.normaliseXY();

			x = x + v.x() * delta * direction;
			y = y + v.y() * delta * direction;


			outside = x < 0 || y < 0 || ((int)x + 1) >= vectorField->width() || ((int)y + 1) >= vectorField->height();

			if (((int)x != lastX) && ((int)y != lastY)) {
				lastX = (int)x;
				lastY = (int)y;
				if (seedGenerator->update(Vector2(lastX, lastY))) {
					points.push_back(Vector3(lastX, lastY, 0));
				}
				else {
					outside = true;
				}
			}
		}
		if (points.size() > 0) {
			lines.push_back(points);
		}
	}
}