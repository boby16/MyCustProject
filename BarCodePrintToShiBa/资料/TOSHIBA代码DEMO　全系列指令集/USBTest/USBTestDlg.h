// USBTestDlg.h : ヘッダー ファイル
//

#if !defined(AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_)
#define AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CUSBTestDlg ダイアログ

class CUSBTestDlg : public CDialog
{
// 構築
public:
	CUSBTestDlg(CWnd* pParent = NULL);	// 標準のコンストラクタ

// ダイアログ データ
	//{{AFX_DATA(CUSBTestDlg)
	enum { IDD = IDD_USBTEST_DIALOG };
		// メモ: この位置に ClassWizard によってデータ メンバが追加されます。
	//}}AFX_DATA

	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CUSBTestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV のサポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON m_hIcon;

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CUSBTestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnPrint();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_)
