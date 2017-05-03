#include "SVP.h"

SVP::SVP(QWidget *parent)
	: QMainWindow(parent)
{

	ui = new Ui::SVPClass();
	ui->setupUi(this);

	connect(ui->buttonRedraw, SIGNAL(clicked()), this, SLOT(buttonRedrawClicked()));
	connect(ui->buttonVar, SIGNAL(clicked()), this, SLOT(buttonVarClicked()));

	integrator = new RungeKuttaIntegrator(nullptr, nullptr, 0, 0, 0, false);



	//ui->drawLabel

}

void SVP::buttonRedrawClicked() {
	integrator->simulate();
	ui->drawLabel->setPixmap(integrator->paint());
}

void SVP::buttonVarClicked() {
	ui->buttonVar->setText("Hallo =D");

	mxArray* A = mxCreateDoubleMatrix(1000, 10, mxREAL);

	int index = 1;


	for (auto lineIterator = integrator->lines.begin(); lineIterator != integrator->lines.end(); lineIterator++) {
		std::list<Vector2> points = *lineIterator;
		float i = 0.0f;
		if (points.size() > 1) {
			auto pointIterator = points.begin();
			Vector2 current = *pointIterator++;
			//Vector2 current = *pointIterator++;
			while (pointIterator != points.end()) {

				mxSetCell(A, index, mxCreateDoubleScalar(current.x()));
				mxSetCell(A, index + 500, mxCreateDoubleScalar(current.y()));
				index++;
				current = *pointIterator++;

				std::cout << std::to_string(current.x()) << ' ' << std::to_string(current.y()) << std::endl;
			}
		}

		index += 500;
	}


	Engine *ep;

	if (!(ep = engOpen(""))) {
		fprintf(stderr, "\nCan't start MATLAB engine\n");
	}


	engEvalString(ep, "addpath('D:\\TU\\Master\\Semester 1\\Visualisierung 2\\Visualisierung_2\\Matlab')");

	engPutVariable(ep, "Z", A);

	engEvalString(ep, "pca");
	//engEvalString(ep, "figure;");
}