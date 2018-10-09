using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class SurveytoXml
	{
		public bool SaveSurveyMain(string string_0, string string_1, SurveyMain surveyMain_0 = null)
		{
			if (surveyMain_0 == null)
			{
				surveyMain_0 = this.oSurveyMainDal.GetBySurveyId(string_0);
			}
			bool result;
			try
			{
				string text = string_1 + this.OutputPath;
				string text2 = "S" + string_0 + "M.dat";
				string path = text + "\\" + text2;
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				int outPutType = this.OutPutType;
				if (outPutType != 2)
				{
					if (outPutType != 3)
					{
						XmlSerializer xmlSerializer = new XmlSerializer(typeof(SurveyMain));
						FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
						xmlSerializer.Serialize(stream, surveyMain_0);
					}
					else
					{
						BinarySerializerHelper.Serialize<SurveyMain>(surveyMain_0, text, text2);
					}
				}
				else
				{
					XmlSerializerHelper.Serialize<SurveyMain>(surveyMain_0, text, text2);
				}
				result = true;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("XML Save:", ex.Message);
				result = false;
			}
			return result;
		}

		public bool SaveSurveyAnswer(string string_0, string string_1, List<SurveyAnswer> list_0 = null, bool bool_0 = true)
		{
			if (list_0 == null)
			{
				list_0 = this.oSurveyAnswerDal.GetListBySurveyId(string_0);
			}
			bool result;
			try
			{
				string text = bool_0 ? (string_1 + this.OutputPath) : string_1;
				string str = "S" + string_0 + "A.dat";
				string path = text + "\\" + str;
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				int outPutType = this.OutPutType;
				if (outPutType != 2 && outPutType != 3)
				{
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyAnswer>));
					FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
					xmlSerializer.Serialize(stream, list_0);
				}
				result = true;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("XML Save:", ex.Message);
				result = false;
			}
			return result;
		}

		public bool SaveSurveyRandom(string string_0, string string_1, List<SurveyRandom> list_0 = null)
		{
			if (list_0 == null)
			{
				list_0 = this.oSurveyRandomDal.GetListBySurveyId(string_0);
			}
			bool result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyRandom>));
				string text = string_1 + this.OutputPath;
				string path = text + "\\S" + string_0 + "R.dat";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				xmlSerializer.Serialize(stream, list_0);
				result = true;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("XML Save:", ex.Message);
				result = false;
			}
			return result;
		}

		public bool SaveSurveySequence(string string_0, string string_1, List<SurveySequence> list_0 = null, bool bool_0 = true)
		{
			if (list_0 == null)
			{
				list_0 = this.oSurveySequenceDal.GetListBySurveyId(string_0);
			}
			bool result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveySequence>));
				string text = bool_0 ? (string_1 + this.OutputPath) : string_1;
				string path = text + "\\S" + string_0 + "S.dat";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				xmlSerializer.Serialize(stream, list_0);
				result = true;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("XML Save:", ex.Message);
				result = false;
			}
			return result;
		}

		public bool SaveSurveyAttach(string string_0, string string_1, List<SurveyAttach> list_0 = null, bool bool_0 = true)
		{
			if (list_0 == null)
			{
				list_0 = this.oSurveyAttachDal.GetListBySurveyId(string_0);
			}
			bool result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyAttach>));
				string text = bool_0 ? (string_1 + this.OutputPath) : string_1;
				string path = text + "\\S" + string_0 + "H.dat";
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				Stream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
				xmlSerializer.Serialize(stream, list_0);
				result = true;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("XML Save:", ex.Message);
				result = false;
			}
			return result;
		}

		public SurveyMain ReadSurveyMain(string string_0, out bool bool_0)
		{
			SurveyMain result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(SurveyMain));
				Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				SurveyMain surveyMain = xmlSerializer.Deserialize(stream) as SurveyMain;
				bool_0 = true;
				result = surveyMain;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("SurveyMain Read:", ex.Message);
				SurveyMain surveyMain2 = new SurveyMain();
				bool_0 = false;
				result = surveyMain2;
			}
			return result;
		}

		public List<SurveyAnswer> ReadSurveyAnswer(string string_0, out bool bool_0)
		{
			List<SurveyAnswer> result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyAnswer>));
				Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				List<SurveyAnswer> list = xmlSerializer.Deserialize(stream) as List<SurveyAnswer>;
				bool_0 = true;
				result = list;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("SurveyAnswer Read:", ex.Message);
				List<SurveyAnswer> list2 = new List<SurveyAnswer>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		public List<SurveyRandom> ReadSurveyRandom(string string_0, out bool bool_0)
		{
			List<SurveyRandom> result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyRandom>));
				Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				List<SurveyRandom> list = xmlSerializer.Deserialize(stream) as List<SurveyRandom>;
				bool_0 = true;
				result = list;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("SurveyRandom Read:", ex.Message);
				List<SurveyRandom> list2 = new List<SurveyRandom>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		public List<SurveySequence> ReadSurveySequence(string string_0, out bool bool_0)
		{
			List<SurveySequence> result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveySequence>));
				Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				List<SurveySequence> list = xmlSerializer.Deserialize(stream) as List<SurveySequence>;
				bool_0 = true;
				result = list;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("SurveyRandom Read:", ex.Message);
				List<SurveySequence> list2 = new List<SurveySequence>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		public List<SurveyAttach> ReadSurveyAttach(string string_0, out bool bool_0)
		{
			List<SurveyAttach> result;
			try
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<SurveyAttach>));
				Stream stream = new FileStream(string_0, FileMode.Open, FileAccess.Read, FileShare.Read);
				List<SurveyAttach> list = xmlSerializer.Deserialize(stream) as List<SurveyAttach>;
				bool_0 = true;
				result = list;
			}
			catch (Exception ex)
			{
				Logging.Error.WriteLog("SurveyAttach Read:", ex.Message);
				List<SurveyAttach> list2 = new List<SurveyAttach>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		public int OutPutType = 1;

		public string OutputPath = "\\Output";
	}
}
