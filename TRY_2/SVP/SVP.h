#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_SVP.h"
#include "Integrator.h"
#include "VectorField.h"
#include "RungeKuttaIntegrator.h"
#include "engine.h"
#include "matrix.h"
#include <string>

class SVP : public QMainWindow
{
	Q_OBJECT

public:
	SVP(QWidget *parent = Q_NULLPTR);

protected slots:
	void buttonRedrawClicked();
	void buttonVarClicked();

private:
	Ui::SVPClass* ui;
	Ui_SVPClass asdf;

	Integrator* integrator;
	VectorField* vectorField;
	QPixmap currentPix;
	bool redraw;
};
