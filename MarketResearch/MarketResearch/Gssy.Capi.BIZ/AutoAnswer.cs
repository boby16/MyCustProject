using System;
using System.Collections.Generic;
using System.Text;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	// Token: 0x02000005 RID: 5
	public class AutoAnswer
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000218D File Offset: 0x0000038D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002195 File Offset: 0x00000395
		public string SurveyId { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000219E File Offset: 0x0000039E
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000021A6 File Offset: 0x000003A6
		public string RouteLogic { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000021AF File Offset: 0x000003AF
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000021B7 File Offset: 0x000003B7
		public string GroupCodeA { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000021C0 File Offset: 0x000003C0
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000021C8 File Offset: 0x000003C8
		public string CircleACode { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000021D1 File Offset: 0x000003D1
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000021D9 File Offset: 0x000003D9
		public string CircleCodeTextA { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000021E2 File Offset: 0x000003E2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000021EA File Offset: 0x000003EA
		public int CircleACount { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000021F3 File Offset: 0x000003F3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000021FB File Offset: 0x000003FB
		public int CircleACurrent { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000220C File Offset: 0x0000040C
		public bool IsLastA { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002215 File Offset: 0x00000415
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000221D File Offset: 0x0000041D
		public string GroupCodeB { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002226 File Offset: 0x00000426
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000222E File Offset: 0x0000042E
		public string CircleBCode { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002237 File Offset: 0x00000437
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000223F File Offset: 0x0000043F
		public string CircleCodeTextB { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002248 File Offset: 0x00000448
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002250 File Offset: 0x00000450
		public int CircleBCount { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002259 File Offset: 0x00000459
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002261 File Offset: 0x00000461
		public int CircleBCurrent { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000226A File Offset: 0x0000046A
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002272 File Offset: 0x00000472
		public bool IsLastB { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000227B File Offset: 0x0000047B
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002283 File Offset: 0x00000483
		public string MyAnswer { get; set; }

		// Token: 0x06000046 RID: 70 RVA: 0x0000419C File Offset: 0x0000239C
		public AutoAnswer()
		{
			this.method_0();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004288 File Offset: 0x00002488
		public void SurveyInit(string string_0, string string_1, int int_0, string string_2, string string_3)
		{
			this.oRandom.RandomSurveyMain(string_0);
			string string_4 = global::GClass0.smethod_0("4ĵȵ̷в԰");
			string string_5 = global::GClass0.smethod_0("5ĳȲ̰");
			this.oSurvey.AddSurvey(string_0, string_1, string_2, string_4, string_5, string_3);
			QFill qfill = new QFill();
			qfill.Init(global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄"), 0);
			qfill.FillText = string_0;
			qfill.BeforeSave();
			qfill.Save(string_0, int_0);
			this.oSurveyAnswerDal.AddOneNoUpdate(string_0, global::GClass0.smethod_0("Jşɚ͍ѕՙقݖࡌॗੑ"), string_3, int_0);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004310 File Offset: 0x00002510
		public string GetChildByIndex(string string_0, int int_0)
		{
			return this.oSurveyDefineDal.GetChildByIndex(string_0, int_0);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000432C File Offset: 0x0000252C
		public void QuestionInit(string string_0, string string_1)
		{
			this.MySurveyDefine = this.oSurveyDefineDal.GetByName(string_1);
			this.MySurveyRoadMap = this.oSurveyRoadMapDal.GetByPageId(this.MySurveyDefine.PAGE_ID, global::GClass0.smethod_0("1"));
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				this.lSurveyDetail = this.oSurveyDetailDal.GetDetails(this.MySurveyDefine.DETAIL_ID);
			}
			this.lSurveyLogic = this.oSurveyLogicDal.GetCheckLogic(this.MySurveyDefine.PAGE_ID);
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
			}
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
				this.lSurveyRandomB = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEB);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000445C File Offset: 0x0000265C
		public int PageInfo(string string_0)
		{
			this.lPageDefine = this.oSurveyDefineDal.GetListByPageId(string_0);
			return this.lPageDefine.Count;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004488 File Offset: 0x00002688
		public string QuestionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(global::GClass0.smethod_0("\vĈȉ̎ЏԌ؍ܒࠓऐ਑ଖగഔจུၓᅗቒፆᑛᕥᙅ᝹ᡷᥳ᩹ᬻ᱓ᵷṾὸ⁤ⅸ≵⍧⑻╾♾✯⠳⤰⨱⬶ⰷⴴ⸵⼺〻ㄸ㈹㌾㐿㔼"));
			stringBuilder.AppendLine();
			if (this.MySurveyDefine.ID > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("臣厠紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_ORDER > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("颓矤趚勲鱽安إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.ANSWER_ORDER.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_ID != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("顽縑凱̥Фԯآܡ"));
				stringBuilder.Append(this.MySurveyDefine.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_NAME != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("闧馐紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TITLE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("串馐骟嵴ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("串馐骟咍ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_USE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("辘僰韧鮐䭸瀮إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_USE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("辞僶韥鮒罝浀䥸爮ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.ANSWER_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.COMBINE_INDEX > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("绉唄骓疎彙鶐笥堓ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.COMBINE_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.DETAIL_ID != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("逅揢驳剺葜稑縇ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.DETAIL_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PARENT_CODE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("夕義鈄懥鱲眼坺蝜眑焇ਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.PARENT_CODE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_CONTENT != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("剦馐骟嵴ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_CONTENT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_TITLE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("_śə͚Ш鶟塴ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_CASE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("_śə͚Ш鶟再ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_CASE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_VARIABLE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("]ŝɟ͘Ъ囑韇筼徍थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_VARIABLE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_PRINT_DECIMAIL > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("^Ŝɘ͙Щ备捷䡋ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_PRINT_DECIMAIL.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MIN_COUNT > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("夔鄄骔疏挊夆阁旮浶थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.MIN_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MAX_COUNT > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("夔鄄骔疏挊尮阁旮浶थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.MAX_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.IS_RANDOM > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("冊釦鈄魵戤儬邆怲殕嬑ਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.IS_RANDOM.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_COUNT_DOWN > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("项說柼匛慸ԧ翔ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.PAGE_COUNT_DOWN.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TYPE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("掫俽璎署厃暠吰ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_FONTSIZE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("掫俽璎塞䭛尠娉ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_HEIGHT > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("掭俿璌駟媠ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_HEIGHT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_WIDTH > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("掭俿璌墺媠ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_WIDTH.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_MASK != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("掫俽Ȫ̈́ѩմ٭ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_MASK);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.TITLE_FONTSIZE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("闠馕樋鮓犎幞䥛帠吉थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.TITLE_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TOOLTIP != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("推俸ȭ͘Ѥե٥ݜ࡮ॶਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TOOLTIP);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.NOTE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("夕淹觤攁ЮԢج愵焰醑粌媂垿ഥฤ༯ဢᄡ"));
				stringBuilder.Append(this.MySurveyDefine.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.LIMIT_LOGIC != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("亜厥鑜儽犎锲覙撠娰थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.LIMIT_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("御犥骑緌窠圭إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_LEVEL);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEA != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("徻犿骗緊瘻媦疤䧩瀈ॉਧ坄థതฯ༢အ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEA);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEB != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("徻犿骗緊瘻媦疤䧩瀈ॊਧ坄థതฯ༢အ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEB);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_PAGE_TYPE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("闼馉唸岥瞡鶕磈䤦纎䙄畦睼宍ഥฤ༯ဢᄡ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_PAGE_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_MSG != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("徦犤骒緍鲐䫦晩ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_MSG);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_COUNT != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("徧犣骓緎釧鶐捷雉ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_COUNT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_USE > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("昦售晟誇ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_TITLE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("摑袉樀鮞ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_INDEX > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("摑袉缥尓ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			stringBuilder.Append(global::GClass0.smethod_0("\nċȈ̉Ўԏ،܍ࠒओਐ଑ఖഗฉཻၒᅔቓፁᑚᕰᙎᝁ᡻ᥓ᭬᩼᰻ᵓṷ὾⁸Ⅴ≸⍵⑧╻♾❾⠯⤳⨰⬱ⰶⴷ⸴⼵〺ㄻ㈸㌹㐾㔿㘼"));
			stringBuilder.AppendLine();
			if (this.MySurveyRoadMap.ID > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("臣厠紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.VERSION_ID > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("跤琻灁搤笑囱إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyRoadMap.VERSION_ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PART_NAME != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("闧剿倁鏮ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PART_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_NOTE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("顽諳搈̥Фԯآܡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_ID != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("顼ĨɎ͂ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.ROUTE_LOGIC != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("项賸赦軦焹锼覗ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyRoadMap.ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.GROUP_ROUTE_LOGIC != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("徤犢糈総喏裺襤霼螗थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyRoadMap.GROUP_ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.FORM_NAME != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("顼笃岈國ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.FORM_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.IS_JUMP > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("春唯凧軴譪ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyRoadMap.IS_JUMP.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.NOTE != global::GClass0.smethod_0(""))
			{
				stringBuilder.Append(global::GClass0.smethod_0("夀淮ȥ̤ЯԢء"));
				stringBuilder.Append(this.MySurveyRoadMap.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				stringBuilder.Append(global::GClass0.smethod_0("\vĈȉ̎ЏԌ؍ܒࠓऐ਑ଖగഔจུၓᅗቒፆᑛᕥᙅᝫ᡿ᥴᩰᬻ᱓ᵷṾὸ⁤ⅸ≵⍧⑻╾♾✯⠳⤰⨱⬶ⰷⴴ⸵⼺〻ㄸ㈹㌾㐿㔼"));
				stringBuilder.AppendLine();
				stringBuilder.Append(global::GClass0.smethod_0("缄礐Ȱ̯Тԭج砝瀋沎洤ଧపഥ栫嬥䅴志"));
				stringBuilder.AppendLine();
				foreach (SurveyDetail surveyDetail in this.lSurveyDetail)
				{
					stringBuilder.Append(string.Format(global::GClass0.smethod_0("jĠɲ̮ЭԠثܪࡲह੺ଦ఩ത๸༰ၼ"), surveyDetail.CODE, surveyDetail.CODE_TEXT, surveyDetail.IS_OTHER.ToString()));
					stringBuilder.AppendLine();
				}
			}
			if (this.lSurveyLogic.Count > 0)
			{
				stringBuilder.Append(global::GClass0.smethod_0("\bĉȎ̏Ќԍؒܓࠐऑਖଗఔകงཱུၐᅖቕፇᑘᕬᙰ᝹ᡴ᥿ᨻ᭓ᱷᵾṸὤ⁸ⅵ≧⍻⑾╾☯✳⠰⤱⨶⬷ⰴⴵ⸺⼻〸ㄹ㈾㌿㐼"));
				stringBuilder.AppendLine();
				foreach (SurveyLogic surveyLogic in this.lSurveyLogic)
				{
					stringBuilder.Append(global::GClass0.smethod_0("逰躛卥將徝䭏إܤ࠯ढਡ"));
					stringBuilder.Append(surveyLogic.FORMULA);
					stringBuilder.AppendLine();
					stringBuilder.Append(global::GClass0.smethod_0("揙砲䷦捩ХԤدܢࠡ"));
					stringBuilder.Append(surveyLogic.LOGIC_MESSAGE);
					stringBuilder.AppendLine();
					stringBuilder.Append(global::GClass0.smethod_0("進躙懈賶ХԤدܢࠡ"));
					stringBuilder.Append(surveyLogic.NOTE);
					stringBuilder.AppendLine();
					stringBuilder.Append(global::GClass0.smethod_0("兏誵畯壷籤躭分霝蟁थਤଯఢഡ"));
					stringBuilder.Append(surveyLogic.IS_ALLOW_PASS.ToString());
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				stringBuilder.Append(global::GClass0.smethod_0(" ġȦ̧Фԥتܫࠨऩਮଯబഭฯཝၸᅾች፯ᑰᕚᙦᝨᡡᥫᩮᬢᱚ") + this.MySurveyDefine.GROUP_CODEA + global::GClass0.smethod_0("AĻɓͷѾո٤ݸࡵ१੻୾౾യำ༰ေᄶሷጴᐵᔺᘻ᜸ᠹ᤾ᨿᬼ"));
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom in this.lSurveyRandomA)
				{
					stringBuilder.Append(surveyRandom.CODE);
					stringBuilder.Append(global::GClass0.smethod_0("#Įȡ"));
				}
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				stringBuilder.Append(global::GClass0.smethod_0(" ġȦ̧Фԥتܫࠨऩਮଯబഭฯཝၸᅾች፯ᑰᕚᙦᝨᡡᥫᩮᬢᱚ") + this.MySurveyDefine.GROUP_CODEB + global::GClass0.smethod_0("AĻɓͷѾո٤ݸࡵ१੻୾౾യำ༰ေᄶሷጴᐵᔺᘻ᜸ᠹ᤾ᨿᬼ"));
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom2 in this.lSurveyRandomB)
				{
					stringBuilder.Append(surveyRandom2.CODE);
					stringBuilder.Append(global::GClass0.smethod_0("#Įȡ"));
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000054E8 File Offset: 0x000036E8
		public string NextPage(string string_0, int int_0, string string_1, string string_2)
		{
			string route_LOGIC = this.MySurveyRoadMap.ROUTE_LOGIC;
			string result = new LogicEngine
			{
				SurveyID = string_0
			}.Route(route_LOGIC);
			this.RouteLogic = route_LOGIC;
			return result;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00005520 File Offset: 0x00003720
		public string GetAutoSurveyId(string string_0, int int_0)
		{
			return this.oSurveyMainDal.GetAutoSurveyId(string_0, int_0);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000553C File Offset: 0x0000373C
		public void SetMain(string string_0, string string_1, int int_0)
		{
			string text = string_1;
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("@"))
			{
				text = text + global::GClass0.smethod_0("]œ") + this.CircleACode;
			}
			if (this.MySurveyDefine.GROUP_LEVEL == global::GClass0.smethod_0("C"))
			{
				text = string.Concat(new string[]
				{
					text,
					global::GClass0.smethod_0("]œ"),
					this.CircleACode,
					global::GClass0.smethod_0("]œ"),
					this.CircleBCode
				});
			}
			switch (this.MySurveyDefine.QUESTION_TYPE)
			{
			case 1:
				this.SetFill(string_0, text, int_0);
				break;
			case 2:
				this.SetSingle(string_0, text, int_0);
				break;
			case 3:
				this.SetMultiple(string_0, text, int_0);
				break;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00005618 File Offset: 0x00003818
		public void SetFill(string string_0, string string_1, int int_0)
		{
			QFill qfill = new QFill();
			qfill.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX);
			qfill.FillText = this.FixSetSingle(string_1);
			if (qfill.FillText == null)
			{
				qfill.FillText = this.MySurveyDefine.QUESTION_NAME;
			}
			this.MyAnswer = qfill.FillText;
			qfill.QuestionName = string_1;
			qfill.Save(string_0, int_0);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000568C File Offset: 0x0000388C
		public void SetSingle(string string_0, string string_1, int int_0)
		{
			QSingle qsingle = new QSingle();
			qsingle.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != global::GClass0.smethod_0(""))
			{
				qsingle.ParentCode = new LogicAnswer
				{
					SurveyID = string_0
				}.GetAnswer(this.MySurveyDefine.PARENT_CODE);
				qsingle.GetDynamicDetails();
			}
			qsingle.SelectedCode = this.FixSetSingle(string_1);
			if (qsingle.SelectedCode == null)
			{
				qsingle.RandomDetails();
				qsingle.SelectedCode = qsingle.QDetails[0].CODE;
			}
			this.MyAnswer = qsingle.SelectedCode;
			if (qsingle.OtherCode != global::GClass0.smethod_0(""))
			{
				qsingle.FillText = global::GClass0.smethod_0("兵俔ȡ") + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + global::GClass0.smethod_0("#Įȡ") + qsingle.FillText;
			}
			qsingle.QuestionName = string_1;
			qsingle.BeforeSave();
			qsingle.Save(string_0, int_0, true);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000057B4 File Offset: 0x000039B4
		public void SetMultiple(string string_0, string string_1, int int_0)
		{
			QMultiple qmultiple = new QMultiple();
			qmultiple.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != global::GClass0.smethod_0(""))
			{
				qmultiple.ParentCode = new LogicAnswer
				{
					SurveyID = string_0
				}.GetAnswer(this.MySurveyDefine.PARENT_CODE);
				qmultiple.GetDynamicDetails();
			}
			qmultiple.RandomDetails();
			this.MyAnswer = global::GClass0.smethod_0("");
			foreach (SurveyDetail surveyDetail in qmultiple.QDetails)
			{
				qmultiple.SelectedValues.Add(surveyDetail.CODE);
				this.MyAnswer = this.MyAnswer + surveyDetail.CODE + global::GClass0.smethod_0("#Įȡ");
			}
			if (qmultiple.OtherCode != global::GClass0.smethod_0(""))
			{
				qmultiple.FillText = global::GClass0.smethod_0("兵俔ȡ") + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + global::GClass0.smethod_0("#Įȡ") + qmultiple.FillText;
			}
			qmultiple.QuestionName = string_1;
			qmultiple.BeforeSave();
			qmultiple.Save(string_0, int_0);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005924 File Offset: 0x00003B24
		public string FixSetSingle(string string_0)
		{
			string result = global::GClass0.smethod_0("");
			this.DAnswer.TryGetValue(string_0, out result);
			return result;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00005950 File Offset: 0x00003B50
		private void method_0()
		{
			this.DAnswer.Add(global::GClass0.smethod_0("Qİ"), global::GClass0.smethod_0("4"));
			this.DAnswer.Add(global::GClass0.smethod_0("Qĳ"), global::GClass0.smethod_0("3"));
			this.DAnswer.Add(global::GClass0.smethod_0("QĲ"), global::GClass0.smethod_0("2"));
			this.DAnswer.Add(global::GClass0.smethod_0("Qĵ"), global::GClass0.smethod_0("0"));
			this.DAnswer.Add(global::GClass0.smethod_0("QĶ"), global::GClass0.smethod_0("0"));
			this.DAnswer.Add(global::GClass0.smethod_0("QĹ"), global::GClass0.smethod_0("0"));
			this.DAnswer.Add(global::GClass0.smethod_0("Qĸ"), global::GClass0.smethod_0("0"));
			this.DAnswer.Add(global::GClass0.smethod_0("Pĳȱ"), global::GClass0.smethod_0("3"));
			this.DAnswer.Add(global::GClass0.smethod_0("PĳȲ"), global::GClass0.smethod_0("1ĸ"));
		}

		// Token: 0x04000014 RID: 20
		private RandomBiz oRandom = new RandomBiz();

		// Token: 0x04000015 RID: 21
		private SurveyBiz oSurvey = new SurveyBiz();

		// Token: 0x04000023 RID: 35
		private Dictionary<string, string> DAnswer = new Dictionary<string, string>();

		// Token: 0x04000024 RID: 36
		public SurveyDefine MySurveyDefine = new SurveyDefine();

		// Token: 0x04000025 RID: 37
		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		// Token: 0x04000026 RID: 38
		private SurveyRoadMap MySurveyRoadMap = new SurveyRoadMap();

		// Token: 0x04000027 RID: 39
		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		// Token: 0x04000028 RID: 40
		private List<SurveyRandom> lSurveyRandomA = new List<SurveyRandom>();

		// Token: 0x04000029 RID: 41
		private List<SurveyRandom> lSurveyRandomB = new List<SurveyRandom>();

		// Token: 0x0400002A RID: 42
		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		// Token: 0x0400002B RID: 43
		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		// Token: 0x0400002C RID: 44
		private List<SurveyDefine> lPageDefine = new List<SurveyDefine>();

		// Token: 0x0400002D RID: 45
		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		// Token: 0x0400002E RID: 46
		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		// Token: 0x0400002F RID: 47
		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		// Token: 0x04000030 RID: 48
		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		// Token: 0x04000031 RID: 49
		private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

		// Token: 0x04000032 RID: 50
		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		// Token: 0x04000033 RID: 51
		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();
	}
}
