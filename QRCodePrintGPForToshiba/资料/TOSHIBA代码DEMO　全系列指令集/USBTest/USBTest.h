// USBTest.h : USBTEST �A�v���P�[�V�����̃��C�� �w�b�_�[ �t�@�C���ł��B
//

#if !defined(AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_)
#define AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// ���C�� �V���{��

/////////////////////////////////////////////////////////////////////////////
// CUSBTestApp:
// ���̃N���X�̓���̒�`�Ɋւ��Ă� USBTest.cpp �t�@�C�����Q�Ƃ��Ă��������B
//

class CUSBTestApp : public CWinApp
{
public:
	CUSBTestApp();

// �I�[�o�[���C�h
	// ClassWizard �͉��z�֐��̃I�[�o�[���C�h�𐶐����܂��B
	//{{AFX_VIRTUAL(CUSBTestApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// �C���v�������e�[�V����

	//{{AFX_MSG(CUSBTestApp)
		// ���� - ClassWizard �͂��̈ʒu�Ƀ����o�֐���ǉ��܂��͍폜���܂��B
		//        ���̈ʒu�ɐ��������R�[�h��ҏW���Ȃ��ł��������B
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ �͑O�s�̒��O�ɒǉ��̐錾��}�����܂��B

#endif // !defined(AFX_USBTEST_H__62130AAD_2D93_4116_8244_1238A91DF249__INCLUDED_)
