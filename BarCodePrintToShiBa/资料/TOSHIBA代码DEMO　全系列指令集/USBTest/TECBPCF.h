#include <windows.h>
#include <stdio.h>
#include <TCHAR.H>

#ifndef __TECBPCL__
#define __TECBPCL__

//TECUSB
INT __stdcall GetDevice(LPGUID, LPTSTR, INT, LPTSTR, LPTSTR);
INT __stdcall GetUSBDevice(LPTSTR, INT, LPTSTR, LPTSTR);

INT GetDeviceName(LPGUID, LPTSTR, INT, LPTSTR, LPTSTR);
INT GetDeviceCheck(LPGUID, LPTSTR);

#endif