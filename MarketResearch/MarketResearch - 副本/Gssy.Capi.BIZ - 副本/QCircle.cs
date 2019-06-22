using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class QCircle
	{
		public string BigTitle { get; set; }

		public string QuestionName { get; set; }

		public string QuestionTitle { get; set; }

		public int ChildTotal { get; set; }

		public string SelectedCode { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public List<SurveyAnswer> QAnswersRead { get; set; }

		private SurveyDefine InfoDefine { get; set; }

		private SurveyRoadMap InfoRoadMap { get; set; }

		private SurveyDefine InfoDefineChild { get; set; }

		private SurveyRandom InfoRandomChild { get; set; }

		public SurveyDetail InfoDetail { get; set; }

		public void Init(string string_0, string string_1)
		{
			string string_2 = "";
			this.InfoRoadMap = this.oSurveyRoadMapDal.GetByPageId(string_0, string_1);
			string_2 = GClass0.smethod_0("7ĦȮ̤УՋ؞ܗࠜढ़ੈୖౕഗ๥ཀ၆ᅅ቗ፈᑴᕊᙈᝄᡂ᥎ᨊ᭞᱀ᵂṔὀ\u2004℃≒⍀⑇╺♁❴⡸⤻⨧⬾Ᵽⴧ⹫⼲〴ㅲ㉼㍵㐰㕌㙁㝀㡎㥂㩄㭌㱗㵎㹈㽁䁁䅛䈿䌱");
			this.InfoDefine = this.oSurveyDefineDal.GetBySql(string_2);
			this.BigTitle = this.InfoDefine.QUESTION_TITLE;
		}

		public void GetChild(string string_0, string string_1, int int_0)
		{
			string string_2 = "";
			int is_RANDOM = this.InfoDefine.IS_RANDOM;
			if (is_RANDOM == 0)
			{
				string_2 = string.Format(GClass0.smethod_0("4ģȩ̡РԶ١ݪࠟक़੏୓ౖച๪ཌྷ၅ᅀቐፍᑷᕗᙅᝑᡆ᥂ᨍ᭛᱃ᵏṛὍ\u2007Ⅲ≠⍰③╫♭❿⡖⥚⨽⬡ⰼⵡ⸩⽥〰ㄶ㉴㍺㑷㔲㙘㝞㡁㥋㩟㭓㱄㵘㹍㽍䁕䄦䈸䌤䑸䔳䙼"), this.InfoDefine.DETAIL_ID, int_0);
				this.InfoDetail = this.oSurveyDetailDal.GetBySql(string_2);
			}
			else
			{
				string_2 = string.Format(GClass0.smethod_0("\u0012ąȳ̻оԨٻݰࡹाਥହస൴฀༧ဣᄦሪጷᐟᔭᘥᜮᠦᤥᩧᬱᰭᴡḱἧ⁡ℓ≊⍌⑋╙♂❥⡰⥜⨗⬋Ⱂⵏ⸃⽏〖ㄐ㉎㍀㑉㔌㙚㝟㡌㥛㩓㭏㱊㵊㹼㽑䁄䅔䈿䌣䐽䔻䙠䜫䡤䤿䨷䭷䱻䵰丳你偰兾剫卡呠啓噢坤塭奭婿嬦尸崤幸弰恼"), string_0, "D", int_0);
				this.InfoRandomChild = this.oSurveyRandomDal.GetBySql(string_2);
				string_2 = string.Format(GClass0.smethod_0("1ĤȬ͚ѝՉ؜ܑࠚय़੊୘౛ക๧ཆ၀ᅇቕፖᑪᕈᙘᝊᡃ᥅ᨈ᭐ᱎᵀṖ὆\u2002Ⅵ≥⍋⑟╔♐❄⡓⥝⨸⬪ⰱ⵮⸤⽮〵ㄱ㉱㍡㑪㔭㙏㝄㡎㥌㨨㬺㰦㴢㹿㼲䁿䄦"), this.InfoDefine.DETAIL_ID, this.InfoRandomChild.CODE);
				this.InfoDetail = this.oSurveyDetailDal.GetBySql(string_2);
			}
			this.QuestionName = this.InfoDefine.QUESTION_NAME + this.InfoDetail.CODE;
			string_2 = string.Format(GClass0.smethod_0("4ģȩ̡РԶ١ݪࠟक़੏୓ౖച๪ཌྷ၅ᅀቐፍᑷᕗᙗ᝙ᡁ᥋ᨍ᭛᱃ᵏṛὍ\u2007⅖≄⍃⑆╽♈❄⠿⤣⨺⭧Ⱛⵧ⸾⼸ぶㅸ㉱㌴㑢㕧㙴㝣㡻㥧㩢㭢㱔㵤㹨㽥䁢䄻䈢䍿䐲䕿䘦"), string_1, this.QuestionName);
			this.InfoDefineChild = this.oSurveyDefineDal.GetBySql(string_2);
			this.QuestionTitle = this.InfoDefineChild.QUESTION_TITLE;
			string text = "";
			this.QDetails = this.oSurveyDetailDal.GetDetails(this.InfoDefineChild.DETAIL_ID, out text);
		}

		public void BeforeSave()
		{
		}

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

		public void ReadAnswer(string string_0)
		{
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			string string_ = string.Format(GClass0.smethod_0(":ĭọ̈̄Ц԰٣ݨࡡद੍୑౐ജ๨ཏ။ᅎቒፏᑴᕚᙀᝅᡔ᥂ᨏ᭙᱅ᵉṙ὏\u2009⅛≒⍔⑓╁♚❽⡈⥄⨢⬹ⱦ⴬⹦⼽〹ㅹ㉹㍲㐵㕥㙦㝷㡢㥤㩦㭡㱣㵓㹥㽫䁤䅭䈧䌻䐢䕿䘲䝿䠦"), string_0, this.QuestionName);
			SurveyAnswer item = new SurveyAnswer();
			item = this.oSurveyAnswerDal.GetBySql(string_);
			list.Add(item);
			string text = this.QuestionName + "_OTH";
			if (this.oSurveyAnswerDal.Exists(string_0, text, ""))
			{
				string_ = string.Format(GClass0.smethod_0(":ĭọ̈̄Ц԰٣ݨࡡद੍୑౐ജ๨ཏ။ᅎቒፏᑴᕚᙀᝅᡔ᥂ᨏ᭙᱅ᵉṙ὏\u2009⅛≒⍔⑓╁♚❽⡈⥄⨢⬹ⱦ⴬⹦⼽〹ㅹ㉹㍲㐵㕥㙦㝷㡢㥤㩦㭡㱣㵓㹥㽫䁤䅭䈧䌻䐢䕿䘲䝿䠦"), string_0, text);
				SurveyAnswer item2 = new SurveyAnswer();
				item2 = this.oSurveyAnswerDal.GetBySql(string_);
				list.Add(item2);
			}
			this.QAnswersRead = list;
		}

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();
	}
}
