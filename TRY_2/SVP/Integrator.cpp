#include "Integrator.h"

QPixmap Integrator::paint() {


	//width - height

	QPixmap pix(500, 500);
	QPainter* painter = new QPainter(&pix);

	painter->setRenderHint(QPainter::Antialiasing);

	//painter->fillRect(0, 0, 500, 500, QBrush(QColor(255, 255, 0)));

	/*if (colour != 10) {

		if (colour == 2) {
			paintBackgroundParameter(painter, colour, Vector3(0, 0, 0), Vector3(255, 255, 255));
		}
		else {
			paintBackgroundParameter(painter, colour, Vector3(255, 0, 0), Vector3(0, 0, 255));
		}
	}*/

	Vector3 lineColour = Vector3(0, 0, 255);
	//int lineColour = 255;

	/*if (colour == 10) {
		lineColour = 0;
	}*/

	paintLines(painter, 3, lineColour);


	/*if (arrows) {
		paintArrows(painter, 20, vectorField->width(), vectorField->height(), lineColour);
	}*/

	delete painter;
	return pix;
}

//void Integrator::paintArrows(QPainter* painter, int distance, int width, int height, int lineColour) {
//
//	QPen pen;
//	pen.setColor(QColor(0, 0, 0));
//	pen.setWidth(2);
//
//	if (lineColour == 0) {
//		pen.setColor(QColor(255, 102, 0));
//	}
//	
//	
//	QVector2D vec1 = QVector2D(0, 0);
//	QVector2D vec2 = QVector2D(4, 12);
//	QVector2D vec3 = QVector2D(-4, 12);
//
//	Vector3 vecDirection;
//	QVector2D direction;
//
//	QMatrix rotationMatrix = QMatrix();
//
//
//	for (int x = distance; x < width; x += distance) {
//		for (int y = distance; y < height; y += distance) {
//			painter->setPen(pen);
//
//			
//			vecDirection = interpolateBilinear(x, y);
//			
//
//
//			Vector2 up = Vector2(0, 1);
//			Vector3 vecTemp = interpolateBilinear(x, y);
//			vecTemp.normaliseXY();
//
//			Vector2 vec = Vector2(vecTemp.x(), vecTemp.y());		
//			
//
//			float angleRadians = atan2(up.cross(vec), up.dot(vec));// +3.1415926;
//
//			rotationMatrix = QMatrix(cos(angleRadians), -sin(angleRadians), sin(angleRadians), cos(angleRadians), 0, 0);
//
//			QVector2D currentVec1 = multiplyWithMatrix(rotationMatrix, vec1);
//			QVector2D currentVec2 = multiplyWithMatrix(rotationMatrix, vec2);
//			QVector2D currentVec3 = multiplyWithMatrix(rotationMatrix, vec3);
//
//
//			currentVec1 = currentVec1 * vecTemp.magnitudeXY() + QVector2D(x, y);
//			currentVec2 = currentVec2 * vecTemp.magnitudeXY() + QVector2D(x, y);
//			currentVec3 = currentVec3 * vecTemp.magnitudeXY() + QVector2D(x, y);
//			
//
//			painter->drawLine(currentVec1.x(), currentVec1.y(), currentVec2.x(), currentVec2.y());
//			painter->drawLine(currentVec2.x(), currentVec2.y(), currentVec3.x(), currentVec3.y());
//			painter->drawLine(currentVec3.x(), currentVec3.y(), currentVec1.x(), currentVec1.y());
//		}
//	}
//
//	
//}

QVector2D Integrator::multiplyWithMatrix(QMatrix matrix, QVector2D vector) {
	return QVector2D(matrix.m11() * vector.x() + matrix.m12() * vector.y(), matrix.m21() * vector.x() + matrix.m22() * vector.y());
}

void Integrator::paintLines(QPainter* painter, int width, Vector3 color) {
	QPen pen;
	pen.setColor(QColor((int)color.x(), (int)color.y(), (int)color.z()));
	pen.setWidth(width);

	for (auto lineIterator = lines.begin(); lineIterator != lines.end(); lineIterator++) {
		std::list<Vector2> points = *lineIterator;
		float i = 0.0f;
		if (points.size() > 1) {
			auto pointIterator = points.begin();
			Vector2 last = *pointIterator++;
			Vector2 current = *pointIterator++;
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

Vector2 Integrator::interpolateBilinear(float x, float y, int t) {
	float topX = ceilf(x);
	float topY = ceilf(y);
	float bottomX = floorf(x);
	float bottomY = floorf(y);
	float deltaX = fabsf(topX - x);
	float deltaY = fabsf(topY - y);
	Vector2 v1 = Vector2(vectorField->vector((int)topX, (int)topY, t));
	Vector2 v2 = Vector2(vectorField->vector((int)bottomX, (int)bottomY, t));
	Vector2 v3 = Vector2(vectorField->vector((int)topX, (int)bottomY, t));
	Vector2 v4 = Vector2(vectorField->vector((int)bottomX, (int)topY, t));
	Vector2 vTop = v3 * deltaX + v1 * (1.0f - deltaX);
	Vector2 vBottom = v4 * deltaX + v2 * (1.0f - deltaX);
	return  vBottom * deltaY + vTop * (1.0f - deltaY);
}
