using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000028 RID: 40
	public class SurveytoXml
	{
		// Token: 0x06000430 RID: 1072 RVA: 0x00026F58 File Offset: 0x00025158
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
				string text2 = global::GClass0.smethod_0("R") + string_0 + global::GClass0.smethod_0("HĪɧͣѵ");
				string path = text + global::GClass0.smethod_0("]") + text2;
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("QŅɋ̦іեٵݧ࠻"), ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0002704C File Offset: 0x0002524C
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
				string str = global::GClass0.smethod_0("R") + string_0 + global::GClass0.smethod_0("DĪɧͣѵ");
				string path = text + global::GClass0.smethod_0("]") + str;
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("QŅɋ̦іեٵݧ࠻"), ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00027134 File Offset: 0x00025334
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
				string path = text + global::GClass0.smethod_0("^Œ") + string_0 + global::GClass0.smethod_0("WĪɧͣѵ");
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("QŅɋ̦іեٵݧ࠻"), ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000271F0 File Offset: 0x000253F0
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
				string path = text + global::GClass0.smethod_0("^Œ") + string_0 + global::GClass0.smethod_0("VĪɧͣѵ");
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("QŅɋ̦іեٵݧ࠻"), ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000272B4 File Offset: 0x000254B4
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
				string path = text + global::GClass0.smethod_0("^Œ") + string_0 + global::GClass0.smethod_0("MĪɧͣѵ");
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("QŅɋ̦іեٵݧ࠻"), ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00027378 File Offset: 0x00025578
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("Cźɼͻѩղهݨࡡ३ਦୗౡൢ๦༻"), ex.Message);
				SurveyMain surveyMain2 = new SurveyMain();
				bool_0 = false;
				result = surveyMain2;
			}
			return result;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000273F4 File Offset: 0x000255F4
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("AŤɢ͹ѫմٍݥࡹॾ੭୵దൗ๡རၦᄻ"), ex.Message);
				List<SurveyAnswer> list2 = new List<SurveyAnswer>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00027470 File Offset: 0x00025670
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("AŤɢ͹ѫմٞݪࡤ७੧୪దൗ๡རၦᄻ"), ex.Message);
				List<SurveyRandom> list2 = new List<SurveyRandom>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000274EC File Offset: 0x000256EC
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("AŤɢ͹ѫմٞݪࡤ७੧୪దൗ๡རၦᄻ"), ex.Message);
				List<SurveySequence> list2 = new List<SurveySequence>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00027568 File Offset: 0x00025768
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
				Logging.Error.WriteLog(global::GClass0.smethod_0("AŤɢ͹ѫմٍݿࡾ२੫୯దൗ๡རၦᄻ"), ex.Message);
				List<SurveyAttach> list2 = new List<SurveyAttach>();
				bool_0 = false;
				result = list2;
			}
			return result;
		}

		// Token: 0x040001D6 RID: 470
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x040001D7 RID: 471
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040001D8 RID: 472
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x040001D9 RID: 473
		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		// Token: 0x040001DA RID: 474
		private SurveyAttachDal oSurveyAttachDal = new SurveyAttachDal();

		// Token: 0x040001DB RID: 475
		public int OutPutType = 1;

		// Token: 0x040001DC RID: 476
		public string OutputPath = global::GClass0.smethod_0("[ŉɰͰѳշٵ");
	}
}
