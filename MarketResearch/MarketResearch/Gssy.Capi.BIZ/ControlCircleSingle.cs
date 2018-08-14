using System;
using System.Collections.Generic;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x0200000E RID: 14
	public class ControlCircleSingle
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002347 File Offset: 0x00000547
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000234F File Offset: 0x0000054F
		public string QuestionName { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002358 File Offset: 0x00000558
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002360 File Offset: 0x00000560
		public SurveyDetail InfoDetail { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002369 File Offset: 0x00000569
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002371 File Offset: 0x00000571
		public List<SurveyDetail> QDetails { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000237A File Offset: 0x0000057A
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002382 File Offset: 0x00000582
		public SurveyDefine QDefine { get; set; }

		// Token: 0x0600008A RID: 138 RVA: 0x00007400 File Offset: 0x00005600
		public void Init(string string_0)
		{
			string text = global::GClass0.smethod_0("");
			this.QDefine = this.oSurveyDefineDal.GetByName(string_0);
			this.QuestionName = string_0;
			if (this.QDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				this.QDetails = this.oSurveyDetailDal.GetDetails(this.QDefine.DETAIL_ID, out text);
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000746C File Offset: 0x0000566C
		public int GetCircleCount(string string_0)
		{
			return this.oSurveyDetailDal.GetDetailCount(string_0);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00007488 File Offset: 0x00005688
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

		// Token: 0x0600008D RID: 141 RVA: 0x000074D4 File Offset: 0x000056D4
		public int LimitDetailsCount(string string_0, string string_1, string string_2)
		{
			string string_3 = string.Format(global::GClass0.smethod_0("ôǣ˩ϡӠ׶ځފࢿ৘૏௓ೖය໊࿭ქᇠደᏭᓒᗼᛢ៧ᣪ᧼᪭ᯛ᳃᷏ớ῍₧⇕⋐⏖ⓕ◇⛘⟟⠶⤺⩝⭁ⱜⴁ⹉⼅ぐㅖ㈴㌺㐷㕒㙙㜡㠺㤫㨾㬸㰢㴥㸧㼷䀩䄧䈨䌡䑞䕅䘚䝑䠢䥹䩽䬓䰉䵺丈伍倒儅刁匝吜唜嘎圞堎夃娈孬將崃市弍恧慡戾捵搾攝晤杧栟楱橯欜汪浯湼潫灣煿牺獺瑬畼癰睽硪礎穡筥籠絯縉缏聜脗艘荻葢蔇蘆蜀蠶褾訽譝豕赞踹轉遂酓鉆鍀鑚镝陟靏顁饏驀魉鰷鴴鸮齳ꀶꅻꉚꍋꑗꕊ꘦"), string_0, string_1);
			List<SurveyAnswer> list = new List<SurveyAnswer>();
			list = this.oSurveyAnswerDal.GetListBySql(string_3);
			return list.Count;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00007510 File Offset: 0x00005710
		public void GetCurrentLimitInfo(string string_0, string string_1, string string_2, string string_3, int int_0)
		{
			string string_4 = string.Format(global::GClass0.smethod_0("àǷ˽ϵӬ׺ڍކࢋ৬ૻ௧೪ආ໶࿑ბᇔዄᏙᓞᗰᛮ៫᣾᧨᪹ᯏ᳟ᷓệῑ₳⇁⋄⏂ⓙ○⛔⟓⣂⧎⪩⮵Ⲡⷽ⺵⿹イㆢ㋀㏎㐻㕞㙕㜭㠮㤿㨪㬬㰾㴹㸻㼫䀽䄳䈼䌵䑒䕉䘖䝝䠖䥍䩉䬧䰵䵆临伱倦儱刵匩吐唐嘂園堚夗娜學尛崟帞弑恳慵截捡搲攑晨杫桫椅樛歨氖洓渀漗瀗焋爎猎瑠異發睱硾礚穵筱籼絳縕缓聈脃艌药葮蔋蘊蜌蠂褊訉譩豩赢踅轵遶酧鉲鍴鑖镑陓靃顕饛驔魝鰫鴨鸲齯ꀢꅯꉎꍟꑛꕆꘪꜬꡄ꥘ꩍꭍ걕괦깇꽝뀣녋뉅"), string_0, string_1);
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

		// Token: 0x0600008F RID: 143 RVA: 0x00007584 File Offset: 0x00005784
		public SurveyDetail GetOneInfo(string string_0, string string_1, string string_2)
		{
			return this.oSurveyDetailDal.GetOne(string_1, string_2);
		}

		// Token: 0x0400004E RID: 78
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x0400004F RID: 79
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x04000050 RID: 80
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();

		// Token: 0x04000051 RID: 81
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();
	}
}
