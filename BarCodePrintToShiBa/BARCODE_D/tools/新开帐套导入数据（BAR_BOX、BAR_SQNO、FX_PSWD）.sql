--ע�⣺��sql�����Ҫ�ֹ��޸�һЩ���ݣ�����ִ�У���������˵����



--����BAR_BOX
--˵��������������ס���������Ҫ�ֹ����롣�����ʽ��������(��DB_2009);�����ף���DB_2010��
USE ������;
GO
INSERT INTO BAR_BOX 
SELECT A.* FROM ������..BAR_BOX A 
LEFT JOIN ������..BAR_BOX B ON A.BOX_NO=B.BOX_NO
WHERE ISNULL(B.BOX_NO,'')='' 
--���������:������ڶ��ٵĲŵ���.����:A.BOX_NO>'*20091231'
--�������ȫ������,��ֻ����������һ��
--AND A.BOX_NO>''

--����BAR_SQNO
--˵��������������ס���������Ҫ�ֹ����롣�����ʽ��������(��DB_2009);�����ף���DB_2010��
USE ������;
GO
INSERT INTO BAR_SQNO 
SELECT A.* FROM ������..BAR_SQNO A 
LEFT JOIN ������..BAR_SQNO B ON A.PRD_NO=B.PRD_NO AND A.PRD_MARK=B.PRD_MARK
WHERE ISNULL(B.PRD_NO,'')='' 
--���������:���Ŵ��ڶ��ٵĲŵ���.����:A.PRD_MARK>'09C'
--���ȫ������,��ֻ����������һ��
--AND A.PRD_MARK>''

--����FX_PSWD
--˵��:������������״��š������״�����Ҫ�ֹ����롣�����ʽ�������״���(�磺2009,��Ҫ��DB_);�����״���(�磺2010,��Ҫ��DB_)
USE SUNSYSTEM;
GO
INSERT INTO SUNSYSTEM 
SELECT '�����״���' as COMPNO,ROLENO,PGM,QRY,INS,UPD,
DEL,PRN,[PROPERTY], U_AMT,R_CST,RANGE,LCK,ALLOW_ID,FLD,UPR,QTY,EPT
FROM SUNSYSTEM
WHERE ISNULL(COMPNO,'')='�����״���' 

