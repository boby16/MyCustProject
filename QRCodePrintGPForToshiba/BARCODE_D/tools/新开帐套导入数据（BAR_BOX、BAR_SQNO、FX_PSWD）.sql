--注意：此sql语句需要手工修改一些内容，才能执行，详见下面的说明。



--导入BAR_BOX
--说明：下面的新帐套、旧帐套需要手工输入。输入格式：旧帐套(如DB_2009);新帐套（如DB_2010）
USE 新帐套;
GO
INSERT INTO BAR_BOX 
SELECT A.* FROM 旧帐套..BAR_BOX A 
LEFT JOIN 新帐套..BAR_BOX B ON A.BOX_NO=B.BOX_NO
WHERE ISNULL(B.BOX_NO,'')='' 
--下面的条件:箱码大于多少的才导入.例如:A.BOX_NO>'*20091231'
--如果箱码全部导入,则只需运行上面一段
--AND A.BOX_NO>''

--导入BAR_SQNO
--说明：下面的新帐套、旧帐套需要手工输入。输入格式：旧帐套(如DB_2009);新帐套（如DB_2010）
USE 新帐套;
GO
INSERT INTO BAR_SQNO 
SELECT A.* FROM 旧帐套..BAR_SQNO A 
LEFT JOIN 新帐套..BAR_SQNO B ON A.PRD_NO=B.PRD_NO AND A.PRD_MARK=B.PRD_MARK
WHERE ISNULL(B.PRD_NO,'')='' 
--下面的条件:批号大于多少的才导入.例如:A.PRD_MARK>'09C'
--如果全部导入,则只需运行上面一段
--AND A.PRD_MARK>''

--导入FX_PSWD
--说明:：下面的新帐套代号、旧帐套代号需要手工输入。输入格式：旧帐套代号(如：2009,不要加DB_);新帐套代号(如：2010,不要加DB_)
USE SUNSYSTEM;
GO
INSERT INTO SUNSYSTEM 
SELECT '新帐套代号' as COMPNO,ROLENO,PGM,QRY,INS,UPD,
DEL,PRN,[PROPERTY], U_AMT,R_CST,RANGE,LCK,ALLOW_ID,FLD,UPR,QTY,EPT
FROM SUNSYSTEM
WHERE ISNULL(COMPNO,'')='旧帐套代号' 

