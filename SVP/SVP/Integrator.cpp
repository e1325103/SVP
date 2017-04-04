#include "Integrator.h"

QPixmap Integrator::paint() {
	QPixmap pix(vectorField->width(), vectorField->height());
	QPainter* painter = new QPainter(&pix);

	painter->setRenderHint(QPainter::Antialiasing);

	painter->fillRect(0, 0, vectorField->width(), vectorField->height(), QBrush(QColor(255, 255, 255)));

	if (colour != 10) {

		if (colour == 2) {
			paintBackgroundParameter(painter, colour, Vector3(0, 0, 0), Vector3(255, 255, 255));
		}
		else {
			paintBackgroundParameter(painter, colour, Vector3(255, 0, 0), Vector3(0, 0, 255));
		}
	}


	int lineColour = 255;

	if (colour == 10) {
		lineColour = 0;
	}

	paintLines(painter, 3, Vector3(lineColour, lineColour, lineColour));


	if (arrows) {
		paintArrows(painter, 20, vectorField->width(), vectorField->height(), lineColour);
	}

	delete painter;
	return pix;
}

void Integrator::paintArrows(QPainter* painter, int distance, int width, int height, int lineColour) {

	QPen pen;
	pen.setColor(QColor(0, 0, 0));
	pen.setWidth(2);

	if (lineColour == 0) {
		pen.setColor(QColor(255, 102, 0));
	}
	
	
	QVector2D vec1 = QVector2D(0, 0);
	QVector2D vec2 = QVector2D(4, 12);
	QVector2D vec3 = QVector2D(-4, 12);

	Vector3 vecDirection;
	QVector2D direction;

	QMatrix rotationMatrix = QMatrix();


	for (int x = distance; x < width; x += distance) {
		for (int y = distance; y < height; y += distance) {
			painter->setPen(pen);

			
			vecDirection = interpolateBilinear(x, y);
			


			Vector2 up = Vector2(0, 1);
			Vector3 vecTemp = interpolateBilinear(x, y);
			vecTemp.normaliseXY();

			Vector2 vec = Vector2(vecTemp.x(), vecTemp.y());		
			

			float angleRadians = atan2(up.cross(vec), up.dot(vec));// +3.1415926;

			rotationMatrix = QMatrix(cos(angleRadians), -sin(angleRadians), sin(angleRadians), cos(angleRadians), 0, 0);

			QVector2D currentVec1 = multiplyWithMatrix(rotationMatrix, vec1);
			QVector2D currentVec2 = multiplyWithMatrix(rotationMatrix, vec2);
			QVector2D currentVec3 = multiplyWithMatrix(rotationMatrix, vec3);


			currentVec1 = currentVec1 * vecTemp.magnitudeXY() + QVector2D(x, y);
			currentVec2 = currentVec2 * vecTemp.magnitudeXY() + QVector2D(x, y);
			currentVec3 = currentVec3 * vecTemp.magnitudeXY() + QVector2D(x, y);
			

			painter->drawLine(currentVec1.x(), currentVec1.y(), currentVec2.x(), currentVec2.y());
			painter->drawLine(currentVec2.x(), currentVec2.y(), currentVec3.x(), currentVec3.y());
			painter->drawLine(currentVec3.x(), currentVec3.y(), currentVec1.x(), currentVec1.y());
		}
	}

	
}

QVector2D Integrator::multiplyWithMatrix(QMatrix matrix, QVector2D vector) {
	return QVector2D(matrix.m11() * vector.x() + matrix.m12() * vector.y(), matrix.m21() * vector.x() + matrix.m22() * vector.y());
}

void Integrator::paintLines(QPainter* painter, int width, Vector3 color) {
	QPen pen;
	pen.setColor(QColor((int)color.x(), (int)color.y(), (int)color.z()));
	pen.setWidth(width);

	for (auto lineIterator = lines.begin(); lineIterator != lines.end(); lineIterator++) {
		std::list<Vector3> points = *lineIterator;
		float i = 0.0f;
		if (points.size() > 1) {
			auto pointIterator = points.begin();
			Vector3 last = *pointIterator++;
			Vector3 current = *pointIterator++;
			while (pointIterator != points.end()) {
				painter->setPen(pen);
				painter->drawLine(last.x(), last.y(), current.x(), current.y());
				last = current;
				current = *pointIterator++;
				i++;
			}
			painter->drawLine(last.x(), last.y(), current.x(), current.y());
		}
	}
}

void Integrator::paintBackgroundParameter(QPainter* painter, int parameter, Vector3 color1, Vector3 color2) {
	maxParameter = std::numeric_limits<float>::min();
	minParameter = std::numeric_limits<float>::max();

	for (int x = 0; x < vectorField->width(); x++) {
		for (int y = 0; y < vectorField->height(); y++) {
			float parameterValue = vectorField->parameter(x, y)[parameter];
			if (parameterValue > maxParameter) {
				maxParameter = parameterValue;
			}
			if (parameterValue < minParameter) {
				minParameter = parameterValue;
			}
		}
	}

	QPen pen;
	pen.setWidth(1);
	for (int x = 0; x < vectorField->width(); x++) {
		for (int y = 0; y < vectorField->height(); y++) {
			float val = interpolateBilinear(x, y, parameter).z();
			val = (val - minParameter) * (1.0f / (maxParameter - minParameter));
			Vector3 mixColor = color1 * val + color2 * (1.0f - val);
			pen.setColor(QColor((int)mixColor.x(), (int)mixColor.y(), (int)mixColor.z()));
			painter->setPen(pen);
			painter->drawPoint(QPoint(x, y));			
		}
	}
}

Vector3 Integrator::interpolateBilinear(float x, float y) {
	return interpolateBilinear(x, y, 0);
}

Vector3 Integrator::interpolateBilinear(float x, float y, int parameter) {
	float topX = ceilf(x);
	float topY = ceilf(y);
	float bottomX = floorf(x);
	float bottomY = floorf(y);
	float deltaX = fabsf(topX - x);
	float deltaY = fabsf(topY - y);
	Vector3 v1 = Vector3(vectorField->vector((int)topX, (int)topY), vectorField->parameter((int)topX, (int)topY)[parameter]);
	Vector3 v2 = Vector3(vectorField->vector((int)bottomX, (int)bottomY), vectorField->parameter((int)bottomX, (int)bottomY)[parameter]);
	Vector3 v3 = Vector3(vectorField->vector((int)topX, (int)bottomY), vectorField->parameter((int)topX, (int)bottomY)[parameter]);
	Vector3 v4 = Vector3(vectorField->vector((int)bottomX, (int)topY), vectorField->parameter((int)bottomX, (int)topY)[parameter]);
	Vector3 vTop = v3 * deltaX + v1 * (1.0f - deltaX);
	Vector3 vBottom = v4 * deltaX + v2 * (1.0f - deltaX);
	return  vBottom * deltaY + vTop * (1.0f - deltaY);
}
