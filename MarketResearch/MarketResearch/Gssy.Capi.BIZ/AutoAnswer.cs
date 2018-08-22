using System;
using System.Collections.Generic;
using System.Text;
using Gssy.Capi.DAL;
using Gssy.Capi.Entities;

namespace Gssy.Capi.BIZ
{
	public class AutoAnswer
	{
		public string SurveyId { get; set; }

		public string RouteLogic { get; set; }

		public string GroupCodeA { get; set; }

		public string CircleACode { get; set; }

		public string CircleCodeTextA { get; set; }

		public int CircleACount { get; set; }

		public int CircleACurrent { get; set; }

		public bool IsLastA { get; set; }

		public string GroupCodeB { get; set; }

		public string CircleBCode { get; set; }

		public string CircleCodeTextB { get; set; }

		public int CircleBCount { get; set; }

		public int CircleBCurrent { get; set; }

		public bool IsLastB { get; set; }

		public string MyAnswer { get; set; }

		public AutoAnswer()
		{
			this.method_0();
		}

		public void SurveyInit(string string_0, string string_1, int int_0, string string_2, string string_3)
		{
			this.oRandom.RandomSurveyMain(string_0);
			string string_4 = GClass0.smethod_0("4ĵȵ̷в԰");
			string string_5 = "1001";
			this.oSurvey.AddSurvey(string_0, string_1, string_2, string_4, string_5, string_3);
			QFill qfill = new QFill();
			qfill.Init(GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄"), 0);
			qfill.FillText = string_0;
			qfill.BeforeSave();
			qfill.Save(string_0, int_0);
			this.oSurveyAnswerDal.AddOneNoUpdate(string_0, GClass0.smethod_0("Jşɚ͍ѕՙقݖࡌॗੑ"), string_3, int_0);
		}

		public string GetChildByIndex(string string_0, int int_0)
		{
			return this.oSurveyDefineDal.GetChildByIndex(string_0, int_0);
		}

		public void QuestionInit(string string_0, string string_1)
		{
			this.MySurveyDefine = this.oSurveyDefineDal.GetByName(string_1);
			this.MySurveyRoadMap = this.oSurveyRoadMapDal.GetByPageId(this.MySurveyDefine.PAGE_ID, "0");
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				this.lSurveyDetail = this.oSurveyDetailDal.GetDetails(this.MySurveyDefine.DETAIL_ID);
			}
			this.lSurveyLogic = this.oSurveyLogicDal.GetCheckLogic(this.MySurveyDefine.PAGE_ID);
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				this.lSurveyRandomA = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEA);
				this.lSurveyRandomB = this.oSurveyRandomDal.GetList(string_0, this.MySurveyDefine.GROUP_CODEB);
			}
		}

		public int PageInfo(string string_0)
		{
			this.lPageDefine = this.oSurveyDefineDal.GetListByPageId(string_0);
			return this.lPageDefine.Count;
		}

		public string QuestionInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GClass0.smethod_0("\vĈȉ̎ЏԌ؍ܒࠓऐ਑ଖగഔจུၓᅗቒፆᑛᕥᙅ᝹ᡷᥳ᩹ᬻ᱓ᵷṾὸ⁤ⅸ≵⍧⑻╾♾✯⠳⤰⨱⬶ⰷⴴ⸵⼺〻ㄸ㈹㌾㐿㔼"));
			stringBuilder.AppendLine();
			if (this.MySurveyDefine.ID > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("臣厠紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_ORDER > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("颓矤趚勲鱽安إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.ANSWER_ORDER.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_ID != "")
			{
				stringBuilder.Append(GClass0.smethod_0("顽縑凱̥Фԯآܡ"));
				stringBuilder.Append(this.MySurveyDefine.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_NAME != "")
			{
				stringBuilder.Append(GClass0.smethod_0("闧馐紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TITLE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("串馐骟嵴ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("串馐骟咍ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_USE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("辘僰韧鮐䭸瀮إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.ANSWER_USE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("辞僶韥鮒罝浀䥸爮ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.ANSWER_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.COMBINE_INDEX > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("绉唄骓疎彙鶐笥堓ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.COMBINE_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.DETAIL_ID != "")
			{
				stringBuilder.Append(GClass0.smethod_0("逅揢驳剺葜稑縇ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.DETAIL_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PARENT_CODE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("夕義鈄懥鱲眼坺蝜眑焇ਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.PARENT_CODE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_CONTENT != "")
			{
				stringBuilder.Append(GClass0.smethod_0("剦馐骟嵴ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.QUESTION_CONTENT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_TITLE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("_śə͚Ш鶟塴ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_CASE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("_śə͚Ш鶟再ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_CASE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_VARIABLE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("]ŝɟ͘Ъ囑韇筼徍थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_VARIABLE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SPSS_PRINT_DECIMAIL > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("^Ŝɘ͙Щ备捷䡋ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.SPSS_PRINT_DECIMAIL.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MIN_COUNT > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("夔鄄骔疏挊夆阁旮浶थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.MIN_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MAX_COUNT > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("夔鄄骔疏挊尮阁旮浶थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.MAX_COUNT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.IS_RANDOM > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("冊釦鈄魵戤儬邆怲殕嬑ਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.IS_RANDOM.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.PAGE_COUNT_DOWN > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("项說柼匛慸ԧ翔ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.PAGE_COUNT_DOWN.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TYPE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("掫俽璎署厃暠吰ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_FONTSIZE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("掫俽璎塞䭛尠娉ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_HEIGHT > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("掭俿璌駟媠ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_HEIGHT.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_WIDTH > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("掭俿璌墺媠ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_WIDTH.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_MASK != "")
			{
				stringBuilder.Append(GClass0.smethod_0("掫俽Ȫ̈́ѩմ٭ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_MASK);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.TITLE_FONTSIZE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("闠馕樋鮓犎幞䥛帠吉थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.TITLE_FONTSIZE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.CONTROL_TOOLTIP != "")
			{
				stringBuilder.Append(GClass0.smethod_0("推俸ȭ͘Ѥե٥ݜ࡮ॶਥତయഢม"));
				stringBuilder.Append(this.MySurveyDefine.CONTROL_TOOLTIP);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.NOTE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("夕淹觤攁ЮԢج愵焰醑粌媂垿ഥฤ༯ဢᄡ"));
				stringBuilder.Append(this.MySurveyDefine.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.LIMIT_LOGIC != "")
			{
				stringBuilder.Append(GClass0.smethod_0("亜厥鑜儽犎锲覙撠娰थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyDefine.LIMIT_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL != "")
			{
				stringBuilder.Append(GClass0.smethod_0("御犥骑緌窠圭إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_LEVEL);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEA != "")
			{
				stringBuilder.Append(GClass0.smethod_0("徻犿骗緊瘻媦疤䧩瀈ॉਧ坄థതฯ༢အ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEA);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_CODEB != "")
			{
				stringBuilder.Append(GClass0.smethod_0("徻犿骗緊瘻媦疤䧩瀈ॊਧ坄థതฯ༢အ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_CODEB);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_PAGE_TYPE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("闼馉唸岥瞡鶕磈䤦纎䙄畦睼宍ഥฤ༯ဢᄡ"));
				stringBuilder.Append(this.MySurveyDefine.GROUP_PAGE_TYPE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_MSG != "")
			{
				stringBuilder.Append(GClass0.smethod_0("徦犤骒緍鲐䫦晩ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_MSG);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.MT_GROUP_COUNT != "")
			{
				stringBuilder.Append(GClass0.smethod_0("徧犣骓緎釧鶐捷雉ࠥतਯଢడ"));
				stringBuilder.Append(this.MySurveyDefine.MT_GROUP_COUNT);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_USE > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("昦售晟誇ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_USE.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_TITLE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("摑袉樀鮞ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_TITLE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.SUMMARY_INDEX > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("摑袉缥尓ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyDefine.SUMMARY_INDEX.ToString());
				stringBuilder.AppendLine();
			}
			stringBuilder.Append(GClass0.smethod_0("\nċȈ̉Ўԏ،܍ࠒओਐ଑ఖഗฉཻၒᅔቓፁᑚᕰᙎᝁ᡻ᥓ᭬᩼᰻ᵓṷ὾⁸Ⅴ≸⍵⑧╻♾❾⠯⤳⨰⬱ⰶⴷ⸴⼵〺ㄻ㈸㌹㐾㔿㘼"));
			stringBuilder.AppendLine();
			if (this.MySurveyRoadMap.ID > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("臣厠紑僱ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.VERSION_ID > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("跤琻灁搤笑囱إܤ࠯ढਡ"));
				stringBuilder.Append(this.MySurveyRoadMap.VERSION_ID.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PART_NAME != "")
			{
				stringBuilder.Append(GClass0.smethod_0("闧剿倁鏮ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PART_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_NOTE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("顽諳搈̥Фԯآܡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.PAGE_ID != "")
			{
				stringBuilder.Append(GClass0.smethod_0("顼ĨɎ͂ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.PAGE_ID);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.ROUTE_LOGIC != "")
			{
				stringBuilder.Append(GClass0.smethod_0("项賸赦軦焹锼覗ܥࠤयਢଡ"));
				stringBuilder.Append(this.MySurveyRoadMap.ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.GROUP_ROUTE_LOGIC != "")
			{
				stringBuilder.Append(GClass0.smethod_0("徤犢糈総喏裺襤霼螗थਤଯఢഡ"));
				stringBuilder.Append(this.MySurveyRoadMap.GROUP_ROUTE_LOGIC);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.FORM_NAME != "")
			{
				stringBuilder.Append(GClass0.smethod_0("顼笃岈國ХԤدܢࠡ"));
				stringBuilder.Append(this.MySurveyRoadMap.FORM_NAME);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.IS_JUMP > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("春唯凧軴譪ԥؤܯࠢड"));
				stringBuilder.Append(this.MySurveyRoadMap.IS_JUMP.ToString());
				stringBuilder.AppendLine();
			}
			if (this.MySurveyRoadMap.NOTE != "")
			{
				stringBuilder.Append(GClass0.smethod_0("夀淮ȥ̤ЯԢء"));
				stringBuilder.Append(this.MySurveyRoadMap.NOTE);
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.QUESTION_TYPE == 2 || this.MySurveyDefine.QUESTION_TYPE == 3)
			{
				stringBuilder.Append(GClass0.smethod_0("\vĈȉ̎ЏԌ؍ܒࠓऐ਑ଖగഔจུၓᅗቒፆᑛᕥᙅᝫ᡿ᥴᩰᬻ᱓ᵷṾὸ⁤ⅸ≵⍧⑻╾♾✯⠳⤰⨱⬶ⰷⴴ⸵⼺〻ㄸ㈹㌾㐿㔼"));
				stringBuilder.AppendLine();
				stringBuilder.Append(GClass0.smethod_0("缄礐Ȱ̯Тԭج砝瀋沎洤ଧపഥ栫嬥䅴志"));
				stringBuilder.AppendLine();
				foreach (SurveyDetail surveyDetail in this.lSurveyDetail)
				{
					stringBuilder.Append(string.Format(GClass0.smethod_0("jĠɲ̮ЭԠثܪࡲह੺ଦ఩ത๸༰ၼ"), surveyDetail.CODE, surveyDetail.CODE_TEXT, surveyDetail.IS_OTHER.ToString()));
					stringBuilder.AppendLine();
				}
			}
			if (this.lSurveyLogic.Count > 0)
			{
				stringBuilder.Append(GClass0.smethod_0("\bĉȎ̏Ќԍؒܓࠐऑਖଗఔകงཱུၐᅖቕፇᑘᕬᙰ᝹ᡴ᥿ᨻ᭓ᱷᵾṸὤ⁸ⅵ≧⍻⑾╾☯✳⠰⤱⨶⬷ⰴⴵ⸺⼻〸ㄹ㈾㌿㐼"));
				stringBuilder.AppendLine();
				foreach (SurveyLogic surveyLogic in this.lSurveyLogic)
				{
					stringBuilder.Append(GClass0.smethod_0("逰躛卥將徝䭏إܤ࠯ढਡ"));
					stringBuilder.Append(surveyLogic.FORMULA);
					stringBuilder.AppendLine();
					stringBuilder.Append(GClass0.smethod_0("揙砲䷦捩ХԤدܢࠡ"));
					stringBuilder.Append(surveyLogic.LOGIC_MESSAGE);
					stringBuilder.AppendLine();
					stringBuilder.Append(GClass0.smethod_0("進躙懈賶ХԤدܢࠡ"));
					stringBuilder.Append(surveyLogic.NOTE);
					stringBuilder.AppendLine();
					stringBuilder.Append(GClass0.smethod_0("兏誵畯壷籤躭分霝蟁थਤଯఢഡ"));
					stringBuilder.Append(surveyLogic.IS_ALLOW_PASS.ToString());
					stringBuilder.AppendLine();
					stringBuilder.AppendLine();
				}
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				stringBuilder.Append(GClass0.smethod_0(" ġȦ̧Фԥتܫࠨऩਮଯబഭฯཝၸᅾች፯ᑰᕚᙦᝨᡡᥫᩮᬢᱚ") + this.MySurveyDefine.GROUP_CODEA + GClass0.smethod_0("AĻɓͷѾո٤ݸࡵ१੻୾౾യำ༰ေᄶሷጴᐵᔺᘻ᜸ᠹ᤾ᨿᬼ"));
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom in this.lSurveyRandomA)
				{
					stringBuilder.Append(surveyRandom.CODE);
					stringBuilder.Append(GClass0.smethod_0("#Įȡ"));
				}
				stringBuilder.AppendLine();
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				stringBuilder.Append(GClass0.smethod_0(" ġȦ̧Фԥتܫࠨऩਮଯబഭฯཝၸᅾች፯ᑰᕚᙦᝨᡡᥫᩮᬢᱚ") + this.MySurveyDefine.GROUP_CODEB + GClass0.smethod_0("AĻɓͷѾո٤ݸࡵ१੻୾౾യำ༰ေᄶሷጴᐵᔺᘻ᜸ᠹ᤾ᨿᬼ"));
				stringBuilder.AppendLine();
				foreach (SurveyRandom surveyRandom2 in this.lSurveyRandomB)
				{
					stringBuilder.Append(surveyRandom2.CODE);
					stringBuilder.Append(GClass0.smethod_0("#Įȡ"));
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

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

		public string GetAutoSurveyId(string string_0, int int_0)
		{
			return this.oSurveyMainDal.GetAutoSurveyId(string_0, int_0);
		}

		public void SetMain(string string_0, string string_1, int int_0)
		{
			string text = string_1;
			if (this.MySurveyDefine.GROUP_LEVEL == "A")
			{
				text = text + "_R" + this.CircleACode;
			}
			if (this.MySurveyDefine.GROUP_LEVEL == "B")
			{
				text = string.Concat(new string[]
				{
					text,
					"_R",
					this.CircleACode,
					"_R",
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

		public void SetSingle(string string_0, string string_1, int int_0)
		{
			QSingle qsingle = new QSingle();
			qsingle.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != "")
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
			if (qsingle.OtherCode != "")
			{
				qsingle.FillText = GClass0.smethod_0("兵俔ȡ") + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + GClass0.smethod_0("#Įȡ") + qsingle.FillText;
			}
			qsingle.QuestionName = string_1;
			qsingle.BeforeSave();
			qsingle.Save(string_0, int_0, true);
		}

		public void SetMultiple(string string_0, string string_1, int int_0)
		{
			QMultiple qmultiple = new QMultiple();
			qmultiple.Init(this.MySurveyDefine.PAGE_ID, this.MySurveyDefine.COMBINE_INDEX, true);
			if (this.MySurveyDefine.PARENT_CODE != "")
			{
				qmultiple.ParentCode = new LogicAnswer
				{
					SurveyID = string_0
				}.GetAnswer(this.MySurveyDefine.PARENT_CODE);
				qmultiple.GetDynamicDetails();
			}
			qmultiple.RandomDetails();
			this.MyAnswer = "";
			foreach (SurveyDetail surveyDetail in qmultiple.QDetails)
			{
				qmultiple.SelectedValues.Add(surveyDetail.CODE);
				this.MyAnswer = this.MyAnswer + surveyDetail.CODE + GClass0.smethod_0("#Įȡ");
			}
			if (qmultiple.OtherCode != "")
			{
				qmultiple.FillText = GClass0.smethod_0("兵俔ȡ") + this.MySurveyDefine.QUESTION_NAME;
				this.MyAnswer = this.MyAnswer + GClass0.smethod_0("#Įȡ") + qmultiple.FillText;
			}
			qmultiple.QuestionName = string_1;
			qmultiple.BeforeSave();
			qmultiple.Save(string_0, int_0);
		}

		public string FixSetSingle(string string_0)
		{
			string result = "";
			this.DAnswer.TryGetValue(string_0, out result);
			return result;
		}

		private void method_0()
		{
			this.DAnswer.Add(GClass0.smethod_0("Qİ"), "5");
			this.DAnswer.Add(GClass0.smethod_0("Qĳ"), "2");
			this.DAnswer.Add(GClass0.smethod_0("QĲ"), "3");
			this.DAnswer.Add(GClass0.smethod_0("Qĵ"), "1");
			this.DAnswer.Add(GClass0.smethod_0("QĶ"), "1");
			this.DAnswer.Add(GClass0.smethod_0("QĹ"), "1");
			this.DAnswer.Add(GClass0.smethod_0("Qĸ"), "1");
			this.DAnswer.Add(GClass0.smethod_0("Pĳȱ"), "2");
			this.DAnswer.Add(GClass0.smethod_0("PĳȲ"), GClass0.smethod_0("1ĸ"));
		}

		private RandomBiz oRandom = new RandomBiz();

		private SurveyBiz oSurvey = new SurveyBiz();

		private Dictionary<string, string> DAnswer = new Dictionary<string, string>();

		public SurveyDefine MySurveyDefine = new SurveyDefine();

		private List<SurveyDetail> lSurveyDetail = new List<SurveyDetail>();

		private SurveyRoadMap MySurveyRoadMap = new SurveyRoadMap();

		private List<SurveyLogic> lSurveyLogic = new List<SurveyLogic>();

		private List<SurveyRandom> lSurveyRandomA = new List<SurveyRandom>();

		private List<SurveyRandom> lSurveyRandomB = new List<SurveyRandom>();

		private List<SurveyMain> lSurveyMain = new List<SurveyMain>();

		private List<SurveyAnswer> lSurveyAnswer = new List<SurveyAnswer>();

		private List<SurveyDefine> lPageDefine = new List<SurveyDefine>();

		private SurveyDefineDal oSurveyDefineDal = new SurveyDefineDal();

		private SurveyDetailDal oSurveyDetailDal = new SurveyDetailDal();

		private SurveyRoadMapDal oSurveyRoadMapDal = new SurveyRoadMapDal();

		private SurveyRandomDal oSurveyRandomDal = new SurveyRandomDal();

		private SurveyLogicDal oSurveyLogicDal = new SurveyLogicDal();

		private SurveyMainDal oSurveyMainDal = new SurveyMainDal();

		private SurveyAnswerDal oSurveyAnswerDal = new SurveyAnswerDal();
	}
}
