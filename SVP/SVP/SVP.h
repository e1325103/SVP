#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_SVP.h"
#include "Integrator.h"
#include "VectorField.h"

class SVP : public QMainWindow
{
	Q_OBJECT

public:
	SVP(QWidget *parent = Q_NULLPTR);
	void buttonRedrawClicked();

private:
	Ui::SVPClass* ui;

	Integrator* integrator;
	VectorField* vectorField;
	QPixmap currentPix;
	bool redraw;
};
