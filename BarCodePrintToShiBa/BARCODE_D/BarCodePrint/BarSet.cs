using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;

namespace BarCodePrintTSB
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class BarSet
	{
		public BarSet()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// 打印条码
		/// </summary>
		/// <param name="dsBarCode">打印数据</param>
		/// <param name="_prdtType">条码类别(EX:0、板材；1、成品；2、外箱)</param>
        /// <param name="_format">打印模板(EX:1:标准版；2、简约版；3、列印编号；4、特殊版；5、富士康)</param>
		/// <returns></returns>
		public bool PrintBarCode(System.Data.DataSet dsBarCode,string _prdtType, string _format)
		{
			for (int i = 0; i < dsBarCode.Tables["BARCODE1"].Rows.Count; i++)
			{
                ToShiBaLib _tsc = new ToShiBaLib();
				_tsc.barCodePrint(dsBarCode.Tables["BARCODE1"].Rows[i], _prdtType, _format);
            }
			return false;
		}

        #region 打印机设定
        public void SetPrintSet(string AppKey, string AppValue, string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            XmlNode xNode;
            XmlElement xElem1;
            XmlElement xElem2;
            xNode = xDoc.SelectSingleNode("//appSettings");
            if (xNode == null)
            {
                XmlElement xeNew = xDoc.CreateElement("appSettings");
                xDoc.DocumentElement.AppendChild(xeNew);
                xNode = xDoc.DocumentElement.LastChild;
            }
            xElem1 = (XmlElement)xNode.SelectSingleNode("//add[@key='" + AppKey + "']");
            if (xElem1 != null)
            {
                xElem1.SetAttribute("value", AppValue);
            }
            else
            {
                xElem2 = xDoc.CreateElement("add");
                xElem2.SetAttribute("key", AppKey);
                xElem2.SetAttribute("value", AppValue);
                xNode.AppendChild(xElem2);
            }
            xDoc.Save(filename);
        }

        public string GetPrintSet(string filename)
        {
            string _printSet = "";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            string _nodeName = "/configuration/appSettings/add[@key=\"PrintSetPath\"]/@value";
            XmlNode xNode;
            xNode = xDoc.SelectSingleNode(_nodeName);
            if (xNode != null)
            {
                _printSet = xNode.InnerText;
            }
            return _printSet;
        }


        public string GetPrintSet(string filename,string appSet)
        {
            string _printSet = "";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            string _nodeName = "/configuration/appSettings/add[@key=\"" + appSet + "\"]/@value";
            XmlNode xNode;
            xNode = xDoc.SelectSingleNode(_nodeName);
            if (xNode != null)
            {
                _printSet = xNode.InnerText;
            }
            return _printSet;
        }
        #endregion

        #region 远程服务器设定
        public void SetRemoteServer(string AppValue, string filename)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            string _nodeName = "/configuration/applicationSettings/BarCodePrintTSB.Properties.Settings/setting[@name=\"BarCodePrint_onlineService_BarPrintServer\"]";
            XmlNode xNode;
            XmlNode xNode1;
            xNode = xDoc.SelectSingleNode(_nodeName);
            if (xNode != null)
            {
                xNode1 = xNode.SelectSingleNode("./value");
                if (xNode1 != null)
                    xNode1.InnerText = AppValue;
            }
            xDoc.Save(filename);
        }

        public string GetRemoteServer(string filename)
        {
            string _remoteServer = "http://localhost/BarPrintService/BarPrintServer.asmx";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(filename);

            string _nodeName = "/configuration/applicationSettings/BarCodePrintTSB.Properties.Settings/setting[@name=\"BarCodePrint_onlineService_BarPrintServer\"]";
            XmlNode xNode;
            XmlNode xNode1;
            xNode = xDoc.SelectSingleNode(_nodeName);
            if (xNode != null)
            {
                xNode1 = xNode.SelectSingleNode("./value");
                if (xNode1 != null)
                    _remoteServer = xNode1.InnerText;
            }
            return _remoteServer;
        }
        #endregion

        /// <summary>
        /// 长度转换：如：C35->1235
        /// </summary>
        /// <param name="_character"></param>
        /// <returns></returns>
        public int CharacterToNum(string _character)
        {
            if (_character.Length != 3)
                return 0;
            else
            {
                int _num = 0;
                switch (_character.Substring(0, 1).ToUpper())
                {
                    case "A":
                        _num = 10;
                        break;
                    case "B":
                        _num = 11;
                        break;
                    case "C":
                        _num = 12;
                        break;
                    case "D":
                        _num = 13;
                        break;
                    case "E":
                        _num = 14;
                        break;
                    case "F":
                        _num = 15;
                        break;
                    case "G":
                        _num = 16;
                        break;
                    case "H":
                        _num = 17;
                        break;
                    case "I":
                        _num = 18;
                        break;
                    case "J":
                        _num = 19;
                        break;
                    case "K":
                        _num = 20;
                        break;
                    case "L":
                        _num = 21;
                        break;
                    case "M":
                        _num = 22;
                        break;
                    case "N":
                        _num = 23;
                        break;
                    case "O":
                        _num = 24;
                        break;
                    case "P":
                        _num = 25;
                        break;
                    case "Q":
                        _num = 26;
                        break;
                    case "R":
                        _num = 27;
                        break;
                    case "S":
                        _num = 28;
                        break;
                    case "T":
                        _num = 29;
                        break;
                    case "U":
                        _num = 30;
                        break;
                    case "V":
                        _num = 31;
                        break;
                    case "W":
                        _num = 32;
                        break;
                    case "X":
                        _num = 33;
                        break;
                    case "Y":
                        _num = 34;
                        break;
                    case "Z":
                        _num = 35;
                        break;
                    default:
                        _num = Convert.ToInt32(_character.Substring(0, 1));
                        break;
                }
                return _num * 100 + Convert.ToInt32(_character.Substring(1, 2));
            }
        }
    }
}
