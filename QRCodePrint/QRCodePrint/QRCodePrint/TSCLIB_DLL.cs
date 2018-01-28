using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace QRCodePrint
{
    public class TSCLIB_DLL
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
        private static string printSet = TSCLIB_DLL.GetPrintSet();
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
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.sendcommand("BOX 50,50,800,110,4");
            TSCLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            TSCLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            TSCLIB_DLL.windowsfont(720, 65, 45, 0, 0, 0, "标楷体", qty);
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrintNoBox300(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(80, 65, 45, 0, 0, 0, "标楷体", orderNo);
            TSCLIB_DLL.barcode("400", "60", "128", "50", "0", "0", "2", "4", orderNo);
            TSCLIB_DLL.printlabel("1", qty);
            TSCLIB_DLL.closeport();
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
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", boxVo.CompName);


            TSCLIB_DLL.sendcommand("ELLIPSE 800,60,200,80,6");
            TSCLIB_DLL.windowsfont(820, 70, 60, 0, 2, 0, "标楷体", "ROHS");
            TSCLIB_DLL.sendcommand("BOX 1060,60,1170,140,6");
            TSCLIB_DLL.windowsfont(1080, 70, 60, 0, 2, 0, "标楷体", "HF");

            TSCLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            TSCLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", boxVo.OrderNo);
            TSCLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            TSCLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPN);
            TSCLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", boxVo.PN);
            TSCLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", content3);
            TSCLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", boxVo.LotNo);
            TSCLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            TSCLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            TSCLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", content5);
            TSCLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", boxVo.Qty);
            TSCLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", content6);
            TSCLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", boxVo.DWGRev);
            TSCLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            TSCLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            TSCLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", content7);
            TSCLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + boxVo.SerialNo));
            //TSCLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
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
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("105", "95", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(70, 70, 65, 0, 0, 0, "标楷体", productVo.CompName);

            TSCLIB_DLL.sendcommand("ELLIPSE 800,50,200,80,6");
            TSCLIB_DLL.windowsfont(820, 60, 60, 0, 2, 0, "标楷体", "ROHS");
            TSCLIB_DLL.sendcommand("BOX 1060,50,1170,130,6");
            TSCLIB_DLL.windowsfont(1080, 60, 60, 0, 2, 0, "标楷体", "HF");

            TSCLIB_DLL.windowsfont(100, 220, 60, 0, 0, 0, "标楷体", lblPO);
            TSCLIB_DLL.barcode("400", "220", "128", "50", "2", "0", "2", "4", productVo.OrderNo);
            TSCLIB_DLL.sendcommand("QRCODE 950,750,L,8,A,0,M2,S3,\"" + qrCode + "\"");
            TSCLIB_DLL.windowsfont(100, 320, 60, 0, 0, 0, "标楷体", lblPn);
            TSCLIB_DLL.windowsfont(400, 320, 60, 0, 0, 0, "标楷体", productVo.PN);
            TSCLIB_DLL.windowsfont(100, 420, 60, 0, 0, 0, "标楷体", lblLotNo);
            TSCLIB_DLL.windowsfont(400, 420, 60, 0, 0, 0, "标楷体", productVo.LotNo);
            TSCLIB_DLL.windowsfont(100, 520, 60, 0, 0, 0, "标楷体", lblDate);
            TSCLIB_DLL.windowsfont(400, 520, 60, 0, 0, 0, "标楷体", _dateValPrint);
            TSCLIB_DLL.windowsfont(100, 620, 60, 0, 0, 0, "标楷体", lblQty);
            TSCLIB_DLL.windowsfont(400, 620, 60, 0, 0, 0, "标楷体", productVo.Qty);
            TSCLIB_DLL.windowsfont(100, 720, 60, 0, 0, 0, "标楷体", lblDwgrev);
            TSCLIB_DLL.windowsfont(400, 720, 60, 0, 0, 0, "标楷体", productVo.DWGRev);
            TSCLIB_DLL.windowsfont(100, 820, 60, 0, 0, 0, "标楷体", "PERCO PN：");
            TSCLIB_DLL.windowsfont(400, 820, 60, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            TSCLIB_DLL.windowsfont(100, 920, 60, 0, 0, 0, "标楷体", lblSno);
            TSCLIB_DLL.windowsfont(400, 920, 60, 0, 0, 0, "标楷体", ("GP" + productVo.SerialNo));
            //TSCLIB_DLL.barcode("400", "920", "128", "50", "2", "0", "2", "4", _barcodetxt);

            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrint200(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.sendcommand("DIRECTION 1,0");
            TSCLIB_DLL.sendcommand("REFERENCE 0,0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.sendcommand("BOX 30,30,550,70,4");
            TSCLIB_DLL.windowsfont(50, 40, 30, 0, 0, 0, "标楷体", orderNo);
            TSCLIB_DLL.barcode("250", "40", "128", "30", "0", "0", "2", "4", orderNo);
            TSCLIB_DLL.windowsfont(500, 42, 30, 0, 0, 0, "标楷体", qty);
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
        }

        public static void OrderNoBarCodePrintNoBox200(string orderNo, string qty)
        {
            string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("70", "24", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.sendcommand("DIRECTION 1,0");
            TSCLIB_DLL.sendcommand("REFERENCE 0,0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(50, 40, 30, 0, 0, 0, "标楷体", orderNo);
            TSCLIB_DLL.barcode("250", "40", "128", "30", "0", "0", "2", "4", orderNo);
            TSCLIB_DLL.printlabel("1", qty);
            TSCLIB_DLL.closeport();
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
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.sendcommand("DIRECTION 1,0");
            TSCLIB_DLL.sendcommand("REFERENCE 0,0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(45, 45, 40, 0, 0, 0, "标楷体", boxVo.CompName);
            TSCLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            TSCLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            TSCLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            TSCLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", boxVo.PN);
            TSCLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            TSCLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", boxVo.LotNo);
            TSCLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", lblDate);
            TSCLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            TSCLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            TSCLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", boxVo.Qty);
            TSCLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            TSCLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", boxVo.DWGRev);
            TSCLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            TSCLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", boxVo.GPPrdName.Substring(0, 2) + boxVo.SPC);
            TSCLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            TSCLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", ("GP" + boxVo.SerialNo));
            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
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
            TSCLIB_DLL.openport(printSet);
            TSCLIB_DLL.setup("100", "95", "3", "12", "0", "2.5", "0");
            TSCLIB_DLL.sendcommand("DIRECTION 1,0");
            TSCLIB_DLL.sendcommand("REFERENCE 0,0");
            TSCLIB_DLL.clearbuffer();
            TSCLIB_DLL.windowsfont(65, 45, 40, 0, 0, 0, "标楷体", productVo.CompName);
            TSCLIB_DLL.windowsfont(65, 150, 40, 0, 0, 0, "标楷体", content);
            TSCLIB_DLL.sendcommand("QRCODE 600,500,L,8,A,0,M2,S3,\"" + str + "\"");
            TSCLIB_DLL.windowsfont(65, 220, 40, 0, 0, 0, "标楷体", content2);
            TSCLIB_DLL.windowsfont(250, 220, 40, 0, 0, 0, "标楷体", productVo.PN);
            TSCLIB_DLL.windowsfont(65, 285, 40, 0, 0, 0, "标楷体", content3);
            TSCLIB_DLL.windowsfont(250, 285, 40, 0, 0, 0, "标楷体", productVo.LotNo);
            TSCLIB_DLL.windowsfont(65, 350, 40, 0, 0, 0, "标楷体", content4);
            TSCLIB_DLL.windowsfont(250, 350, 40, 0, 0, 0, "标楷体", _dateValPrint);
            TSCLIB_DLL.windowsfont(65, 415, 40, 0, 0, 0, "标楷体", content5);
            TSCLIB_DLL.windowsfont(250, 415, 40, 0, 0, 0, "标楷体", productVo.Qty);
            TSCLIB_DLL.windowsfont(65, 480, 40, 0, 0, 0, "标楷体", content6);
            TSCLIB_DLL.windowsfont(250, 480, 40, 0, 0, 0, "标楷体", productVo.DWGRev);
            TSCLIB_DLL.windowsfont(65, 535, 40, 0, 0, 0, "标楷体", "PERCO PN：");
            TSCLIB_DLL.windowsfont(250, 535, 40, 0, 0, 0, "标楷体", productVo.GPPrdName.Substring(0, 2) + productVo.SPC);
            TSCLIB_DLL.windowsfont(65, 600, 40, 0, 0, 0, "标楷体", content7);
            TSCLIB_DLL.barcode("250", "600", "128", "40", "2", "0", "2", "4", _barcodetxt);

            TSCLIB_DLL.printlabel("1", "1");
            TSCLIB_DLL.closeport();
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
