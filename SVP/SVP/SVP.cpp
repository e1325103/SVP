#include "SVP.h"

SVP::SVP(QWidget *parent)
	: QMainWindow(parent)
{

	ui = new Ui::SVPClass();
	ui->setupUi(this);

	connect(ui->buttonRedraw, SIGNAL(clicked()), this, SLOT(buttonRedrawClicked()));

	integrator = new RungeKuttaIntegrator(nullptr, nullptr, 0, 0, 0, false);



	//ui->drawLabel

}

void SVP::buttonRedrawClicked() {
	integrator->simulate();
	ui->drawLabel->setPixmap(integrator->paint());
}