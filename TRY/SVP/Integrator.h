#pragma once
#include "SeedGenerator.h"
#include "VectorField.h"
#include "Vector.h"

#include <math.h>
#include <cfloat>
#include <QPainter>
#include <QPaintEvent>
#include <QVector>

class Integrator
{
public:
	explicit
		Integrator(VectorField* _vectorField, SeedGenerator* _seedGenerator, float _delta, int _steps, int _colour, bool _arrows) :
		vectorField(_vectorField), seedGenerator(_seedGenerator), delta(_delta), steps(_steps), colour(_colour), arrows(_arrows) { }
	~Integrator();
	virtual void simulate() = 0;
	QPixmap paint();
	Vector3 interpolateBilinear(float x, float y);
	Vector3 interpolateBilinear(float x, float y, int parameter);

	VectorField* vectorField;
	float minParameter;
	float maxParameter;
protected:
	SeedGenerator* seedGenerator;
	float delta;
	int colour;
	bool arrows;
	int steps;
	std::list<std::list<Vector3>> lines;

	void paintBackgroundParameter(QPainter* painter, int parameter, Vector3 color1, Vector3 color2);
	void paintLines(QPainter* painter, int width, Vector3 color);
	void paintArrows(QPainter* painter, int distance, int width, int height, int lineColour);

private:
	QVector2D multiplyWithMatrix(QMatrix matrix, QVector2D vector);
};