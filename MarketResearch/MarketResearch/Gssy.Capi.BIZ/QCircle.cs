using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000018 RID: 24
	public class QCircle
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00002A86 File Offset: 0x00000C86
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00002A8E File Offset: 0x00000C8E
		public string BigTitle { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00002A97 File Offset: 0x00000C97
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00002A9F File Offset: 0x00000C9F
		public string QuestionName { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000271 RID: 625 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public string QuestionTitle { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00002AB9 File Offset: 0x00000CB9
		// (set) Token: 0x06000273 RID: 627 RVA: 0x00002AC1 File Offset: 0x00000CC1
		public int ChildTotal { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00002ACA File Offset: 0x00000CCA
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00002AD2 File Offset: 0x00000CD2
		public string SelectedCode { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00002ADB File Offset: 0x00000CDB
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00002AE3 File Offset: 0x00000CE3
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00002AEC File Offset: 0x00000CEC
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public List<SurveyAnswer> QAnswersRead { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00002AFD File Offset: 0x00000CFD
		// (set) Token: 0x0600027B RID: 635 RVA: 0x00002B05 File Offset: 0x00000D05
		private SurveyDefine InfoDefine { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00002B0E File Offset: 0x00000D0E
		// (set) Token: 0x0600027D RID: 637 RVA: 0x00002B16 File Offset: 0x00000D16
		private SurveyRoadMap InfoRoadMap { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00002B1F File Offset: 0x00000D1F
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00002B27 File Offset: 0x00000D27
		private SurveyDefine InfoDefineChild { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00002B30 File Offset: 0x00000D30
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00002B38 File Offset: 0x00000D38
		private SurveyRandom InfoRandomChild { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00002B41 File Offset: 0x00000D41
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00002B49 File Offset: 0x00000D49
		public SurveyDetail InfoDetail { get; set; }

		// Token: 0x06000285 RID: 645 RVA: 0x0002026C File Offset: 0x0001E46C
		public void Init(string string_0, string string_1)
		{
			string string_2 = global::GClass0.smethod_0("");
			this.InfoRoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
			string_2 = global::GClass0.smethod_0("7ĦȮ̤УՋ؞ܗࠜढ़ੈୖౕഗ๥ཀ၆ᅅ቗ፈᑴᕊᙈᝄᡂ᥎ᨊ᭞᱀ᵂṔὀ\u2004℃≒⍀⑇╺♁❴⡸⤻⨧⬾Ᵽⴧ⹫⼲〴ㅲ㉼㍵㐰㕌㙁㝀㡎㥂㩄㭌㱗㵎㹈㽁䁁䅛䈿䌱");
			this.InfoDefine = this.oSurveyDefineDal.GetBySql(string_2);
			this.BigTitle = this.InfoDefine.QUESTION_TITLE;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000202C8 File Offset: 0x0001E4C8
		public void GetChild(string string_0, string string_1, int int_0)
		{
			string string_2 = global::GClass0.smethod_0("");
			int is_RANDOM = this.InfoDefine.IS_RANDOM;
			if (is_RANDOM == 0)
			{
				string_2 = string.Format(global::GClass0.smethod_0("4ģȩ̡РԶ١ݪࠟक़੏୓ౖച๪ཌྷ၅ᅀቐፍᑷᕗᙅᝑᡆ᥂ᨍ᭛᱃ᵏṛὍ\u2007Ⅲ≠⍰③╫♭❿⡖⥚⨽⬡ⰼⵡ⸩⽥〰ㄶ㉴㍺㑷㔲㙘㝞㡁㥋㩟㭓㱄㵘㹍㽍䁕䄦䈸䌤䑸䔳䙼"), this.InfoDefine.DETAIL_ID, int_0);
				this.InfoDetail = this.oSurveyDetailDal.GetBySql(string_2);
			}
			else
			{
				string_2 = string.Format(global::GClass0.smethod_0("\u0012ąȳ̻оԨٻݰࡹाਥହస൴฀༧ဣᄦሪጷᐟᔭᘥᜮᠦᤥᩧᬱᰭᴡḱἧ⁡ℓ≊⍌⑋╙♂❥⡰⥜⨗⬋Ⱂⵏ⸃⽏〖ㄐ㉎㍀㑉㔌㙚㝟㡌㥛㩓㭏㱊㵊㹼㽑䁄䅔䈿䌣䐽䔻䙠䜫䡤䤿䨷䭷䱻䵰丳你偰兾剫卡呠啓噢坤塭奭婿嬦尸崤幸弰恼"), string_0, global::GClass0.smethod_0("E"), int_0);
				this.InfoRandomChild = this.oSurveyRandomDal.GetBySql(string_2);
				string_2 = string.Format(global::GClass0.smethod_0("1ĤȬ͚ѝՉ؜ܑࠚय़੊୘౛ക๧ཆ၀ᅇቕፖᑪᕈᙘᝊᡃ᥅ᨈ᭐ᱎᵀṖ὆\u2002Ⅵ≥⍋⑟╔♐❄⡓⥝⨸⬪ⰱ⵮⸤⽮〵ㄱ㉱㍡㑪㔭㙏㝄㡎㥌㨨㬺㰦㴢㹿㼲䁿䄦"), this.InfoDefine.DETAIL_ID, this.InfoRandomChild.CODE);
				this.InfoDetail = this.oSurveyDetailDal.GetBySql(string_2);
			}
			this.QuestionName = this.InfoDefine.QUESTION_NAME + this.InfoDetail.CODE;
			string_2 = string.Format(global::GClass0.smethod_0("4ģȩ̡РԶ١ݪࠟक़੏୓ౖച๪ཌྷ၅ᅀቐፍᑷᕗᙗ᝙ᡁ᥋ᨍ᭛᱃ᵏṛὍ\u2007⅖≄⍃⑆╽♈❄⠿⤣⨺⭧Ⱛⵧ⸾⼸ぶㅸ㉱㌴㑢㕧㙴㝣㡻㥧㩢㭢㱔㵤㹨㽥䁢䄻䈢䍿䐲䕿䘦"), string_1, this.QuestionName);
			this.InfoDefineChild = this.oSurveyDefineDal.GetBySql(string_2);
			this.QuestionTitle = this.InfoDefineChild.QUESTION_TITLE;
			string text = global::GClass0.smethod_0("");
			this.QDetails = this.oSurveyDetailDal.GetDetails(this.InfoDefineChild.DETAIL_ID, out text);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000024F1 File Offset: 0x000006F1
		public void BeforeSave()
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00020418 File Offset: 0x0001E618
		public void SaveSingle(string string_0, int int_0)
		{
			SurveyAnswer surveyAnswer = new SurveyAnswer();
			surveyAnswer.QUESTION_NAME = this.QuestionName;
			surveyAnswer.CODE = this.SelectedCode;
			surveyAnswer.MULTI_ORDER = 0;
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list.Add(surveyAnswer);
			this.oSurveyAnswerDal.UpdateList(string_0, int_0, list);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00020468 File Offset: 0x0001E668
		public void ReadAnswer(string string_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			string string_ = string.Format(global::GClass0.smethod_0(":ĭọ̈̄Ц԰٣ݨࡡद੍୑౐ജ๨ཏ။ᅎቒፏᑴᕚᙀᝅᡔ᥂ᨏ᭙᱅ᵉṙ὏\u2009⅛≒⍔⑓╁♚❽⡈⥄⨢⬹ⱦ⴬⹦⼽〹ㅹ㉹㍲㐵㕥㙦㝷㡢㥤㩦㭡㱣㵓㹥㽫䁤䅭䈧䌻䐢䕿䘲䝿䠦"), string_0, this.QuestionName);
			SurveyAnswer item = new SurveyAnswer();
			item = this.oSurveyAnswerDal.GetBySql(string_);
			list.Add(item);
			string text = this.QuestionName + global::GClass0.smethod_0("[Ōɖ͉");
			if (this.oSurveyAnswerDal.Exists(string_0, text, global::GClass0.smethod_0("")))
			{
				string_ = string.Format(global::GClass0.smethod_0(":ĭọ̈̄Ц԰٣ݨࡡद੍୑౐ജ๨ཏ။ᅎቒፏᑴᕚᙀᝅᡔ᥂ᨏ᭙᱅ᵉṙ὏\u2009⅛≒⍔⑓╁♚❽⡈⥄⨢⬹ⱦ⴬⹦⼽〹ㅹ㉹㍲㐵㕥㙦㝷㡢㥤㩦㭡㱣㵓㹥㽫䁤䅭䈧䌻䐢䕿䘲䝿䠦"), string_0, text);
				SurveyAnswer item2 = new SurveyAnswer();
				item2 = this.oSurveyAnswerDal.GetBySql(string_);
				list.Add(item2);
			}
			this.QAnswersRead = list;
		}

		// Token: 0x04000113 RID: 275
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x04000114 RID: 276
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x04000115 RID: 277
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x04000116 RID: 278
		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		// Token: 0x04000117 RID: 279
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();
	}
}
