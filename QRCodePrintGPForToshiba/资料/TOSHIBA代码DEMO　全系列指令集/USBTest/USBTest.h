// USBTest.h : USBTEST アプリケーションのメイン ヘッダー ファイルです。
//

#if !defined(AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_)
#define AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// メイン シンボル

/////////////////////////////////////////////////////////////////////////////
// CUSBTestApp:
// このクラスの動作の定義に関しては USBTest.cpp ファイルを参照してください。
//

class CUSBTestApp : public CWinApp
{
public:
	CUSBTestApp();

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CUSBTestApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// インプリメンテーション

	//{{AFX_MSG(CUSBTestApp)
		// メモ - ClassWizard はこの位置にメンバ関数を追加または削除します。
		//        この位置に生成されるコードを編集しないでください。
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_)
