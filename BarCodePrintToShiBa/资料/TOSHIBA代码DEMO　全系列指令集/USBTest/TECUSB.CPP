#include "StdAfx.h"
#include <setupapi.h>
#include "Usb100.h"  
#include "BulkUsb.h"
#include "TECBPCF.h"

static const GUID GUID_CLASS_MY_BULK = 
{0xa5dcbf10, 0x6530, 0x11d2, {0x90, 0x1f, 0x00, 0xc0, 0x4f, 0xb9, 0x51, 0xed} };
//{a5dcbf10-6530-11d2-901f-00c04fb951ed}

//
//	GetDevice：DOSデバイス（LPP,COM）のGUIDからデバイス名を取得
//	注：プリンタドライバのポート指定によらない運用を目指した処理だが、現状使用可能なGUIDがない
//
//	GetDevice：GUIDで検出可能なデバイス名をインデックス番号で指定して取得
//	RETURN：iOrderと同じ値の場合は取得、iOrderより小さい場合は検出数、-1の場合はパラメータエラー、iOrderが0の場合はデバイス数
//	IN	pGuid：GUID
//	IN	pType：
//	IN	iOrder：インデックス番号（検出順）
//  IN  pSerialNumber:
//	OUT	pDeviceName：デバイス名
//
INT __stdcall GetDevice(
	LPGUID		pGuid,
	LPTSTR		pType,
	INT			iOrder,
	LPTSTR		pSerialNumber,
	LPTSTR		pDeviceName
	)
{
	INT		iNum = 0;

	if( iOrder == 0 )
		iNum = GetDeviceCheck(pGuid, pType);
	else
		iNum = GetDeviceName(pGuid, pType, iOrder, pSerialNumber, pDeviceName);

	return iNum;
}

//
//	GetUSBDevice：MSのUSB印刷サポートのGUIDで検出可能なデバイス名をインデックス番号で指定して取得
//	RETURN：iOrderと同じ値の場合は取得数、iOrderより小さい場合は検出数、-1の場合はパラメータエラー、iOrderが0の場合はデバイス数
//	IN	pType：検出対象となるデバイスの種別
//	IN	iOrder：インデックス番号（検出順）
//  IN  pSerialNumber:検出するデバイスのシリアル番号
//	OUT	pDeviceName：デバイス名
//
INT __stdcall GetUSBDevice(
	LPTSTR		pType,
	INT			iOrder,
	LPTSTR		pSerialNumber,
	LPTSTR		pDeviceName
	)
{
	INT		iNum = 0;
	LPGUID	pGUID;

	pGUID  = (LPGUID)&GUID_CLASS_MY_BULK;

	iNum = GetDevice(pGUID, pType, iOrder, pSerialNumber, pDeviceName);

	return iNum;
}

//
//	GetDeviceName：GUIDで検出可能なデバイス名をインデックス番号で指定して取得
//	RETURN：iOrderと同じ値の場合は取得、iOrderより小さい場合は検出数、-1の場合はパラメータエラー
//	IN	pGuid：GUID
//	IN	pType：検出対象となるデバイスの種別
//	IN	iOrder：インデックス番号（検出順）
//  IN  pSerialNumber:検出するデバイスのシリアル番号
//	OUT	pDeviceName：デバイス名
//
INT GetDeviceName(
	LPGUID		pGuid,
	LPTSTR		pType,
	INT			iOrder,
	LPTSTR		pSerialNumber,
	LPTSTR		pDeviceName
	)
{
	INT			iNum, iRes;
	ULONG		i, NumberDevices;
	BOOLEAN		done;
	HDEVINFO	hardwareDeviceInfo;
	SP_INTERFACE_DEVICE_DATA deviceInfoData;
	TCHAR		pPortName[256];

	iNum = 0;
	iRes = 0;
	NumberDevices = 4;
	done = FALSE;
	deviceInfoData.cbSize = sizeof(SP_INTERFACE_DEVICE_DATA);

	hardwareDeviceInfo = SetupDiGetClassDevs(pGuid, NULL, NULL, (DIGCF_PRESENT | DIGCF_DEVICEINTERFACE));

	if( hardwareDeviceInfo == 0 || iOrder < 0 || pDeviceName == NULL )
		return -1;

	if( pType != NULL && _tcscmp(pType, "") != 0 )
	{
		if( _tcscmp(pType, "TEC") == 0 )
			_tcscpy(pPortName, "vid_08a6");
		else
		{
			if( pSerialNumber != NULL && _tcscmp(pSerialNumber, "") != 0 )
				_stprintf(pPortName, "%s#%s", pType, pSerialNumber);
			else
				_tcscpy(pPortName, pType);
		}
	}
	else
		_tcscpy(pPortName, "");

	while( !done )
	{
		NumberDevices *= 2;

		for( i = 0; i < NumberDevices; i++ )
		{
			if( SetupDiEnumDeviceInterfaces(hardwareDeviceInfo, 0, pGuid, i, &deviceInfoData) )
			{
				PSP_INTERFACE_DEVICE_DETAIL_DATA	functionClassDeviceData = NULL;
				ULONG								predictedLength = 0;
				ULONG								requiredLength = 0;

				SetupDiGetDeviceInterfaceDetail(hardwareDeviceInfo, &deviceInfoData, NULL, 0, &requiredLength, NULL);

				predictedLength = requiredLength;

				functionClassDeviceData = (PSP_INTERFACE_DEVICE_DETAIL_DATA)malloc(predictedLength);
				functionClassDeviceData->cbSize = sizeof(SP_INTERFACE_DEVICE_DETAIL_DATA);

				if( SetupDiGetInterfaceDeviceDetail(hardwareDeviceInfo,
														&deviceInfoData,
														functionClassDeviceData,
														predictedLength,
														&requiredLength,
														NULL))
				{
/*
					if( pType != NULL && _tcscmp(pType, "") != 0 )
					{

						if( _tcscmp(pType, "TEC") == 0 )
						{
							if( _tcsstr(functionClassDeviceData->DevicePath, "vid_08a6") != NULL )
								iNum++;
						}
						else
						{
							if( _tcsstr(functionClassDeviceData->DevicePath, pType) != NULL )
								iNum++;
						}

					}
*/
					if( _tcscmp(pPortName, "") != 0 )
					{
						if( _tcsstr(functionClassDeviceData->DevicePath, pPortName) != NULL )
							iNum++;
					}
					else
						iNum++;

					iRes = iNum;
					if( iRes == iOrder )
					{
						_tcscpy(pDeviceName, functionClassDeviceData->DevicePath);
						free(functionClassDeviceData);
						return iRes;
					}
				}
				free(functionClassDeviceData);
			}
			else
			{
				if( ERROR_NO_MORE_ITEMS == GetLastError() )
				{
					done = TRUE;
					break;
				}
			}
		}
	}
	iRes = -1;

	return iRes;
}


//
//	GetDeviceCheck：GUIDで検出可能なデバイス数を取得
//	RETURN：0以上の場合はデバイス数、-1の場合はパラメータエラー
//	IN	pGuid：GUID
//	IN	pType：検出対象となるデバイスの種別
//
INT GetDeviceCheck(
	LPGUID		pGuid,
	LPTSTR		pType
	)
{
	INT			iNum;
	ULONG		i, NumberDevices;
	BOOLEAN		done;
	HDEVINFO	hardwareDeviceInfo;
	SP_INTERFACE_DEVICE_DATA deviceInfoData;

	iNum = 0;
	NumberDevices = 4;
	done = FALSE;
	deviceInfoData.cbSize = sizeof(SP_INTERFACE_DEVICE_DATA);

	hardwareDeviceInfo = SetupDiGetClassDevs(pGuid, NULL, NULL, (DIGCF_PRESENT | DIGCF_DEVICEINTERFACE));

	if( hardwareDeviceInfo == 0 )
		return -1;

	while( !done )
	{
		NumberDevices *= 2;

		for( i = 0; i < NumberDevices; i++ )
		{
			if( SetupDiEnumDeviceInterfaces(hardwareDeviceInfo, 0, pGuid, i, &deviceInfoData) )
			{
				if( pType != NULL && _tcscmp(pType, "") != 0 )
				{
					PSP_INTERFACE_DEVICE_DETAIL_DATA	functionClassDeviceData = NULL;
					ULONG								predictedLength = 0;
					ULONG								requiredLength = 0;

					SetupDiGetDeviceInterfaceDetail(hardwareDeviceInfo, &deviceInfoData, NULL, 0, &requiredLength, NULL);

					predictedLength = requiredLength;

					functionClassDeviceData = (PSP_INTERFACE_DEVICE_DETAIL_DATA)malloc(predictedLength);
					functionClassDeviceData->cbSize = sizeof(SP_INTERFACE_DEVICE_DETAIL_DATA);

					if( SetupDiGetInterfaceDeviceDetail(hardwareDeviceInfo,
															&deviceInfoData,
															functionClassDeviceData,
															predictedLength,
															&requiredLength,
															NULL))
					{
						if( _tcscmp(pType, "TEC") == 0 )
						{
							if( _tcsstr(functionClassDeviceData->DevicePath, "vid_08a6") != NULL )
								iNum++;
						}
						else
						{
							if( _tcsstr(functionClassDeviceData->DevicePath, pType) != NULL )
								iNum++;
						}
					}
					free(functionClassDeviceData);
				}
				else
					iNum++;
			}
			else
			{
				if( ERROR_NO_MORE_ITEMS == GetLastError() )
				{
					done = TRUE;
					break;
				}
			}
		}
	}

	return iNum;
}