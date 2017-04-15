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
#include <QtWidgets/QTextEdit>
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
    QWidget *widget_2;
    QVBoxLayout *verticalLayout_4;
    QWidget *widget_11;
    QVBoxLayout *verticalLayout_15;
    QGroupBox *groupIntegration;
    QVBoxLayout *verticalLayout_10;
    QWidget *widget_3;
    QVBoxLayout *verticalLayout_5;
    QRadioButton *radioEuler;
    QWidget *widget_8;
    QHBoxLayout *horizontalLayout_3;
    QRadioButton *radioRunge;
    QWidget *widget_9;
    QHBoxLayout *horizontalLayout_4;
    QTextEdit *textSteps;
    QWidget *widget_10;
    QHBoxLayout *horizontalLayout_5;
    QTextEdit *textDelta;
    QWidget *widget_14;
    QVBoxLayout *verticalLayout_12;
    QGroupBox *groupEven;
    QVBoxLayout *verticalLayout_14;
    QWidget *widget_16;
    QVBoxLayout *verticalLayout_13;
    QRadioButton *radioRandom;
    QWidget *widget_18;
    QHBoxLayout *horizontalLayout_8;
    QTextEdit *textNumberRandom;
    QWidget *widget_17;
    QHBoxLayout *horizontalLayout_7;
    QRadioButton *radioEven;
    QWidget *widget_19;
    QHBoxLayout *horizontalLayout_9;
    QTextEdit *textDistance;
    QWidget *widget_15;
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
    QPushButton *buttonLic;
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

        widget_2 = new QWidget(centralWidget);
        widget_2->setObjectName(QStringLiteral("widget_2"));
        widget_2->setMaximumSize(QSize(200, 16777215));
        verticalLayout_4 = new QVBoxLayout(widget_2);
        verticalLayout_4->setSpacing(6);
        verticalLayout_4->setContentsMargins(11, 11, 11, 11);
        verticalLayout_4->setObjectName(QStringLiteral("verticalLayout_4"));
        widget_11 = new QWidget(widget_2);
        widget_11->setObjectName(QStringLiteral("widget_11"));
        widget_11->setMaximumSize(QSize(16777215, 225));
        verticalLayout_15 = new QVBoxLayout(widget_11);
        verticalLayout_15->setSpacing(6);
        verticalLayout_15->setContentsMargins(11, 11, 11, 11);
        verticalLayout_15->setObjectName(QStringLiteral("verticalLayout_15"));
        groupIntegration = new QGroupBox(widget_11);
        groupIntegration->setObjectName(QStringLiteral("groupIntegration"));
        groupIntegration->setMaximumSize(QSize(16777215, 210));
        groupIntegration->setCheckable(false);
        groupIntegration->setChecked(false);
        verticalLayout_10 = new QVBoxLayout(groupIntegration);
        verticalLayout_10->setSpacing(6);
        verticalLayout_10->setContentsMargins(11, 11, 11, 11);
        verticalLayout_10->setObjectName(QStringLiteral("verticalLayout_10"));
        widget_3 = new QWidget(groupIntegration);
        widget_3->setObjectName(QStringLiteral("widget_3"));
        widget_3->setMaximumSize(QSize(16777215, 40));
        verticalLayout_5 = new QVBoxLayout(widget_3);
        verticalLayout_5->setSpacing(6);
        verticalLayout_5->setContentsMargins(11, 11, 11, 11);
        verticalLayout_5->setObjectName(QStringLiteral("verticalLayout_5"));
        radioEuler = new QRadioButton(widget_3);
        radioEuler->setObjectName(QStringLiteral("radioEuler"));
        radioEuler->setChecked(true);

        verticalLayout_5->addWidget(radioEuler);


        verticalLayout_10->addWidget(widget_3);

        widget_8 = new QWidget(groupIntegration);
        widget_8->setObjectName(QStringLiteral("widget_8"));
        widget_8->setMaximumSize(QSize(16777215, 40));
        horizontalLayout_3 = new QHBoxLayout(widget_8);
        horizontalLayout_3->setSpacing(6);
        horizontalLayout_3->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_3->setObjectName(QStringLiteral("horizontalLayout_3"));
        radioRunge = new QRadioButton(widget_8);
        radioRunge->setObjectName(QStringLiteral("radioRunge"));

        horizontalLayout_3->addWidget(radioRunge);


        verticalLayout_10->addWidget(widget_8);

        widget_9 = new QWidget(groupIntegration);
        widget_9->setObjectName(QStringLiteral("widget_9"));
        widget_9->setMaximumSize(QSize(16777215, 42));
        horizontalLayout_4 = new QHBoxLayout(widget_9);
        horizontalLayout_4->setSpacing(6);
        horizontalLayout_4->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_4->setObjectName(QStringLiteral("horizontalLayout_4"));
        textSteps = new QTextEdit(widget_9);
        textSteps->setObjectName(QStringLiteral("textSteps"));
        textSteps->setMaximumSize(QSize(100, 30));
        textSteps->setInputMethodHints(Qt::ImhDigitsOnly);

        horizontalLayout_4->addWidget(textSteps);


        verticalLayout_10->addWidget(widget_9);

        widget_10 = new QWidget(groupIntegration);
        widget_10->setObjectName(QStringLiteral("widget_10"));
        widget_10->setMaximumSize(QSize(16777215, 42));
        horizontalLayout_5 = new QHBoxLayout(widget_10);
        horizontalLayout_5->setSpacing(6);
        horizontalLayout_5->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_5->setObjectName(QStringLiteral("horizontalLayout_5"));
        textDelta = new QTextEdit(widget_10);
        textDelta->setObjectName(QStringLiteral("textDelta"));
        textDelta->setMaximumSize(QSize(100, 30));

        horizontalLayout_5->addWidget(textDelta);


        verticalLayout_10->addWidget(widget_10);


        verticalLayout_15->addWidget(groupIntegration);


        verticalLayout_4->addWidget(widget_11);

        widget_14 = new QWidget(widget_2);
        widget_14->setObjectName(QStringLiteral("widget_14"));
        widget_14->setMaximumSize(QSize(16777215, 250));
        verticalLayout_12 = new QVBoxLayout(widget_14);
        verticalLayout_12->setSpacing(6);
        verticalLayout_12->setContentsMargins(11, 11, 11, 11);
        verticalLayout_12->setObjectName(QStringLiteral("verticalLayout_12"));
        groupEven = new QGroupBox(widget_14);
        groupEven->setObjectName(QStringLiteral("groupEven"));
        groupEven->setMaximumSize(QSize(16777215, 16777215));
        verticalLayout_14 = new QVBoxLayout(groupEven);
        verticalLayout_14->setSpacing(6);
        verticalLayout_14->setContentsMargins(11, 11, 11, 11);
        verticalLayout_14->setObjectName(QStringLiteral("verticalLayout_14"));
        widget_16 = new QWidget(groupEven);
        widget_16->setObjectName(QStringLiteral("widget_16"));
        widget_16->setMaximumSize(QSize(16777215, 40));
        verticalLayout_13 = new QVBoxLayout(widget_16);
        verticalLayout_13->setSpacing(6);
        verticalLayout_13->setContentsMargins(11, 11, 11, 11);
        verticalLayout_13->setObjectName(QStringLiteral("verticalLayout_13"));
        radioRandom = new QRadioButton(widget_16);
        radioRandom->setObjectName(QStringLiteral("radioRandom"));
        radioRandom->setChecked(true);

        verticalLayout_13->addWidget(radioRandom);


        verticalLayout_14->addWidget(widget_16);

        widget_18 = new QWidget(groupEven);
        widget_18->setObjectName(QStringLiteral("widget_18"));
        widget_18->setMaximumSize(QSize(16777215, 42));
        horizontalLayout_8 = new QHBoxLayout(widget_18);
        horizontalLayout_8->setSpacing(6);
        horizontalLayout_8->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_8->setObjectName(QStringLiteral("horizontalLayout_8"));
        textNumberRandom = new QTextEdit(widget_18);
        textNumberRandom->setObjectName(QStringLiteral("textNumberRandom"));
        textNumberRandom->setMaximumSize(QSize(100, 30));
        textNumberRandom->setInputMethodHints(Qt::ImhDigitsOnly);

        horizontalLayout_8->addWidget(textNumberRandom);


        verticalLayout_14->addWidget(widget_18);

        widget_17 = new QWidget(groupEven);
        widget_17->setObjectName(QStringLiteral("widget_17"));
        widget_17->setMaximumSize(QSize(16777215, 40));
        horizontalLayout_7 = new QHBoxLayout(widget_17);
        horizontalLayout_7->setSpacing(6);
        horizontalLayout_7->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_7->setObjectName(QStringLiteral("horizontalLayout_7"));
        radioEven = new QRadioButton(widget_17);
        radioEven->setObjectName(QStringLiteral("radioEven"));
        radioEven->setEnabled(true);

        horizontalLayout_7->addWidget(radioEven);


        verticalLayout_14->addWidget(widget_17);

        widget_19 = new QWidget(groupEven);
        widget_19->setObjectName(QStringLiteral("widget_19"));
        widget_19->setMaximumSize(QSize(16777215, 42));
        horizontalLayout_9 = new QHBoxLayout(widget_19);
        horizontalLayout_9->setSpacing(6);
        horizontalLayout_9->setContentsMargins(11, 11, 11, 11);
        horizontalLayout_9->setObjectName(QStringLiteral("horizontalLayout_9"));
        textDistance = new QTextEdit(widget_19);
        textDistance->setObjectName(QStringLiteral("textDistance"));
        textDistance->setEnabled(false);
        textDistance->setMaximumSize(QSize(100, 30));
        textDistance->setInputMethodHints(Qt::ImhDigitsOnly);

        horizontalLayout_9->addWidget(textDistance);


        verticalLayout_14->addWidget(widget_19);


        verticalLayout_12->addWidget(groupEven);


        verticalLayout_4->addWidget(widget_14);

        widget_15 = new QWidget(widget_2);
        widget_15->setObjectName(QStringLiteral("widget_15"));

        verticalLayout_4->addWidget(widget_15);


        horizontalLayout->addWidget(widget_2);

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
        buttonLic = new QPushButton(widget_7);
        buttonLic->setObjectName(QStringLiteral("buttonLic"));

        verticalLayout_9->addWidget(buttonLic);


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

        buttonLic->setDefault(false);


        QMetaObject::connectSlotsByName(SVPClass);
    } // setupUi

    void retranslateUi(QMainWindow *SVPClass)
    {
        SVPClass->setWindowTitle(QApplication::translate("SVPClass", "SVP", Q_NULLPTR));
        drawLabel->setText(QString());
        groupIntegration->setTitle(QApplication::translate("SVPClass", "Integration", Q_NULLPTR));
        radioEuler->setText(QApplication::translate("SVPClass", "Euler Integration", Q_NULLPTR));
        radioRunge->setText(QApplication::translate("SVPClass", "Runge Kutta", Q_NULLPTR));
        textSteps->setHtml(QApplication::translate("SVPClass", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0//EN\" \"http://www.w3.org/TR/REC-html40/strict.dtd\">\n"
"<html><head><meta name=\"qrichtext\" content=\"1\" /><style type=\"text/css\">\n"
"p, li { white-space: pre-wrap; }\n"
"</style></head><body style=\" font-family:'MS Shell Dlg 2'; font-size:8.25pt; font-weight:400; font-style:normal;\">\n"
"<p style=\"-qt-paragraph-type:empty; margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><br /></p></body></html>", Q_NULLPTR));
        textSteps->setPlaceholderText(QApplication::translate("SVPClass", "Anzahl Schritte", Q_NULLPTR));
        textDelta->setPlaceholderText(QApplication::translate("SVPClass", "Delta", Q_NULLPTR));
        groupEven->setTitle(QApplication::translate("SVPClass", "Starpunktverteilung", Q_NULLPTR));
        radioRandom->setText(QApplication::translate("SVPClass", "Zufallsgeneriert", Q_NULLPTR));
        textNumberRandom->setHtml(QApplication::translate("SVPClass", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0//EN\" \"http://www.w3.org/TR/REC-html40/strict.dtd\">\n"
"<html><head><meta name=\"qrichtext\" content=\"1\" /><style type=\"text/css\">\n"
"p, li { white-space: pre-wrap; }\n"
"</style></head><body style=\" font-family:'MS Shell Dlg 2'; font-size:8.25pt; font-weight:400; font-style:normal;\">\n"
"<p style=\"-qt-paragraph-type:empty; margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><br /></p></body></html>", Q_NULLPTR));
        textNumberRandom->setPlaceholderText(QApplication::translate("SVPClass", "Anzahl Saatpunkte", Q_NULLPTR));
        radioEven->setText(QApplication::translate("SVPClass", "Gleichm\303\244\303\237ig Verteilt", Q_NULLPTR));
        textDistance->setHtml(QApplication::translate("SVPClass", "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0//EN\" \"http://www.w3.org/TR/REC-html40/strict.dtd\">\n"
"<html><head><meta name=\"qrichtext\" content=\"1\" /><style type=\"text/css\">\n"
"p, li { white-space: pre-wrap; }\n"
"</style></head><body style=\" font-family:'MS Shell Dlg 2'; font-size:8.25pt; font-weight:400; font-style:normal;\">\n"
"<p style=\"-qt-paragraph-type:empty; margin-top:0px; margin-bottom:0px; margin-left:0px; margin-right:0px; -qt-block-indent:0; text-indent:0px;\"><br /></p></body></html>", Q_NULLPTR));
        textDistance->setPlaceholderText(QApplication::translate("SVPClass", "Abstand", Q_NULLPTR));
        groupBoxColour->setTitle(QApplication::translate("SVPClass", "Farben", Q_NULLPTR));
        radioColourOne->setText(QApplication::translate("SVPClass", "Fabrkanal 1", Q_NULLPTR));
        radioColourTwo->setText(QApplication::translate("SVPClass", "Farbkanal 2", Q_NULLPTR));
        labelColourOne->setText(QApplication::translate("SVPClass", "Farbe 1", Q_NULLPTR));
        labelColourTwo->setText(QApplication::translate("SVPClass", "Farbe 2", Q_NULLPTR));
        checkArrows->setText(QApplication::translate("SVPClass", "Pfeile", Q_NULLPTR));
        buttonLic->setText(QApplication::translate("SVPClass", "Linear Intergral Convolution", Q_NULLPTR));
        buttonRedraw->setText(QApplication::translate("SVPClass", "Neu Zeichnen", Q_NULLPTR));
    } // retranslateUi

};

namespace Ui {
    class SVPClass: public Ui_SVPClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_SVP_H
