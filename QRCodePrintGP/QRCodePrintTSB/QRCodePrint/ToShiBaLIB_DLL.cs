using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace QRCodePrintTSB
{
    public class ToShiBaLIB_DLL
    {
        //300DPI
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

        private static string dpi = GetPrintSetDPI();
        private static string printSet = ToShiBaLIB_DLL.GetPrintSet();
        private static string compType = GetCompanyType();
        public static void OrderNoBarCodePrint(string orderNo, string qty)
        {
            if (dpi == "200")
                OrderNoBarCodePrint200(orderNo, qty);
            else
                OrderNoBarCodePrint300(orderNo, qty);
        }

        public static void OrderNoBarCodePrintNoBox(string orderNo, string qty)
        {
            if (dpi == "200")
                OrderNoBarCodePrintNoBox200(orderNo, qty);
            else
                OrderNoBarCodePrintNoBox300(orderNo, qty);
        }

        public static void BoxPrint(BoxVO boxVo)
        {
            int curSerialNo = 0;
            if (int.TryParse(boxVo.SerialNo.Substring(8), out curSerialNo) && curSerialNo > 0)
            {
                for (var i = 1; i <= boxVo.PrintCount; i++)
                {
                    if (i > 1)
                    {
                        curSerialNo += 1;
                    }
                    boxVo.SerialNo = boxVo.SerialNo.Substring(0, boxVo.SerialNo.Length - 6) + curSerialNo.ToString("000000");
                    if (dpi == "200")
                        BoxPrint200(boxVo);
                    else
                        BoxPrint300(boxVo);
                }
                //SaveBoxSerialNo(curSerialNo);
                DbMannager.SaveDateSerailNo(boxVo.PDate, curSerialNo);
            }
        }
        public static void ProductPrint(ProductVO productVo)
        {
            int curSerialNo = 0;
            if (int.TryParse(productVo.SerialNo.Substring(8), out curSerialNo) && curSerialNo > 0)
            {
                for (var i = 1; i <= productVo.PrintCount; i++)
                {
                    if (i > 1)
                    {
                        curSerialNo += 1;
                    }
                    productVo.SerialNo = productVo.SerialNo.Substring(0, productVo.SerialNo.Length - 6) + curSerialNo.ToString("000000");
                    if (dpi == "200")
                        ProductPrint200(productVo);
                    else
                        ProductPrint300(productVo);
                }
                //SaveBoxSerialNo(curSerialNo);
                DbMannager.SaveDateSerailNo(productVo.PDate, curSerialNo);
            }
        }

        public static void OrderNoBarCodePrint300(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.sendcommand("BOX 50,50,800,110,4");
            ToShiBaLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.windowsfont(720, 65, 45, 0, 0, 0, "标楷体", qty);
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrintNoBox300(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.printlabel("1", qty);
            ToShiBaLIB_DLL.closeport();
        }

        public static void BoxPrint300(BoxVO boxVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = boxVo.PDate.Year +"/"+((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString())+ "/"+((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string lblPO = "PO：";
            string lblPN = "P/N：";
            string content3 = "LOT NO：";
            string lblDate = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string qrCode = string.Concat(new string[]
            {
                boxVo.PN,
                "\\",
                boxVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                boxVo.Qty,
                "\\",
                boxVo.DWGRev,
                "\\GP",
                boxVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", boxVo.CompName);


            ToShiBaLIB_DLL.sendcommand("ELLIPSE 800,60,200,80,6");
            ToShiBaLIB_DLL.windowsfont(820, 70, 60, 0, 2, 0, "标楷体", "ROHS");
            ToShiBaLIB_DLL.sendcommand("BOX 1060,60,1170,140,6");
            ToShiBaLIB_DLL.windowsfont(1080, 70, 60, 0, 2, 0, "标楷体", "HF");

            ToShiBaLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            ToShiBaLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", boxVo.OrderNo);
            ToShiBaLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPN);
            ToShiBaLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", boxVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", boxVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", boxVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", boxVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            ToShiBaLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + boxVo.SerialNo));
            //ToShiBaLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }


        public static void ProductPrint300(ProductVO productVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = productVo.PDate.Year + "/" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString()) + "/" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string lblPO = "PO：";
            string lblPn = "P/N：";
            string lblLotNo = "LOT NO：";
            string lblDate = "DATE：";
            string lblQty = "QTY：";
            string lblDwgrev = "DWG REV：";
            string lblSno = "Serial NO：";
            string _barcodetxt = productVo.GPPrdName + productVo.LotNo + productVo.SerialNo;//一维码
            string qrCode = string.Concat(new string[]
            {
                productVo.PN,
                "\\",
                productVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                productVo.Qty,
                "\\",
                productVo.DWGRev,
                "\\GP",
                productVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", productVo.CompName);

            ToShiBaLIB_DLL.sendcommand("ELLIPSE 800,50,200,80,6");
            ToShiBaLIB_DLL.windowsfont(820, 60, 60, 0, 2, 0, "标楷体", "ROHS");
            ToShiBaLIB_DLL.sendcommand("BOX 1060,50,1170,130,6");
            ToShiBaLIB_DLL.windowsfont(1080, 60, 60, 0, 2, 0, "标楷体", "HF");

            ToShiBaLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            ToShiBaLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", productVo.OrderNo);
            ToShiBaLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPn);
            ToShiBaLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", productVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", lblLotNo);
            ToShiBaLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", productVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", lblQty);
            ToShiBaLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", productVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", lblDwgrev);
            ToShiBaLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", productVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            ToShiBaLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", lblSno);
            ToShiBaLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + productVo.SerialNo));
            //ToShiBaLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", _barcodetxt);

            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrint200(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.sendcommand("BOX 30,30,550,70,4");
            ToShiBaLIB_DLL.windowsfont(50, 40, 30, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("250", "40", "128", "30", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.windowsfont(500, 42, 30, 0, 0, 0, "标楷体", qty);
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrintNoBox200(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(50, 40, 30, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("250", "40", "128", "30", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.printlabel("1", qty);
            ToShiBaLIB_DLL.closeport();
        }

        public static void BoxPrint200(BoxVO boxVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = boxVo.PDate.Year + "/" + ((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString()) + "/" + ((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string content = "PO：";
            string content2 = "P/N：";
            string content3 = "LOT NO：";
            string lblDate = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string str = string.Concat(new string[]
            {
                boxVo.PN,
                "\\",
                boxVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                boxVo.Qty,
                "\\",
                boxVo.DWGRev,
                "\\GP",
                boxVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(45, 45, 40, 0, 0, 0, "标楷体", boxVo.CompName);
            ToShiBaLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            ToShiBaLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            ToShiBaLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            ToShiBaLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", boxVo.PN);
            ToShiBaLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", boxVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", boxVo.Qty);
            ToShiBaLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", boxVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            ToShiBaLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }
        public static void ProductPrint200(ProductVO productVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = productVo.PDate.Year + "/" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString()) + "/" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string content = "PO：";
            string content2 = "P/N：";
            string content3 = "LOT NO：";
            string content4 = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string _barcodetxt = productVo.GPPrdName + productVo.LotNo + productVo.SerialNo;//一维码
            string str = string.Concat(new string[]
            {
                productVo.PN,
                "\\",
                productVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                productVo.Qty,
                "\\",
                productVo.DWGRev,
                "\\",
                _barcodetxt
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.sendcommand("DIRECTION 1,0");
            ToShiBaLIB_DLL.sendcommand("REFERENCE 0,0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(65, 45, 40, 0, 0, 0, "标楷体", productVo.CompName);
            ToShiBaLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            ToShiBaLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            ToShiBaLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            ToShiBaLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", productVo.PN);
            ToShiBaLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", productVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", content4);
            ToShiBaLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", productVo.Qty);
            ToShiBaLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", productVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            ToShiBaLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", _barcodetxt);

            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }




        public static void OrderNoBarCodePrintNew(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.sendcommand("BOX 50,50,800,110,4");
            ToShiBaLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.windowsfont(720, 65, 45, 0, 0, 0, "标楷体", qty);
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrintNoBoxNew(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            ToShiBaLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            ToShiBaLIB_DLL.printlabel("1", qty);
            ToShiBaLIB_DLL.closeport();
        }

        public static void BoxPrintNew(BoxVO boxVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = boxVo.PDate.Year + "/" + ((boxVo.PDate.Month < 10) ? ("0" + boxVo.PDate.Month.ToString()) : boxVo.PDate.Month.ToString()) + "/" + ((boxVo.PDate.Day < 10) ? ("0" + boxVo.PDate.Day.ToString()) : boxVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string lblPO = "PO：";
            string lblPN = "P/N：";
            string content3 = "LOT NO：";
            string lblDate = "DATE：";
            string content5 = "QTY：";
            string content6 = "DWG REV：";
            string content7 = "Serial NO：";
            string qrCode = string.Concat(new string[]
            {
                boxVo.PN,
                "\\",
                boxVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                boxVo.Qty,
                "\\",
                boxVo.DWGRev,
                "\\GP",
                boxVo.SerialNo
            });

            string _ip = GetPrintSetIP();
            string _port = GetPrintSetPort();


            StringBuilder str = new StringBuilder();
            str.Append("{D1150,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            str.Append("{PC000;0070,0070,20,20,e,00,B,J0101=" + boxVo.CompName + "|}").Append('\n');


            ToShiBaLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", boxVo.CompName);


            ToShiBaLIB_DLL.sendcommand("ELLIPSE 800,60,200,80,6");
            ToShiBaLIB_DLL.windowsfont(820, 70, 60, 0, 2, 0, "标楷体", "ROHS");
            ToShiBaLIB_DLL.sendcommand("BOX 1060,60,1170,140,6");
            ToShiBaLIB_DLL.windowsfont(1080, 70, 60, 0, 2, 0, "标楷体", "HF");

            ToShiBaLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            ToShiBaLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", boxVo.OrderNo);
            ToShiBaLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPN);
            ToShiBaLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", boxVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", content3);
            ToShiBaLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", boxVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", content5);
            ToShiBaLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", boxVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", content6);
            ToShiBaLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", boxVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            ToShiBaLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", content7);
            ToShiBaLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + boxVo.SerialNo));
            //ToShiBaLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }


        public static void ProductPrintNew(ProductVO productVo)
        {
            var _dateValPrint = string.Empty;
            string _dateVal = productVo.PDate.Year + "/" + ((productVo.PDate.Month < 10) ? ("0" + productVo.PDate.Month.ToString()) : productVo.PDate.Month.ToString()) + "/" + ((productVo.PDate.Day < 10) ? ("0" + productVo.PDate.Day.ToString()) : productVo.PDate.Day.ToString());
            if (compType == "1")
                _dateValPrint = _dateVal;
            string lblPO = "PO：";
            string lblPn = "P/N：";
            string lblLotNo = "LOT NO：";
            string lblDate = "DATE：";
            string lblQty = "QTY：";
            string lblDwgrev = "DWG REV：";
            string lblSno = "Serial NO：";
            string _barcodetxt = productVo.GPPrdName + productVo.LotNo + productVo.SerialNo;//一维码
            string qrCode = string.Concat(new string[]
            {
                productVo.PN,
                "\\",
                productVo.LotNo,
                "\\",
                _dateVal,
                "\\",
                productVo.Qty,
                "\\",
                productVo.DWGRev,
                "\\GP",
                productVo.SerialNo
            });
            ToShiBaLIB_DLL.openport(printSet);
            ToShiBaLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            ToShiBaLIB_DLL.clearbuffer();
            ToShiBaLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", productVo.CompName);

            ToShiBaLIB_DLL.sendcommand("ELLIPSE 800,50,200,80,6");
            ToShiBaLIB_DLL.windowsfont(820, 60, 60, 0, 2, 0, "标楷体", "ROHS");
            ToShiBaLIB_DLL.sendcommand("BOX 1060,50,1170,130,6");
            ToShiBaLIB_DLL.windowsfont(1080, 60, 60, 0, 2, 0, "标楷体", "HF");

            ToShiBaLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            ToShiBaLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", productVo.OrderNo);
            ToShiBaLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            ToShiBaLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPn);
            ToShiBaLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", productVo.PN);
            ToShiBaLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", lblLotNo);
            ToShiBaLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", productVo.LotNo);
            ToShiBaLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            ToShiBaLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            ToShiBaLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", lblQty);
            ToShiBaLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", productVo.Qty);
            ToShiBaLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", lblDwgrev);
            ToShiBaLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", productVo.DWGRev);
            ToShiBaLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            ToShiBaLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            ToShiBaLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", lblSno);
            ToShiBaLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + productVo.SerialNo));
            //ToShiBaLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", _barcodetxt);

            ToShiBaLIB_DLL.printlabel("1", "1");
            ToShiBaLIB_DLL.closeport();
        }

        public void barCodePrintNew(DataRow _dr)
        {

            BarSet _bs = new BarSet();
            string fileName = System.Windows.Forms.Application.ExecutablePath + ".config";
            string _ip = _bs.GetPrintSet(fileName, "PrintSetIP");
            string _port = _bs.GetPrintSet(fileName, "PrintSetPort");

            String ipPort = "";
            //建立连接
            IPAddress ipa = IPAddress.Parse(_ip);
            IPEndPoint ipe = new IPEndPoint(ipa, int.Parse(_port));
            Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            soc.Connect(ipe);


            //string str= "hello,123456789,大家好! ";



            //箱条码（富士康版）
            string _bar_no = _dr["BARCODE"].ToString();//序列号
            string _line11 = "供 应 商   " + _dr["FOX_SUP"].ToString(); string _line12 = "材　　　 质   " + _dr["FOX_CZ"].ToString();
            string _line21 = "品　　名   " + _dr["FOX_PRD_NAME"].ToString(); string _line22 = "规　　　 格   " + _dr["BOX_SPC"].ToString();
            string _line31 = "版　　次   " + _dr["FOX_BC"].ToString(); string _line32 = "Foxconn料号 " + _dr["FOX_LH"].ToString();
            string _line41 = "生产批号   " + _dr["FOX_BAT_MANU"].ToString(); string _line42 = "原 料 批 号   " + _dr["FOX_BAT_MAT"].ToString();
            string _qty = "数量(卷)   " + _dr["BOX_QTY"].ToString();
            string _weight = "重量(KG)   " + _dr["FOX_WEIGHT"].ToString();
            string _bat_no = "追溯批号   " + _dr["BOX_BAT"].ToString();

            StringBuilder str = new StringBuilder();
            str.Append("{D1150,0950,1050|}").Append('\n');
            str.Append("{C|}").Append('\n');

            str.Append("{PC000;0950,0780,20,20,e,22,B,J0101=" + _line11 + "|}").Append('\n');
            str.Append("{PC000;0550,0780,20,20,e,22,B,J0101=" + _line12 + "|}").Append('\n');

            str.Append("{PC000;0950,0700,20,20,e,22,B,J0101=" + _line21 + "|}").Append('\n');
            str.Append("{PC000;0550,0700,20,20,e,22,B,J0101=" + _line22 + "|}").Append('\n');

            str.Append("{PC000;0950,0620,20,20,e,22,B,J0101=" + _line31 + "|}").Append('\n');
            str.Append("{PC000;0550,0620,20,20,e,22,B,J0101=" + _line32 + "|}").Append('\n');

            str.Append("{PC000;0950,0540,20,20,e,22,B,J0101=" + _line41 + "|}").Append('\n');
            str.Append("{PC000;0550,0540,20,20,e,22,B,J0101=" + _line42 + "|}").Append('\n');

            str.Append("{PC000;0950,0460,20,20,e,22,B,J0101=" + _qty + "|}").Append('\n');
            str.Append("{PC000;0950,0380,20,20,e,22,B,J0101=" + _weight + "|}").Append('\n');

            str.Append("{PC000;0950,0300,20,20,e,22,B,J0101=" + _bat_no + "|}").Append('\n');
            str.Append("{PC000;0950,0220,20,20,e,22,B,J0101=出货日期|}").Append('\n');

            str.Append("{XB00;0300,0100,9,1,04,0,0080,+0000000000,000,0,00=" + _bar_no + _dr["INIT_NO"].ToString() + "|}").Append('\n');

            str.Append("{PC000;0600,0060,15,15,e,22,B=" + _bar_no.Substring(1) + "-" + _dr["INIT_NO"].ToString() + "|}").Append('\n');

            str.Append("{XS;I,0001,0002C6111|}").Append('\n');

            byte[] b = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str.ToString());
            soc.Send(b);
            soc.Close();
        }


        public static string GetPrintSet()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetPath\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        /// <summary>
        /// DPI分为200（分公司为243）和300（总公司342）
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetDPI()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetDPI\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }
        /// <summary>
        /// 1为分公司；否则为总公司
        /// </summary>
        /// <returns></returns>
        public static string GetCompanyType()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"CompanyType\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        /// <summary>
        /// IP
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetIP()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetIP\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }



        /// <summary>
        /// PORT
        /// </summary>
        /// <returns></returns>
        public static string GetPrintSetPort()
        {
            string result = "";
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            string xpath = "/configuration/appSettings/add[@key=\"PrintSetPort\"]/@value";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            if (xmlNode != null)
            {
                result = xmlNode.InnerText;
            }
            return result;
        }

        public static string GetBoxSerialNo(DateTime date)
        {
            //return GetBoxSerialDate() + GetBoxMaxSerialNo().ToString("000000");

            var dateCur = date.Year.ToString() + "" + ((date.Month < 10) ? ("0" + date.Month.ToString()) : date.Month.ToString())
                + "" + ((date.Day < 10) ? ("0" + date.Day.ToString()) : date.Day.ToString());

            var serialNo = DbMannager.GetMaxSerailNo(date).ToString("000000");
            return dateCur + serialNo;
        }

        #region ///
        //private static int GetBoxMaxSerialNo()
        //{
        //    GetBoxSerialDate();

        //    int result = 0;
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //    string xpath = "/configuration/appSettings/add[@key=\"BoxSerialNo\"]/@value";
        //    XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //    if (xmlNode != null)
        //    {
        //        result = Convert.ToInt32(xmlNode.InnerText);
        //    }
        //    result += 1;
        //    return result;
        //}
        //private static string GetBoxSerialDate()
        //{
        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //    string xpath = "/configuration/appSettings/add[@key=\"BoxSerialDate\"]/@value";
        //    XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //    if (xmlNode != null)
        //    {
        //        var result = Convert.ToDateTime(xmlNode.InnerText);
        //        if (result.Date != DateTime.Today)
        //        {
        //            SaveBoxSerialNo(0, true);
        //            SaveBoxSerialDate();
        //        }
        //    }
        //    else
        //    {
        //        SaveBoxSerialNo(0, true);
        //        SaveBoxSerialDate();
        //    }
        //    var dateCur = DateTime.Today.Year.ToString() + "" + ((DateTime.Today.Month < 10) ? ("0" + DateTime.Today.Month.ToString()) : DateTime.Today.Month.ToString())
        //        + "" + ((DateTime.Today.Day < 10) ? ("0" + DateTime.Today.Day.ToString()) : DateTime.Today.Day.ToString());
        //    return dateCur;
        //}

        //private static bool SaveBoxSerialNo(int boxSerialNo, bool modifyEnforce = false)
        //{
        //    var key = "BoxSerialNo";
        //    try
        //    {
        //        int result = 0;
        //        XmlDocument xmlDocument = new XmlDocument();
        //        xmlDocument.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
        //        string xpath = "/configuration/appSettings/add[@key=\"BoxSerialNo\"]/@value";
        //        XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
        //        if (xmlNode != null)
        //        {
        //            result = Convert.ToInt32(xmlNode.InnerText);
        //        }

        //        if (result < boxSerialNo || modifyEnforce)
        //        {
        //            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //            if (config.AppSettings.Settings[key] != null)
        //                config.AppSettings.Settings[key].Value = boxSerialNo.ToString();
        //            else
        //                config.AppSettings.Settings.Add(key, boxSerialNo.ToString());
        //            config.Save(ConfigurationSaveMode.Modified);
        //            ConfigurationManager.RefreshSection("appSettings");
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private static bool SaveBoxSerialDate()
        //{
        //    var key = "BoxSerialDate";
        //    try
        //    {
        //        var dateCur = DateTime.Today.Year.ToString() + "-" + ((DateTime.Today.Month < 10) ? ("0" + DateTime.Today.Month.ToString()) : DateTime.Today.Month.ToString())
        //            + "-" + ((DateTime.Today.Day < 10) ? ("0" + DateTime.Today.Day.ToString()) : DateTime.Today.Day.ToString());

        //        Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //        if (config.AppSettings.Settings[key] != null)
        //            config.AppSettings.Settings[key].Value = dateCur;
        //        else
        //            config.AppSettings.Settings.Add(key, dateCur);
        //        config.Save(ConfigurationSaveMode.Modified);
        //        ConfigurationManager.RefreshSection("appSettings");
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        #endregion
    }

    public class DbMannager
    {
        public static OleDbConnection getConn()
        {
            var dataSourceUrl = System.Windows.Forms.Application.StartupPath;
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dataSourceUrl + "\\foconn.accdb";
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }

        public static DataSet GetData()
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from BAR_IDX";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "QRCodeData");
            conn.Close();
            return mydataset;
        }

        public static DataSet GetDateSerailNo(DateTime date)
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from Date_SerailNo Where S_Date=#" + date.ToString("yyyy-MM-dd") + "#";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "Date_SerailNo");
            conn.Close();
            conn.Dispose();
            return mydataset;
        }


        public static int GetMaxSerailNo(DateTime date)
        {
            OleDbConnection conn = DbMannager.getConn();
            OleDbDataAdapter adapter = new OleDbDataAdapter();
            string sqlstr = "select * from Date_SerailNo Where S_Date=#" + date.ToString("yyyy-MM-dd") + "#";
            var mydataset = new DataSet();
            adapter.SelectCommand = new OleDbCommand(sqlstr, conn);
            adapter.Fill(mydataset, "Date_SerailNo");
            conn.Close();
            conn.Dispose();
            if (mydataset != null && mydataset.Tables.Count > 0 && mydataset.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(mydataset.Tables[0].Rows[0]["SerialNo"]) + 1;
            }
            return 1;
        }

        public static bool SaveDateSerailNo(DateTime date, int serialNo)
        {
            var mydataset = GetDateSerailNo(date);
            if (mydataset != null && mydataset.Tables.Count > 0 && mydataset.Tables[0].Rows.Count > 0)
            {
                OleDbConnection conn = DbMannager.getConn();
                try
                {
                    conn.Open();
                    string sqlstr = string.Format("Update Date_SerailNo Set SerialNo={1} where S_Date=#{0}#", date.ToString("yyyy-MM-dd"), serialNo);
                    OleDbCommand inst = new OleDbCommand(sqlstr, conn);
                    inst.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
                return true;
            }
            else
            {
                OleDbConnection conn = DbMannager.getConn();
                try
                {
                    conn.Open();
                    string sqlstr = "Insert into Date_SerailNo (S_Date,SerialNo) values ('" + date.ToString("yyyy-MM-dd") + "'," + serialNo + ")";
                    OleDbCommand inst = new OleDbCommand(sqlstr, conn);
                    inst.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.Dispose();
                }
                return true;
            }
        }
    }

    public class BoxVO
    {
        public string CompName
        {
            get;
            set;
        }
        public string GPPrdName
        {
            get;
            set;
        }
        public string PN
        {
            get;
            set;
        }
        public string SPC
        {
            get;
            set;
        }
        public string LotNo
        {
            get;
            set;
        }
        public DateTime PDate
        {
            get;
            set;
        }
        public string Qty
        {
            get;
            set;
        }
        public string DWGRev
        {
            get;
            set;
        }
        public string SerialNo
        {
            get;
            set;
        }
        public string OrderNo { get; set; }

        public int PrintCount { get; set; }
    }

    public class ProductVO
    {
        public string CompName
        {
            get;
            set;
        }
        public string GPPrdName
        {
            get;
            set;
        }
        public string PN
        {
            get;
            set;
        }
        public string SPC
        {
            get;
            set;
        }
        public string LotNo
        {
            get;
            set;
        }
        public DateTime PDate
        {
            get;
            set;
        }
        public string Qty
        {
            get;
            set;
        }
        public string DWGRev
        {
            get;
            set;
        }
        public string SerialNo
        {
            get;
            set;
        }

        public string OrderNo { get; set; }

        public int PrintCount { get; set; }
    }
}
