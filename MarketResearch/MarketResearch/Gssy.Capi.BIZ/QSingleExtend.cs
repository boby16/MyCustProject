using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000020 RID: 32
	public class QSingleExtend
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00003414 File Offset: 0x00001614
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000341C File Offset: 0x0000161C
		public string QuestionName { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000342D File Offset: 0x0000162D
		public string QuestionTitle { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00003436 File Offset: 0x00001636
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000343E File Offset: 0x0000163E
		public string ParentCode { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00003447 File Offset: 0x00001647
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000344F File Offset: 0x0000164F
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00003458 File Offset: 0x00001658
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00003460 File Offset: 0x00001660
		public SurveyAnswer OneAnswer { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00003469 File Offset: 0x00001669
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00003471 File Offset: 0x00001671
		public List<SurveyAnswer> QAnswers { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000347A File Offset: 0x0000167A
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00003482 File Offset: 0x00001682
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000348B File Offset: 0x0000168B
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00003493 File Offset: 0x00001693
		public SurveyDefine QDefine { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000349C File Offset: 0x0000169C
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000034A4 File Offset: 0x000016A4
		public string OtherCode { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000034AD File Offset: 0x000016AD
		// (set) Token: 0x06000398 RID: 920 RVA: 0x000034B5 File Offset: 0x000016B5
		public string FillText { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000399 RID: 921 RVA: 0x000034BE File Offset: 0x000016BE
		// (set) Token: 0x0600039A RID: 922 RVA: 0x000034C6 File Offset: 0x000016C6
		public DateTime QInitDateTime { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000034CF File Offset: 0x000016CF
		// (set) Token: 0x0600039C RID: 924 RVA: 0x000034D7 File Offset: 0x000016D7
		public string SelectedCode { get; set; }

		// Token: 0x0600039E RID: 926 RVA: 0x00003509 File Offset: 0x00001709
		public void Init(string string_0, int int_0)
		{
			this.QInitDateTime = DateTime.Now;
			this.QDefine = this.oSurveyDefineDal.GetByPageId(string_0, int_0);
			this.QuestionName = this.QDefine.QUESTION_NAME;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00022A8C File Offset: 0x00020C8C
		public void GetExtendDetails(string string_0)
		{
			this.ExtendCodePlace = this.QDefine.MIN_COUNT;
			string string_ = string.Format(global::GClass0.smethod_0("äǓ˙ϑӐ׆ڑޚ࢏ৈ૟௃ೆඊ໺࿝ვᇐዀᏝᓧᗇᛕេ᣶᧲᪽ᯋᳳ᷿ừ´₷⇒⋐⏀ⓒ◛⛝⟏⣆⧊⪭⮱ⲫⶭ⻲⾸ヺㆡ㊥㏥㓭㗦㚡㟣㠐㤚㨘㭜㰒㴔㹙㽐䁗䄅䈐䌘䐖䔑䘅䝐䠋䤇䨞䬘䰂䴄上伜假儃初匐吆唌嘅圿堤奯娠孼尽崨帶張恷慶扽捴搀攷昽朵栬椺橭欩氳派測漦瀣焙爾獵琾畢瘧眲硐祓稝筯籎絈繏罝聎腲艐荀葒蕛虝蜐衸襆詈譞豎贊蹭轭遳酧鉬鍨鑼镫陥需頢餾騺魧鰩鵧鸾鼸ꁖꅸꉱꌴꐳꕑꙞꝔꡊ꤮ꨰꬬ갬굱긺꽵뀠넦눬댤둢딢똨"), new object[]
			{
				this.QDefine.DETAIL_ID,
				this.ExtendCodePlace.ToString(),
				this.QDefine.PARENT_CODE,
				string_0
			});
			this.QDetails = this.oSurveyDetailDal.GetListBySql(string_);
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.IS_OTHER == 1)
				{
					this.OtherCode = surveyDetail.CODE;
				}
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000353A File Offset: 0x0000173A
		public void BeforeSave()
		{
			this.QAnswers = this.method_0();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00022B58 File Offset: 0x00020D58
		private List<SurveyAnswer> method_0()
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.QUESTION_NAME = this.QuestionName;
			surveyAnswer.CODE = this.SelectedCode;
			surveyAnswer.MULTI_ORDER = 0;
			surveyAnswer.BEGIN_DATE = new DateTime?(this.QInitDateTime);
			surveyAnswer.MODIFY_DATE = new DateTime?(DateTime.Now);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list.Add(surveyAnswer);
			if (this.OtherCode != null && this.OtherCode != global::GClass0.smethod_0(""))
			{
				list.Add(new SurveyAnswer
				{
					QUESTION_NAME = this.QuestionName + global::GClass0.smethod_0("[Ōɖ͉"),
					CODE = this.FillText,
					MULTI_ORDER = 0,
					BEGIN_DATE = new DateTime?(this.QInitDateTime),
					MODIFY_DATE = new DateTime?(DateTime.Now)
				});
			}
			return list;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00003548 File Offset: 0x00001748
		public void Save(string string_0, int int_0)
		{
			this.oSurveyAnswerDal.ClearBySequenceId(string_0, int_0);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, this.QAnswers);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000356A File Offset: 0x0000176A
		public void ReadAnswer(string string_0, int int_0)
		{
			this.QAnswersRead = this.oSurveyAnswerDal.GetListBySequenceId(string_0, int_0);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00022C3C File Offset: 0x00020E3C
		public string ReadAnswerByQuestionName(string string_0, string string_1)
		{
			string text = global::GClass0.smethod_0("");
			SurveyAnswer one = this.oSurveyAnswerDal.GetOne(string_0, string_1);
			return one.CODE;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000357F File Offset: 0x0000177F
		public virtual void GetDynamicDetails()
		{
			this.QDetails = this.oSurveyDetailDal.GetList(this.QDefine.DETAIL_ID, this.ParentCode);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00020240 File Offset: 0x0001E440
		public List<SurveyDetail> RandomDetails(List<SurveyDetail> list_0)
		{
			if (list_0.Count > 1)
			{
				RandomEngine randomEngine = new RandomEngine();
				list_0 = randomEngine.RandomDetails(list_0);
			}
			return list_0;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000035A3 File Offset: 0x000017A3
		public void RandomDetails()
		{
			this.QDetails = this.RandomDetails(this.QDetails);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00022C6C File Offset: 0x00020E6C
		public string GetInnerCodeText()
		{
			string result = global::GClass0.smethod_0("");
			foreach (SurveyDetail surveyDetail in this.QDetails)
			{
				if (surveyDetail.CODE == this.SelectedCode)
				{
					result = surveyDetail.CODE_TEXT;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400019D RID: 413
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x0400019E RID: 414
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400019F RID: 415
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x040001A0 RID: 416
		private int ExtendCodePlace;
	}
}
