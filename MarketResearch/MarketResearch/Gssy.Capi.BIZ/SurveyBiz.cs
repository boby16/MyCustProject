using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Gssy.Capi.Common;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000024 RID: 36
	public class SurveyBiz
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00003633 File Offset: 0x00001833
		// (set) Token: 0x060003DD RID: 989 RVA: 0x0000363B File Offset: 0x0000183B
		public SurveyMain MySurvey { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00003644 File Offset: 0x00001844
		// (set) Token: 0x060003DF RID: 991 RVA: 0x0000364C File Offset: 0x0000184C
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00003655 File Offset: 0x00001855
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000365D File Offset: 0x0000185D
		public List<V_SurveyQC> QVSurveyQC { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00003666 File Offset: 0x00001866
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000366E File Offset: 0x0000186E
		public List<V_Summary> QVSummary { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00003677 File Offset: 0x00001877
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000367F File Offset: 0x0000187F
		public List<SurveyMain> QMain { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00003688 File Offset: 0x00001888
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00003690 File Offset: 0x00001890
		public string RecordFileName { get; set; }

		// Token: 0x060003E9 RID: 1001 RVA: 0x00025198 File Offset: 0x00023398
		public bool ExistSurvey(string string_0)
		{
			return this.oSurveyMainDal.Exists(string_0);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00003699 File Offset: 0x00001899
		public void GetSurveyMain(int int_0)
		{
			this.QMain = this.oSurveyMainDal.GetSurveyMain(int_0);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000251B4 File Offset: 0x000233B4
		public List<SurveyMain> GetSurveyMainList(int int_0)
		{
			return this.oSurveyMainDal.GetSurveyMain(int_0);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000036AD File Offset: 0x000018AD
		public void GetBySurveyId(string string_0)
		{
			this.MySurvey = this.oSurveyMainDal.GetBySurveyId(string_0);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000251D0 File Offset: 0x000233D0
		public SurveyMain GetSurveyMainListBySurveyId(string string_0)
		{
			return this.oSurveyMainDal.GetBySurveyId(string_0);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000251EC File Offset: 0x000233EC
		public List<SurveyAnswer> GetBySysAudio(string string_0)
		{
			return this.oSurveyAnswerDal.GetListRecord(string_0);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0002520C File Offset: 0x0002340C
		public SurveySequence GetAudioByPageId(string string_0, int int_0, string string_1)
		{
			return this.oSurveySequenceDal.GetAudioByPageId(string_0, int_0, string_1);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0002522C File Offset: 0x0002342C
		public List<SurveyAnswer> GetBySysPhoto(string string_0)
		{
			return this.oSurveyAnswerDal.GetListByCode(string_0, global::GClass0.smethod_0("_Şɘ͟э՞ٙݕࡌौ੖୎"));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00025254 File Offset: 0x00023454
		public void GetSurveyyQC(string string_0)
		{
			this.QVSurveyQC = this.oVSurveyQCDal.GetListBySurveyId(string_0);
			for (int i = 0; i < this.QVSurveyQC.Count; i++)
			{
				if (this.QVSurveyQC[i].DETAIL_ID != global::GClass0.smethod_0("") && this.QVSurveyQC[i].CODE != global::GClass0.smethod_0(""))
				{
					this.QVSurveyQC[i].CODE_TEXT = this.oSurveyDetailDal.GetCodeText(this.QVSurveyQC[i].DETAIL_ID, this.QVSurveyQC[i].CODE);
				}
				if (this.QVSurveyQC[i].DETAIL_ID == global::GClass0.smethod_0("") && this.QVSurveyQC[i].CODE != global::GClass0.smethod_0(""))
				{
					this.QVSurveyQC[i].CODE_TEXT = this.QVSurveyQC[i].CODE;
				}
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace(global::GClass0.smethod_0("?ŀȿ"), global::GClass0.smethod_0(""));
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace(global::GClass0.smethod_0("8Ĭɀ̿"), global::GClass0.smethod_0(""));
				this.QVSurveyQC[i].QUESTION_TITLE = this.QVSurveyQC[i].QUESTION_TITLE.Replace(global::GClass0.smethod_0("8Łɐ̿"), global::GClass0.smethod_0(""));
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00025430 File Offset: 0x00023630
		public void GetSummary(string string_0)
		{
			this.QVSummary = this.oVSummaryDal.GetListBySurveyId(string_0);
			for (int i = 0; i < this.QVSummary.Count; i++)
			{
				if (this.QVSummary[i].DETAIL_ID != global::GClass0.smethod_0("") && this.QVSummary[i].CODE != global::GClass0.smethod_0(""))
				{
					this.QVSummary[i].CODE_TEXT = this.oSurveyDetailDal.GetCodeText(this.QVSummary[i].DETAIL_ID, this.QVSummary[i].CODE);
				}
				if (this.QVSummary[i].DETAIL_ID == global::GClass0.smethod_0("") && this.QVSummary[i].CODE != global::GClass0.smethod_0(""))
				{
					this.QVSummary[i].CODE_TEXT = this.QVSummary[i].CODE;
				}
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000036C1 File Offset: 0x000018C1
		public void UpdateSurvey()
		{
			this.oSurveyMainDal.Update(this.MySurvey);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0002555C File Offset: 0x0002375C
		public void UpdateSurveyLastTime(string string_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.LAST_START_TIME = new DateTime?(DateTime.Now);
			bySurveyId.LAST_END_TIME = new DateTime?(DateTime.Now);
			this.oSurveyMainDal.Update(bySurveyId);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000255A4 File Offset: 0x000237A4
		public void AddSurvey(string string_0, string string_1, string string_2, string string_3, string string_4, string string_5)
		{
			this.oSurveyMainDal.Add(string_0, string_1, string_2, string_3, string_4);
			this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Xşɛ͞т՟ٚ݀ࡂॖ੄"), string.Format(global::GClass0.smethod_0("uĽȶͲѳհٱܨࡋैਫ୧౦ർ"), DateTime.Now), 0);
			this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("Ańɂ͙ыՔٓݍࡃज़ਜ਼୓ౙൖ๐གၐᅕ"), string.Format(global::GClass0.smethod_0("lĦȯͭѪի٨ܿࡂृਢ୨౯പแཀွᅫቨጾᑰᕱᙼ"), DateTime.Now), 0);
			this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("BŅɝ͘шՕٔݜࡌग़੔୏ొൊ๜ཋ၅"), string_1, 0);
			string byCodeText = this.oSurveyConfigDal.GetByCodeText(global::GClass0.smethod_0("VņɇͬѦդ"));
			this.oSurveyAnswerDal.AddOne(string_0, global::GClass0.smethod_0("UŇɜ͋х"), byCodeText, 0);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0002566C File Offset: 0x0002386C
		public int GetEndType(string string_0, int int_0, string string_1)
		{
			int int_ = int_0 - 1;
			SurveySequence bySequenceID = this.oSurveySequenceDal.GetBySequenceID(string_0, int_);
			int result;
			if (bySequenceID.PAGE_ID == string_1)
			{
				result = 1;
			}
			else
			{
				result = 2;
			}
			return result;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000256A8 File Offset: 0x000238A8
		public bool CloseSurveyByExit(string string_0, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.IS_FINISH = int_0;
			bySurveyId.END_TIME = new DateTime?(DateTime.Now);
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, global::GClass0.smethod_0("Xşɛ͞т՟ٚݑࡐे੓"));
			bySurveyId.USER_ID = one.CODE;
			this.oSurveyMainDal.Update(bySurveyId);
			if (this.RecordFileName != global::GClass0.smethod_0("") && this.RecordFileName != null)
			{
				this.oSurveyAnswerDal.AddOneNoUpdate(string_0, global::GClass0.smethod_0("_Şɘ͟э՞݄ٙࡑेੋ୎"), this.RecordFileName, 0);
			}
			return true;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0002574C File Offset: 0x0002394C
		public void CloseSurvey(string string_0, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.IS_FINISH = int_0;
			if (bySurveyId.START_TIME == bySurveyId.END_TIME)
			{
				bySurveyId.END_TIME = new DateTime?(DateTime.Now);
			}
			bySurveyId.LAST_END_TIME = new DateTime?(DateTime.Now);
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, global::GClass0.smethod_0("Xşɛ͞т՟ٚݑࡐे੓"));
			bySurveyId.USER_ID = one.CODE;
			this.oSurveyMainDal.Update(bySurveyId);
			if (int_0 != 3)
			{
				this.method_0(bySurveyId);
				if (this.RecordFileName != global::GClass0.smethod_0(""))
				{
					this.oSurveyAnswerDal.AddOneNoUpdate(string_0, global::GClass0.smethod_0("_Şɘ͟э՞݄ٙࡑेੋ୎"), this.RecordFileName, 0);
				}
				List<SurveyRandom> listBySurveyId = this.oSurveyRandomDal.GetListBySurveyId(string_0);
				bool flag = false;
				string a = global::GClass0.smethod_0("");
				using (List<SurveyRandom>.Enumerator enumerator = listBySurveyId.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SurveyRandom surveyRandom = enumerator.Current;
						if (a != surveyRandom.QUESTION_SET)
						{
							flag = this.oSurveyAnswerDal.Exists(string_0, surveyRandom.QUESTION_SET, global::GClass0.smethod_0("]œ"));
							a = surveyRandom.QUESTION_SET;
						}
						if (!flag)
						{
							SurveyAnswer surveyAnswer = new SurveyAnswer();
							surveyAnswer.SURVEY_ID = string_0;
							surveyAnswer.QUESTION_NAME = surveyRandom.QUESTION_SET + global::GClass0.smethod_0("]œ") + surveyRandom.RANDOM_INDEX.ToString();
							surveyAnswer.CODE = surveyRandom.CODE;
							surveyAnswer.MULTI_ORDER = surveyRandom.RANDOM_INDEX;
							surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
							surveyAnswer.SEQUENCE_ID = 90000;
							surveyAnswer.SURVEY_GUID = surveyRandom.QUESTION_SET;
							surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
							this.oSurveyAnswerDal.Add(surveyAnswer);
						}
					}
					return;
				}
			}
			if (this.RecordFileName != global::GClass0.smethod_0(""))
			{
				this.oSurveyAnswerDal.AddOneNoUpdate(string_0, global::GClass0.smethod_0("_Şɘ͟э՞݄ٙࡑेੋ୎"), this.RecordFileName, 0);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000259CC File Offset: 0x00023BCC
		private void method_0(SurveyMain surveyMain_0)
		{
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ"), surveyMain_0.SURVEY_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("\\Ōɚ͔яՊيݜࡋॅ"), surveyMain_0.VERSION_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Rŕɀ͖ќՋم"), surveyMain_0.USER_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Dŏɑ͝ќՋم"), surveyMain_0.CITY_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Yŝɉ͕ђ՚ِ݊ࡏॄ"), surveyMain_0.START_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Mŉɂ͚ѐՊُ݄"), surveyMain_0.END_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Cŏɞ͘єՙٝ݉ࡕ॒ਗ਼୐ొ൏ไ"), surveyMain_0.LAST_START_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Aōɘ͞іՍى݂࡚ॐ੊୏ౄ"), surveyMain_0.LAST_END_TIME.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Hşɛ͗їՇق݁࡜ो੅"), surveyMain_0.CUR_PAGE_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Sņɜ͎рՎٕ݈ࡗॄ੓ୗౖെ์ཕ"), surveyMain_0.CIRCLE_A_CURRENT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Mńɞ͈цՌ݆࡙ٗॆੋୖౌൕ"), surveyMain_0.CIRCLE_A_COUNT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Sņɜ͎рՎٕ݋ࡗॄ੓ୗౖെ์ཕ"), surveyMain_0.CIRCLE_B_CURRENT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Mńɞ͈цՌ࡙ٗ݅ॆੋୖౌൕ"), surveyMain_0.CIRCLE_B_COUNT.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("@śɘ̀ьՊيݑࡉ"), surveyMain_0.IS_FINISH.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Xŏɘ͝тՈن݁࡜ो੅"), surveyMain_0.SEQUENCE_ID.ToString(), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Xşɛ͞т՟ٚ݃ࡖो੅"), surveyMain_0.SURVEY_GUID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("JńɎ̓ыՐٜ݋ࡅ"), surveyMain_0.CLIENT_ID, 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("Zśɇ͍уՆِݜࡋॅ"), surveyMain_0.PROJECT_ID, 0);
			string arg = surveyMain_0.END_TIME.ToString();
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("CŚɜ͛щՒٕݏࡁॕ੕୑౛െ์ཅ"), string.Format(global::GClass0.smethod_0("lĦȯͭѪի٨ܿࡂृਢ୨౯പแཀွᅫቨጾᑰᕱᙼ"), arg), 0);
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("\\śɟ͚юՓٖ݄ࡆॕੑ୛ెൌๅ"), string.Format(global::GClass0.smethod_0("lĦȯͭѪի٨ܿࡂृਢ୨౯പแཀွᅫቨጾᑰᕱᙼ"), arg), 0);
			string text = surveyMain_0.SURVEY_GUID.ToString();
			if (surveyMain_0.VERSION_ID.IndexOf(global::GClass0.smethod_0("歠帍灉")) > -1)
			{
				text += global::GClass0.smethod_0("歠帍灉");
			}
			else
			{
				text += global::GClass0.smethod_0("浈諗灉");
			}
			string string_ = EncryptTool.Encrypt(text, global::GClass0.smethod_0("EłɨͦѶպٲݵ"));
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("BŅɝ͘шՕٔ݃ࡇॆੂ୔ౚേ์ཆ၄"), string_, 0);
			TimeSpan timeSpan = surveyMain_0.END_TIME.Value - surveyMain_0.START_TIME.Value;
			this.oSurveyAnswerDal.AddOne(surveyMain_0.SURVEY_ID, global::GClass0.smethod_0("CŚɜ͛щՒٕ݊ࡇ॔੒୚౐ൊ๏ང"), ((int)timeSpan.TotalMinutes).ToString(), 0);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public string GetUserID(string string_0)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, global::GClass0.smethod_0("Xşɛ͞т՟ٚݑࡐे੓"));
			return one.CODE;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00025E14 File Offset: 0x00024014
		public void UpdateSurvey(string string_0, string string_1)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			bySurveyId.CUR_PAGE_ID = string_1;
			this.oSurveyMainDal.Update(bySurveyId);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000036D4 File Offset: 0x000018D4
		public void SaveAnswer(string string_0, int int_0, string[,] string_1, int int_1)
		{
			this.oSurveyAnswerDal.SaveByArray(string_0, int_0, string_1, int_1);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00025E44 File Offset: 0x00024044
		public void SaveOneAnswer(string string_0, int int_0, string string_1, string string_2)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.SURVEY_ID = string_0;
			surveyAnswer.QUESTION_NAME = string_1;
			surveyAnswer.CODE = string_2;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
			surveyAnswer.SEQUENCE_ID = int_0;
			surveyAnswer.SURVEY_GUID = global::GClass0.smethod_0("");
			surveyAnswer.BEGIN_DATE = new DateTime?(DateTime.Now);
			this.oSurveyAnswerDal.SaveOneAnswer(surveyAnswer);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000036E6 File Offset: 0x000018E6
		public void ClearPageAnswer(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveySequenceDal.DeleteBySequenceID(string_0, int_0);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00025EB8 File Offset: 0x000240B8
		public string GetOneAnswerCode(string string_0, string string_1, string string_2, int int_0)
		{
			string result = global::GClass0.smethod_0("");
			try
			{
				SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
				if (int_0 == 1)
				{
					result = one.CODE;
				}
				else
				{
					string code = one.CODE;
					result = this.oSurveyDetailDal.GetCodeText(string_2, code);
				}
			}
			catch (Exception)
			{
				result = global::GClass0.smethod_0("");
			}
			return result;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00025F28 File Offset: 0x00024128
		public List<SurveyAnswer> GetListBySequenceId(string string_0, int int_0)
		{
			return this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00025F44 File Offset: 0x00024144
		public string GetInfoBySequenceId(string string_0, int int_0)
		{
			string text = global::GClass0.smethod_0("");
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
			foreach (SurveyAnswer surveyAnswer in list)
			{
				text = text + Environment.NewLine + string.Format(global::GClass0.smethod_0("颉勧紙笏зշػݷࠩनਧ灒摍๸༳ၼ"), surveyAnswer.QUESTION_NAME, surveyAnswer.CODE);
			}
			return text;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00025FD8 File Offset: 0x000241D8
		public string GetNextSurveyId(string string_0, int int_0)
		{
			string text = global::GClass0.smethod_0("");
			string text2 = global::GClass0.smethod_0("");
			string text3 = global::GClass0.smethod_0("");
			text = this.oSurveyMainDal.GetMaxSurveyIDByCity(string_0);
			for (int i = 0; i < int_0 - 1; i++)
			{
				text2 += global::GClass0.smethod_0("1");
			}
			text2 += global::GClass0.smethod_0("0");
			for (int j = 0; j < int_0; j++)
			{
				text3 += global::GClass0.smethod_0("8");
			}
			if (text == global::GClass0.smethod_0(""))
			{
				text = string_0 + text2;
			}
			else if (text.Substring(1, int_0) == text3)
			{
				text = global::GClass0.smethod_0("");
			}
			else
			{
				text = (int.Parse(text) + 1).ToString();
			}
			return text;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000260B8 File Offset: 0x000242B8
		public string GetCityCode(string string_0)
		{
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, global::GClass0.smethod_0("GŊɖ͘"));
			return one.CODE;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000260E4 File Offset: 0x000242E4
		public void AddSurvyeSync(string string_0, string string_1, int int_0)
		{
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			SurveySync surveySync = new SurveySync();
			surveySync.SURVEY_ID = string_0;
			surveySync.SURVEY_GUID = bySurveyId.SURVEY_GUID;
			surveySync.SYNC_DATE = new DateTime?(DateTime.Now);
			surveySync.SYNC_NOTE = string_1;
			surveySync.SYNC_STATE = int_0;
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			surveySyncDal.Add(surveySync);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00026144 File Offset: 0x00024344
		public bool CheckUploadSync(string string_0)
		{
			bool result = false;
			SurveyMain bySurveyId = this.oSurveyMainDal.GetBySurveyId(string_0);
			SurveySyncDal surveySyncDal = new SurveySyncDal();
			if (surveySyncDal.CheckObjectExist(string_0, bySurveyId.SURVEY_GUID))
			{
				SurveySync bySuveyID_GUID = surveySyncDal.GetBySuveyID_GUID(string_0, bySurveyId.SURVEY_GUID);
				if (bySuveyID_GUID.SYNC_STATE == 1)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00026194 File Offset: 0x00024394
		public string GetCodeFromAnswerList(List<SurveyAnswer> list_0, string string_0)
		{
			SurveyBiz.Class9 @class = new SurveyBiz.Class9();
			@class.QuestionName = string_0;
			string result = global::GClass0.smethod_0("");
			int num = list_0.FindIndex(new Predicate<SurveyAnswer>(@class.method_0));
			if (num > -1)
			{
				result = list_0[num].CODE;
			}
			return result;
		}

		// Token: 0x040001BD RID: 445
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x040001BE RID: 446
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x040001BF RID: 447
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x040001C0 RID: 448
		private SurveySequenceDal oSurveySequenceDal = new SurveySequenceDal();

		// Token: 0x040001C1 RID: 449
		private V_SurveyQCDal oVSurveyQCDal = new V_SurveyQCDal();

		// Token: 0x040001C2 RID: 450
		private V_SummaryDal oVSummaryDal = new V_SummaryDal();

		// Token: 0x040001C3 RID: 451
		private SurveyConfigDal oSurveyConfigDal = new SurveyConfigDal();

		// Token: 0x040001C4 RID: 452
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x040001C5 RID: 453
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x02000032 RID: 50
		[CompilerGenerated]
		private sealed class Class9
		{
			// Token: 0x06000458 RID: 1112 RVA: 0x00027774 File Offset: 0x00025974
			internal bool method_0(SurveyAnswer surveyAnswer_0)
			{
				return surveyAnswer_0.QUESTION_NAME == this.QuestionName;
			}

			// Token: 0x040001F1 RID: 497
			public string QuestionName;
		}
	}
}
