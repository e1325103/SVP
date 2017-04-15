#include "SVP.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	SVP w;
	w.show();
	return a.exec();
}
