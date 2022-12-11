@ECHO OFF
Set Path=%Path%;C:\Program Files\Remotesoft\Obfuscator\bin

echo ***********  [ obfuscating SystemModify] **************

d:

cd D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\SystemModify\bin
obfuscator -dll -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam Utility.dll
obfuscator -exe -sign "D:\sunlike.snk" -clrversion v2.0.50727  -removeparam SystemModify.exe

echo ***********  [ Íê³É ] **************
@ECHO ON
