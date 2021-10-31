del .\Build.txt

del /f /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarCodePrint\*.*
rd /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarCodePrint
del /f /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\*.*
rd /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService

call "D:\Program Files (x86)\Microsoft Visual Studio 8\Common7\IDE\devenv.exe" /out ".\Build.txt" /rebuild release "D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCode.sln"


cd D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\
Call Obfuscator_BarPrintService.bat >.\Obfuscator_BarPrintService.txt

copy BarPrintService.dll obfuscated\

xcopy /y D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\BarPrintServer.asmx D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\
copy D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\Global.asax D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\
xcopy /y D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\bin\obfuscated\*.dll D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\bin\
xcopy /y /s D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\Resources\*.* D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\Resources\

del D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\Resources\vssver.scc
del D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\Resources\en-us\vssver.scc
del D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\Resources\zh-cn\vssver.scc
del D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\Resources\zh-tw\vssver.scc

del /f /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\bin\obfuscated\*.*
rd /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarPrintService\bin\obfuscated

xcopy /y D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodePrint\bin\Debug\BarCodePrintTSB.exe D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarCodePrint\
copy D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodePrint\bin\Debug\TSCLIB.DLL D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarCodePrint\
copy D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodePrint\bin\Debug\Interop.Excel.dll D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarCodePrint\


del /f /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\SystemModify\*.*
rd /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\SystemModify


cd D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\
Call Obfuscator_SystemModify.bat >.\Obfuscator_SystemModify.txt

xcopy /y D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\SystemModify\bin\obfuscated\SystemModify.exe D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\

copy D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\SystemModify\bin\obfuscated\*.dll D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\BarPrintService\

copy D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\°æ±¾ËµÃ÷.txt  D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\BarCodeBuild\update\

del /f /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\SystemModify\bin\obfuscated\*.*
rd /s /q D:\Git\MyCustProject\QRCodePrintGPForToshiba\BARCODE_D\SystemModify\bin\obfuscated

pause