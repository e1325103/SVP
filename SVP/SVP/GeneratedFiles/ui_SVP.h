/********************************************************************************
** Form generated from reading UI file 'SVP.ui'
**
** Created by: Qt User Interface Compiler version 5.8.0
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_SVP_H
#define UI_SVP_H

#include <QtCore/QVariant>
#include <QtWidgets/QAction>
#include <QtWidgets/QApplication>
#include <QtWidgets/QButtonGroup>
#include <QtWidgets/QCheckBox>
#include <QtWidgets/QGroupBox>
#include <QtWidgets/QHBoxLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QRadioButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QVBoxLayout>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_SVPClass
{
public:
    QWidget *centralWidget;
    QHBoxLayout *horizontalLayout_2;
    QHBoxLayout *horizontalLayout;
    QLabel *drawLabel;
    QWidget *widget_4;
    QVBoxLayout *verticalLayout_6;
    QWidget *widget_13;
    QVBoxLayout *verticalLayout_19;
    QGroupBox *groupBoxColour;
    QVBoxLayout *verticalLayout_18;
    QWidget *widget_23;
    QVBoxLayout *verticalLayout_17;
    QRadioButton *radioColourOne;
    QWidget *widget_24;
    QVBoxLayout *verticalLayout_16;
    QRadioButton *radioColourTwo;
    QWidget *widget_25;
    QHBoxLayout *horizontalLayout_6;
    QWidget *widgetColourOne;
    QLabel *labelColourOne;
    QWidget *widget_20;
    QHBoxLayout *horizontalLayout_10;
    QWidget *widgetColourTwo;
    QLabel *labelColourTwo;
    QWidget *widget_5;
    QVBoxLayout *verticalLayout_7;
    QCheckBox *checkArrows;
    QWidget *widget_6;
    QVBoxLayout *verticalLayout_8;
    QWidget *widget_7;
    QVBoxLayout *verticalLayout_9;
    QPushButton *buttonVar;
    QWidget *widget_21;
    QWidget *widget_12;
    QVBoxLayout *verticalLayout_11;
    QPushButton *buttonRedraw;
    QWidget *widget_27;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *SVPClass)
    {
        if (SVPClass->objectName().isEmpty())
            SVPClass->setObjectName(QStringLiteral("SVPClass"));
        SVPClass->resize(949, 711);
        centralWidget = new QWidget(SVPClass);
        centralWidget->setObjectName(QStringLiteral("centralWidget"));
        horizontalLayout_2 = new QHBoxLayout(centralWidget);
        horizontalLayout_2->setSpacing(6);
        horizontalLayout_2->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_2->setObjectName(QStringLiteral("horizontalLayout_2"));
        horizontalLayout = new QHBoxLayout();
        horizontalLayout->setSpacing(6);
        horizontalLayout->setObjectName(QStringLiteral("horizontalLayout"));
        drawLabel = new QLabel(centralWidget);
        drawLabel->setObjectName(QStringLiteral("drawLabel"));

        horizontalLayout->addWidget(drawLabel);

        widget_4 = new QWidget(centralWidget);
        widget_4->setObjectName(QStringLiteral("widget_4"));
        widget_4->setMaximumSize(QSize(200, 16777215));
        verticalLayout_6 = new QVBoxLayout(widget_4);
        verticalLayout_6->setSpacing(6);
        verticalLayout_6->setContentsMargins(11, 11, 11, 11);
        verticalLayout_6->setObjectName(QStringLiteral("verticalLayout_6"));
        widget_13 = new QWidget(widget_4);
        widget_13->setObjectName(QStringLiteral("widget_13"));
        widget_13->setMaximumSize(QSize(16777215, 210));
        verticalLayout_19 = new QVBoxLayout(widget_13);
        verticalLayout_19->setSpacing(6);
        verticalLayout_19->setContentsMargins(11, 11, 11, 11);
        verticalLayout_19->setObjectName(QStringLiteral("verticalLayout_19"));
        groupBoxColour = new QGroupBox(widget_13);
        groupBoxColour->setObjectName(QStringLiteral("groupBoxColour"));
        groupBoxColour->setCheckable(true);
        verticalLayout_18 = new QVBoxLayout(groupBoxColour);
        verticalLayout_18->setSpacing(6);
        verticalLayout_18->setContentsMargins(11, 11, 11, 11);
        verticalLayout_18->setObjectName(QStringLiteral("verticalLayout_18"));
        widget_23 = new QWidget(groupBoxColour);
        widget_23->setObjectName(QStringLiteral("widget_23"));
        verticalLayout_17 = new QVBoxLayout(widget_23);
        verticalLayout_17->setSpacing(6);
        verticalLayout_17->setContentsMargins(11, 11, 11, 11);
        verticalLayout_17->setObjectName(QStringLiteral("verticalLayout_17"));
        radioColourOne = new QRadioButton(widget_23);
        radioColourOne->setObjectName(QStringLiteral("radioColourOne"));
        radioColourOne->setChecked(true);

        verticalLayout_17->addWidget(radioColourOne);


        verticalLayout_18->addWidget(widget_23);

        widget_24 = new QWidget(groupBoxColour);
        widget_24->setObjectName(QStringLiteral("widget_24"));
        verticalLayout_16 = new QVBoxLayout(widget_24);
        verticalLayout_16->setSpacing(6);
        verticalLayout_16->setContentsMargins(11, 11, 11, 11);
        verticalLayout_16->setObjectName(QStringLiteral("verticalLayout_16"));
        radioColourTwo = new QRadioButton(widget_24);
        radioColourTwo->setObjectName(QStringLiteral("radioColourTwo"));

        verticalLayout_16->addWidget(radioColourTwo);


        verticalLayout_18->addWidget(widget_24);

        widget_25 = new QWidget(groupBoxColour);
        widget_25->setObjectName(QStringLiteral("widget_25"));
        horizontalLayout_6 = new QHBoxLayout(widget_25);
        horizontalLayout_6->setSpacing(6);
        horizontalLayout_6->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_6->setObjectName(QStringLiteral("horizontalLayout_6"));
        widgetColourOne = new QWidget(widget_25);
        widgetColourOne->setObjectName(QStringLiteral("widgetColourOne"));
        widgetColourOne->setMaximumSize(QSize(20, 20));
        QPalette palette;
        QBrush brush(QColor(0, 0, 0, 255));
        brush.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::WindowText, brush);
        QBrush brush1(QColor(255, 0, 0, 255));
        brush1.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::Button, brush1);
        QBrush brush2(QColor(255, 127, 127, 255));
        brush2.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::Light, brush2);
        QBrush brush3(QColor(255, 63, 63, 255));
        brush3.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::Midlight, brush3);
        QBrush brush4(QColor(127, 0, 0, 255));
        brush4.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::Dark, brush4);
        QBrush brush5(QColor(170, 0, 0, 255));
        brush5.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::Mid, brush5);
        palette.setBrush(QPalette::Active, QPalette::Text, brush);
        QBrush brush6(QColor(255, 255, 255, 255));
        brush6.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::BrightText, brush6);
        palette.setBrush(QPalette::Active, QPalette::ButtonText, brush);
        palette.setBrush(QPalette::Active, QPalette::Base, brush6);
        palette.setBrush(QPalette::Active, QPalette::Window, brush1);
        palette.setBrush(QPalette::Active, QPalette::Shadow, brush);
        palette.setBrush(QPalette::Active, QPalette::AlternateBase, brush2);
        QBrush brush7(QColor(255, 255, 220, 255));
        brush7.setStyle(Qt::SolidPattern);
        palette.setBrush(QPalette::Active, QPalette::ToolTipBase, brush7);
        palette.setBrush(QPalette::Active, QPalette::ToolTipText, brush);
        palette.setBrush(QPalette::Inactive, QPalette::WindowText, brush);
        palette.setBrush(QPalette::Inactive, QPalette::Button, brush1);
        palette.setBrush(QPalette::Inactive, QPalette::Light, brush2);
        palette.setBrush(QPalette::Inactive, QPalette::Midlight, brush3);
        palette.setBrush(QPalette::Inactive, QPalette::Dark, brush4);
        palette.setBrush(QPalette::Inactive, QPalette::Mid, brush5);
        palette.setBrush(QPalette::Inactive, QPalette::Text, brush);
        palette.setBrush(QPalette::Inactive, QPalette::BrightText, brush6);
        palette.setBrush(QPalette::Inactive, QPalette::ButtonText, brush);
        palette.setBrush(QPalette::Inactive, QPalette::Base, brush6);
        palette.setBrush(QPalette::Inactive, QPalette::Window, brush1);
        palette.setBrush(QPalette::Inactive, QPalette::Shadow, brush);
        palette.setBrush(QPalette::Inactive, QPalette::AlternateBase, brush2);
        palette.setBrush(QPalette::Inactive, QPalette::ToolTipBase, brush7);
        palette.setBrush(QPalette::Inactive, QPalette::ToolTipText, brush);
        palette.setBrush(QPalette::Disabled, QPalette::WindowText, brush4);
        palette.setBrush(QPalette::Disabled, QPalette::Button, brush1);
        palette.setBrush(QPalette::Disabled, QPalette::Light, brush2);
        palette.setBrush(QPalette::Disabled, QPalette::Midlight, brush3);
        palette.setBrush(QPalette::Disabled, QPalette::Dark, brush4);
        palette.setBrush(QPalette::Disabled, QPalette::Mid, brush5);
        palette.setBrush(QPalette::Disabled, QPalette::Text, brush4);
        palette.setBrush(QPalette::Disabled, QPalette::BrightText, brush6);
        palette.setBrush(QPalette::Disabled, QPalette::ButtonText, brush4);
        palette.setBrush(QPalette::Disabled, QPalette::Base, brush1);
        palette.setBrush(QPalette::Disabled, QPalette::Window, brush1);
        palette.setBrush(QPalette::Disabled, QPalette::Shadow, brush);
        palette.setBrush(QPalette::Disabled, QPalette::AlternateBase, brush1);
        palette.setBrush(QPalette::Disabled, QPalette::ToolTipBase, brush7);
        palette.setBrush(QPalette::Disabled, QPalette::ToolTipText, brush);
        widgetColourOne->setPalette(palette);
        widgetColourOne->setAutoFillBackground(true);

        horizontalLayout_6->addWidget(widgetColourOne);

        labelColourOne = new QLabel(widget_25);
        labelColourOne->setObjectName(QStringLiteral("labelColourOne"));

        horizontalLayout_6->addWidget(labelColourOne);


        verticalLayout_18->addWidget(widget_25);

        widget_20 = new QWidget(groupBoxColour);
        widget_20->setObjectName(QStringLiteral("widget_20"));
        horizontalLayout_10 = new QHBoxLayout(widget_20);
        horizontalLayout_10->setSpacing(6);
        horizontalLayout_10->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_10->setObjectName(QStringLiteral("horizontalLayout_10"));
        widgetColourTwo = new QWidget(widget_20);
        widgetColourTwo->setObjectName(QStringLiteral("widgetColourTwo"));
        widgetColourTwo->setMaximumSize(QSize(20, 20));
        QPalette palette1;
        palette1.setBrush(QPalette::Active, QPalette::WindowText, brush);
        QBrush brush8(QColor(0, 0, 255, 255));
        brush8.setStyle(Qt::SolidPattern);
        palette1.setBrush(QPalette::Active, QPalette::Button, brush8);
        QBrush brush9(QColor(127, 127, 255, 255));
        brush9.setStyle(Qt::SolidPattern);
        palette1.setBrush(QPalette::Active, QPalette::Light, brush9);
        QBrush brush10(QColor(63, 63, 255, 255));
        brush10.setStyle(Qt::SolidPattern);
        palette1.setBrush(QPalette::Active, QPalette::Midlight, brush10);
        QBrush brush11(QColor(0, 0, 127, 255));
        brush11.setStyle(Qt::SolidPattern);
        palette1.setBrush(QPalette::Active, QPalette::Dark, brush11);
        QBrush brush12(QColor(0, 0, 170, 255));
        brush12.setStyle(Qt::SolidPattern);
        palette1.setBrush(QPalette::Active, QPalette::Mid, brush12);
        palette1.setBrush(QPalette::Active, QPalette::Text, brush);
        palette1.setBrush(QPalette::Active, QPalette::BrightText, brush6);
        palette1.setBrush(QPalette::Active, QPalette::ButtonText, brush);
        palette1.setBrush(QPalette::Active, QPalette::Base, brush6);
        palette1.setBrush(QPalette::Active, QPalette::Window, brush8);
        palette1.setBrush(QPalette::Active, QPalette::Shadow, brush);
        palette1.setBrush(QPalette::Active, QPalette::AlternateBase, brush9);
        palette1.setBrush(QPalette::Active, QPalette::ToolTipBase, brush7);
        palette1.setBrush(QPalette::Active, QPalette::ToolTipText, brush);
        palette1.setBrush(QPalette::Inactive, QPalette::WindowText, brush);
        palette1.setBrush(QPalette::Inactive, QPalette::Button, brush8);
        palette1.setBrush(QPalette::Inactive, QPalette::Light, brush9);
        palette1.setBrush(QPalette::Inactive, QPalette::Midlight, brush10);
        palette1.setBrush(QPalette::Inactive, QPalette::Dark, brush11);
        palette1.setBrush(QPalette::Inactive, QPalette::Mid, brush12);
        palette1.setBrush(QPalette::Inactive, QPalette::Text, brush);
        palette1.setBrush(QPalette::Inactive, QPalette::BrightText, brush6);
        palette1.setBrush(QPalette::Inactive, QPalette::ButtonText, brush);
        palette1.setBrush(QPalette::Inactive, QPalette::Base, brush6);
        palette1.setBrush(QPalette::Inactive, QPalette::Window, brush8);
        palette1.setBrush(QPalette::Inactive, QPalette::Shadow, brush);
        palette1.setBrush(QPalette::Inactive, QPalette::AlternateBase, brush9);
        palette1.setBrush(QPalette::Inactive, QPalette::ToolTipBase, brush7);
        palette1.setBrush(QPalette::Inactive, QPalette::ToolTipText, brush);
        palette1.setBrush(QPalette::Disabled, QPalette::WindowText, brush11);
        palette1.setBrush(QPalette::Disabled, QPalette::Button, brush8);
        palette1.setBrush(QPalette::Disabled, QPalette::Light, brush9);
        palette1.setBrush(QPalette::Disabled, QPalette::Midlight, brush10);
        palette1.setBrush(QPalette::Disabled, QPalette::Dark, brush11);
        palette1.setBrush(QPalette::Disabled, QPalette::Mid, brush12);
        palette1.setBrush(QPalette::Disabled, QPalette::Text, brush11);
        palette1.setBrush(QPalette::Disabled, QPalette::BrightText, brush6);
        palette1.setBrush(QPalette::Disabled, QPalette::ButtonText, brush11);
        palette1.setBrush(QPalette::Disabled, QPalette::Base, brush8);
        palette1.setBrush(QPalette::Disabled, QPalette::Window, brush8);
        palette1.setBrush(QPalette::Disabled, QPalette::Shadow, brush);
        palette1.setBrush(QPalette::Disabled, QPalette::AlternateBase, brush8);
        palette1.setBrush(QPalette::Disabled, QPalette::ToolTipBase, brush7);
        palette1.setBrush(QPalette::Disabled, QPalette::ToolTipText, brush);
        widgetColourTwo->setPalette(palette1);
        widgetColourTwo->setAutoFillBackground(true);

        horizontalLayout_10->addWidget(widgetColourTwo);

        labelColourTwo = new QLabel(widget_20);
        labelColourTwo->setObjectName(QStringLiteral("labelColourTwo"));

        horizontalLayout_10->addWidget(labelColourTwo);


        verticalLayout_18->addWidget(widget_20);


        verticalLayout_19->addWidget(groupBoxColour);


        verticalLayout_6->addWidget(widget_13);

        widget_5 = new QWidget(widget_4);
        widget_5->setObjectName(QStringLiteral("widget_5"));
        widget_5->setMaximumSize(QSize(16777215, 40));
        verticalLayout_7 = new QVBoxLayout(widget_5);
        verticalLayout_7->setSpacing(6);
        verticalLayout_7->setContentsMargins(11, 11, 11, 11);
        verticalLayout_7->setObjectName(QStringLiteral("verticalLayout_7"));
        checkArrows = new QCheckBox(widget_5);
        checkArrows->setObjectName(QStringLiteral("checkArrows"));

        verticalLayout_7->addWidget(checkArrows);


        verticalLayout_6->addWidget(widget_5);

        widget_6 = new QWidget(widget_4);
        widget_6->setObjectName(QStringLiteral("widget_6"));
        widget_6->setMaximumSize(QSize(16777215, 40));
        verticalLayout_8 = new QVBoxLayout(widget_6);
        verticalLayout_8->setSpacing(6);
        verticalLayout_8->setContentsMargins(11, 11, 11, 11);
        verticalLayout_8->setObjectName(QStringLiteral("verticalLayout_8"));

        verticalLayout_6->addWidget(widget_6);

        widget_7 = new QWidget(widget_4);
        widget_7->setObjectName(QStringLiteral("widget_7"));
        widget_7->setMaximumSize(QSize(16777215, 40));
        verticalLayout_9 = new QVBoxLayout(widget_7);
        verticalLayout_9->setSpacing(6);
        verticalLayout_9->setContentsMargins(11, 11, 11, 11);
        verticalLayout_9->setObjectName(QStringLiteral("verticalLayout_9"));
        buttonVar = new QPushButton(widget_7);
        buttonVar->setObjectName(QStringLiteral("buttonVar"));

        verticalLayout_9->addWidget(buttonVar);


        verticalLayout_6->addWidget(widget_7);

        widget_21 = new QWidget(widget_4);
        widget_21->setObjectName(QStringLiteral("widget_21"));
        widget_21->setMaximumSize(QSize(16777215, 40));

        verticalLayout_6->addWidget(widget_21);

        widget_12 = new QWidget(widget_4);
        widget_12->setObjectName(QStringLiteral("widget_12"));
        widget_12->setMaximumSize(QSize(16777215, 40));
        verticalLayout_11 = new QVBoxLayout(widget_12);
        verticalLayout_11->setSpacing(6);
        verticalLayout_11->setContentsMargins(11, 11, 11, 11);
        verticalLayout_11->setObjectName(QStringLiteral("verticalLayout_11"));
        buttonRedraw = new QPushButton(widget_12);
        buttonRedraw->setObjectName(QStringLiteral("buttonRedraw"));

        verticalLayout_11->addWidget(buttonRedraw);


        verticalLayout_6->addWidget(widget_12);

        widget_27 = new QWidget(widget_4);
        widget_27->setObjectName(QStringLiteral("widget_27"));

        verticalLayout_6->addWidget(widget_27);


        horizontalLayout->addWidget(widget_4);


        horizontalLayout_2->addLayout(horizontalLayout);

        SVPClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(SVPClass);
        menuBar->setObjectName(QStringLiteral("menuBar"));
        menuBar->setGeometry(QRect(0, 0, 949, 21));
        SVPClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(SVPClass);
        mainToolBar->setObjectName(QStringLiteral("mainToolBar"));
        SVPClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(SVPClass);
        statusBar->setObjectName(QStringLiteral("statusBar"));
        SVPClass->setStatusBar(statusBar);

        retranslateUi(SVPClass);

        buttonVar->setDefault(false);


        QMetaObject::connectSlotsByName(SVPClass);
    } // setupUi

    void retranslateUi(QMainWindow *SVPClass)
    {
        SVPClass->setWindowTitle(QApplication::translate("SVPClass", "SVP", Q_NULLPTR));
        drawLabel->setText(QString());
        groupBoxColour->setTitle(QApplication::translate("SVPClass", "Farben", Q_NULLPTR));
        radioColourOne->setText(QApplication::translate("SVPClass", "Fabrkanal 1", Q_NULLPTR));
        radioColourTwo->setText(QApplication::translate("SVPClass", "Farbkanal 2", Q_NULLPTR));
        labelColourOne->setText(QApplication::translate("SVPClass", "Farbe 1", Q_NULLPTR));
        labelColourTwo->setText(QApplication::translate("SVPClass", "Farbe 2", Q_NULLPTR));
        checkArrows->setText(QApplication::translate("SVPClass", "Pfeile", Q_NULLPTR));
        buttonVar->setText(QApplication::translate("SVPClass", "Plot Variability", Q_NULLPTR));
        buttonRedraw->setText(QApplication::translate("SVPClass", "Draw Lines", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class SVPClass: public Ui_SVPClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SVP_H
