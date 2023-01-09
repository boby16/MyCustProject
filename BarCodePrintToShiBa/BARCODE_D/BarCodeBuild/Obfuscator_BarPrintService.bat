@ECHO OFF
Set Path=%Path%;C:\Program Files\Remotesoft\Obfuscator\bin

echo ***********  [ obfuscating BarPrintService] **************

d:
cd D:\SourceCode\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\bin
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam SpcPgm.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam CommonVar.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam DbSunSys.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam Business.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam DataAccess.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam SunSys.dll
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam Utility.dll

echo ***********  [ Íê³É ] **************
@ECHO ON
