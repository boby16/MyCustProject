* Function in ttftohex.dll ;
*   getfonthex       -  - Get bitmap in hex code of specific out
*                         string and font to buffer.
*     Parameter:
*       1) LPSTR outStr,              // output string   �ַ���  (��ɫbmp�ļ�����)
*       2) LPSTR lfFaceName,          // Windows font name  �������� (��ָ��Ϊ BITMAP)
*       3) int   lfStrSize,           // string size (in 0.1mm units, in 0.05mm units if
                                      // the printer resolution is 600dpi )  �����С(��λ 0.1mm,����ӡ��Ϊ600DPI,��λΪ0.05mm)
*       4) int   lfXCoordinates,      // x-coordinates of print origin for drawing  ��ӡ��ʼλ��X����
*       5) int   lfYCoordinates,      // y-coordinates of print origin for drawing  ��ӡ��ʼλ��Y����
*       6) int   lfstyle,             // font style 0:normal,1:bold ,2:Italic,3:underline,4:strikeout  ����
*       7) LPSTR hexBuf               // buffer to receive hex codes,
*                                        buffer size has better less than 100k    ���ջ�����:�ַ���16���ƴ洢 Buffer���С��100K
*    int Return : Byte count of buffer contents if successful     ����ֵ: Buffer�Ĵ�С
*
* Note : 1) Before program to call function getfonthex in ttftohex.dll,
*           Statement must be added to declare it in the call program.
*        2) Function name getfonthex must in lower case.
*        3) Before function getfonthex() is called, the buffer must to allocate first.
*        4) The return of getfonthex() is greate than 0 if function call is
*           successful, and result of Chinese data is stored buffer.
*           The total number of byte output in buffer is return by getfonthex().



1.                     ttftohex.dll ��������
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
    int_bufsize:=getfonthex('DLLʾ�� ','����',50 ,20,20,0,ary_byt1);  //���ִ���ary_byt1������
    for int_i:=0 to int_bufsize-1 do
    begin
         str_out:=str_out+chr(ary_byt1[int_i]);                       //ary_byt1lת��Ϊ�ַ���
    end;
    assignfile(f_iofile,'COM1');               //����˿�
    rewrite(f_iofile);
    writeln(f_iofile,'{D0281,0870,0251|}');    //�����ǩֽ 87mm*25.1mm,��ǩ���3mm
    writeln(f_iofile,'{C|}');                  //�����ʼ��
    writeln(f_iofile,str_out);                 //д(DLLʾ��)���˿�
    writeln(f_iofile,'{XS;I,0002,0002C4201|}');//���2��
    closefile(f_iofile);
    .....
    ...
END.

2.             �����õ�ɫbmp�ļ�����  20051110
.....
........
int_bufsize:=getfonthex('C:\MYPIC.BMP','BITMAP',50 ,20,20,0,ary_byt1);  //ͼ���ļ�����ary_byt1������
.....
....


//ע: �ļ������ǵ�ɫλͼ��ʽBMP�ļ� ,�ļ����ܴ���hexBuf Buffer�Ĵ�С