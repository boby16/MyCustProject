// USBTestDlg.h : �w�b�_�[ �t�@�C��
//

#if !defined(AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_)
#define AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CUSBTestDlg �_�C�A���O

class CUSBTestDlg : public CDialog
{
// �\�z
public:
	CUSBTestDlg(CWnd* pParent = NULL);	// �W���̃R���X�g���N�^

// �_�C�A���O �f�[�^
	//{{AFX_DATA(CUSBTestDlg)
	enum { IDD = IDD_USBTEST_DIALOG };
		// ����: ���̈ʒu�� ClassWizard �ɂ���ăf�[�^ �����o���ǉ�����܂��B
	//}}AFX_DATA

	// ClassWizard �͉��z�֐��̃I�[�o�[���C�h�𐶐����܂��B
	//{{AFX_VIRTUAL(CUSBTestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �̃T�|�[�g
	//}}AFX_VIRTUAL

// �C���v�������e�[�V����
protected:
	HICON m_hIcon;

	// �������ꂽ���b�Z�[�W �}�b�v�֐�
	//{{AFX_MSG(CUSBTestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnPrint();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ �͑O�s�̒��O�ɒǉ��̐錾��}�����܂��B

#endif // !defined(AFX_USBTESTDLG_H__238DBDD4_0030_4FC4_9647_2662B99F3336__INCLUDED_)
