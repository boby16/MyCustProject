; CLW ファイルは MFC ClassWizard の情報を含んでいます。

[General Info]
Version=1
LastClass=CUSBTestDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "USBTest.h"

ClassCount=4
Class1=CUSBTestApp
Class2=CUSBTestDlg

ResourceCount=3
Resource2=IDR_MAINFRAME
Resource3=IDD_USBTEST_DIALOG

[CLS:CUSBTestApp]
Type=0
HeaderFile=USBTest.h
ImplementationFile=USBTest.cpp
Filter=N

[CLS:CUSBTestDlg]
Type=0
HeaderFile=USBTestDlg.h
ImplementationFile=USBTestDlg.cpp
Filter=D
BaseClass=CDialog
VirtualFilter=dWC



[DLG:IDD_USBTEST_DIALOG]
Type=1
Class=CUSBTestDlg
ControlCount=2
Control1=IDCANCEL,button,1342242816
Control2=IDC_PRINT,button,1342242816

