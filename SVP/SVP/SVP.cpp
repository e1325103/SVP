#include "SVP.h"

SVP::SVP(QWidget *parent)
	: QMainWindow(parent)
{

	ui = new Ui::SVPClass();
	ui->setupUi(this);

	connect(ui->buttonRedraw, SIGNAL(clicked()), this, SLOT(buttonRedrawClicked()));

}

void SVP::buttonRedrawClicked() {
	ui->drawLabel->setPixmap(integrator->paint());
}