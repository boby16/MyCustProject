* Function in ttftohex.dll ;
*   getfonthex       -  - Get bitmap in hex code of specific out
*                         string and font to buffer.
*     Parameter:
*       1) LPSTR outStr,              // output string   字符串  (或单色bmp文件名称)
*       2) LPSTR lfFaceName,          // Windows font name  字体名称 (或指定为 BITMAP)
*       3) int   lfStrSize,           // string size (in 0.1mm units, in 0.05mm units if
                                      // the printer resolution is 600dpi )  字体大小(单位 0.1mm,若打印机为600DPI,则单位为0.05mm)
*       4) int   lfXCoordinates,      // x-coordinates of print origin for drawing  打印开始位置X坐标
*       5) int   lfYCoordinates,      // y-coordinates of print origin for drawing  打印开始位置Y坐标
*       6) int   lfstyle,             // font style 0:normal,1:bold ,2:Italic,3:underline,4:strikeout  字形
*       7) LPSTR hexBuf               // buffer to receive hex codes,
*                                        buffer size has better less than 100k    接收缓冲区:字符以16进制存储 Buffer最好小于100K
*    int Return : Byte count of buffer contents if successful     返回值: Buffer的大小
*
* Note : 1) Before program to call function getfonthex in ttftohex.dll,
*           Statement must be added to declare it in the call program.
*        2) Function name getfonthex must in lower case.
*        3) Before function getfonthex() is called, the buffer must to allocate first.
*        4) The return of getfonthex() is greate than 0 if function call is
*           successful, and result of Chinese data is stored buffer.
*           The total number of byte output in buffer is return by getfonthex().



1.                     ttftohex.dll 调用例程
`........
implementation
function getfonthex(pch_text,pch_font:pchar;int_fontsize,int_x,int_y,intstyle:integer;var ary_byt:array of byte):integer;stdcall;external '.\ttftohex.dll' name 'getfonthex';
{$R *.DFM}
...
...
procedure TTEC_DLL.btn_CallDllClick(Sender: TObject);
var    .....
      ary_byt1: array [0..51200] of byte;
       ....
begin
    .......
    ...
    int_bufsize:=getfonthex('DLL示例 ','黑体',50 ,20,20,0,ary_byt1);  //汉字存入ary_byt1缓存中
    for int_i:=0 to int_bufsize-1 do
    begin
         str_out:=str_out+chr(ary_byt1[int_i]);                       //ary_byt1l转换为字符串
    end;
    assignfile(f_iofile,'COM1');               //定义端口
    rewrite(f_iofile);
    writeln(f_iofile,'{D0281,0870,0251|}');    //定义标签纸 87mm*25.1mm,标签间隔3mm
    writeln(f_iofile,'{C|}');                  //缓存初始化
    writeln(f_iofile,str_out);                 //写(DLL示例)到端口
    writeln(f_iofile,'{XS;I,0002,0002C4201|}');//输出2张
    closefile(f_iofile);
    .....
    ...
END.

2.             　调用单色bmp文件方法  20051110
.....
........
int_bufsize:=getfonthex('C:\MYPIC.BMP','BITMAP',50 ,20,20,0,ary_byt1);  //图像文件存入ary_byt1缓存中
.....
....


//注: 文件必须是单色位图格式BMP文件 ,文件不能大于hexBuf Buffer的大小