using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class ControlCircleSingle
	{
		public string QuestionName { get; set; }

		public SurveyDetail InfoDetail { get; set; }

		public List<SurveyDetail> QDetails { get; set; }

		public SurveyDefine QDefine { get; set; }

		public void Init(string string_0)
		{
			string text = "";
			this.QDefine = this.oSurveyDefineDal.GetByName(string_0);
			this.QuestionName = string_0;
			if (this.QDefine.DETAIL_ID != "")
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text);
			}
		}

		public int GetCircleCount(string string_0)
		{
			return this.oSurveyDetailDal.GetDetailCount(string_0);
		}

		public void GetCurrentInfo(string string_0, int int_0, string string_1, int int_1)
		{
			if (int_1 == 1)
			{
				string oneCode = this.oSurveyRandomDal.GetOneCode(string_0, string_1, 1, int_0);
				this.InfoDetail = this.oSurveyDetailDal.GetOne(string_1, oneCode);
			}
			else
			{
				this.InfoDetail = this.oSurveyDetailDal.GetOneByOrder(string_1, int_0);
			}
		}

		public int LimitDetailsCount(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(GClass0.smethod_0("ôǣ˩ϡӠ׶ځފࢿ৘૏௓ೖය໊࿭ქᇠደᏭᓒᗼᛢ៧ᣪ᧼᪭ᯛ᳃᷏ớ῍₧⇕⋐⏖ⓕ◇⛘⟟⠶⤺⩝⭁ⱜⴁ⹉⼅ぐㅖ㈴㌺㐷㕒㙙㜡㠺㤫㨾㬸㰢㴥㸧㼷䀩䄧䈨䌡䑞䕅䘚䝑䠢䥹䩽䬓䰉䵺丈伍倒儅刁匝吜唜嘎圞堎夃娈孬將崃市弍恧慡戾捵搾攝晤杧栟楱橯欜汪浯湼潫灣煿牺獺瑬畼癰睽硪礎穡筥籠絯縉缏聜脗艘荻葢蔇蘆蜀蠶褾訽譝豕赞踹轉遂酓鉆鍀鑚镝陟靏顁饏驀魉鰷鴴鸮齳ꀶꅻꉚꍋꑗꕊ꘦"), string_0, string_1);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySql(string_3);
			return list.Count;
		}

		public void GetCurrentLimitInfo(string string_0, string string_1, string string_2, string string_3, int int_0)
		{
			string string_4 = string.Format(GClass0.smethod_0("àǷ˽ϵӬ׺ڍކࢋ৬ૻ௧೪ආ໶࿑ბᇔዄᏙᓞᗰᛮ៫᣾᧨᪹ᯏ᳟ᷓệῑ₳⇁⋄⏂ⓙ○⛔⟓⣂⧎⪩⮵Ⲡⷽ⺵⿹イㆢ㋀㏎㐻㕞㙕㜭㠮㤿㨪㬬㰾㴹㸻㼫䀽䄳䈼䌵䑒䕉䘖䝝䠖䥍䩉䬧䰵䵆临伱倦儱刵匩吐唐嘂園堚夗娜學尛崟帞弑恳慵截捡搲攑晨杫桫椅樛歨氖洓渀漗瀗焋爎猎瑠異發睱硾礚穵筱籼絳縕缓聈脃艌药葮蔋蘊蜌蠂褊訉譩豩赢踅轵遶酧鉲鍴鑖镑陓靃顕饛驔魝鰫鴨鸲齯ꀢꅯꉎꍟꑛꕆꘪꜬꡄ꥘ꩍꭍ걕괦깇꽝뀣녋뉅"), string_0, string_1);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySql(string_4);
			for (int i = 0; i < list.Count; i++)
			{
				if (i == int_0 - 1)
				{
					this.InfoDetail = this.oSurveyDetailDal.GetOne(string_3, list[i].CODE.ToString());
				}
			}
		}

		public SurveyDetail GetOneInfo(string string_0, string string_1, string string_2)
		{
			return this.oSurveyDetailDal.GetOne(string_1, string_2);
		}

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
