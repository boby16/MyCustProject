using System;
using System.Collections.Generic;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	// Token: 0x0200006F RID: 111
	public static class SurveyHelper
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x000042D7 File Offset: 0x000024D7
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x000042DE File Offset: 0x000024DE
		public static DateTime PageStartTime { get; set; }

		// Token: 0x060006B0 RID: 1712 RVA: 0x000A0D94 File Offset: 0x0009EF94
		public static void SurveyExtend_Add(string string_0, string string_1, string string_2, int int_0)
		{
			VEAnswer veanswer = new VEAnswer();
			veanswer.QUESTION_NAME = string_0;
			veanswer.CODE = string_1;
			veanswer.CODE_TEXT = string_2;
			veanswer.SEQUENCE_ID = int_0;
			SurveyHelper.SurveyExtend.Add(veanswer);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000A0DD0 File Offset: 0x0009EFD0
		public static void SurveyExtend_Remove(string string_0, string string_1, string string_2, int int_0)
		{
			VEAnswer veanswer = new VEAnswer();
			veanswer.QUESTION_NAME = string_0;
			veanswer.CODE = string_1;
			veanswer.CODE_TEXT = string_2;
			veanswer.SEQUENCE_ID = int_0;
			SurveyHelper.SurveyExtend.Remove(veanswer);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000228F File Offset: 0x0000048F
		public static void Init()
		{
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000228F File Offset: 0x0000048F
		public static void Load()
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000228F File Offset: 0x0000048F
		public static void Save()
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000A0E0C File Offset: 0x0009F00C
		public static string ShowInfo()
		{
			return global::GClass0.smethod_0("") + Environment.NewLine + global::GClass0.smethod_0("$ĥȪ̫Шԩخܯࠬ鳾奸按巋廎死༷ဴᄵሺጻᐸᔹᘾ᜿ᠼ") + Environment.NewLine + global::GClass0.smethod_0("闩剱紓僳йԢء") + SurveyHelper.SurveyID + Environment.NewLine + global::GClass0.smethod_0("埋弆ȹ̢С") + SurveyHelper.SurveyCity + Environment.NewLine + global::GClass0.smethod_0("*īȨ̩Юԯجܭ࠲勲蠧梫帽ഷิ်༵ᄻሸጹᐾᔿᘼ") + Environment.NewLine + global::GClass0.smethod_0("闩剱岊儓йԢء") + SurveyHelper.SurveySequence.ToString() + Environment.NewLine + global::GClass0.smethod_0("彃卂驻͛щՙٙ݀ࡇॉਖ਼ୌీഹย༡") + SurveyHelper.RoadMapVersion + Environment.NewLine + global::GClass0.smethod_0("彞十驾͚шՏقݙࡌीਹଢడ") + SurveyHelper.NavCurPage + Environment.NewLine + global::GClass0.smethod_0("彜千驸͊ф՘لݗࡉेੈୁహഢม") + SurveyHelper.CurPageName + Environment.NewLine + global::GClass0.smethod_0("寻茬曈䱘йԢء") + SurveyHelper.NavOperation + Environment.NewLine + global::GClass0.smethod_0("$ĥȪ̫Шԩخܯࠬर喥碡忏桼ห༷ဴᄵሺጻᐸᔹᘾ᜿ᠼ") + Environment.NewLine + global::GClass0.smethod_0("Oĭ幎岡瞥鶑炌墭箩椾潴ହఢഡ") + SurveyHelper.CircleACount.ToString() + Environment.NewLine + global::GClass0.smethod_0("Qį幌岧瞣鶓炎塚婅羃喬碪楴ഹย༡") + SurveyHelper.CircleACurrent.ToString() + Environment.NewLine + global::GClass0.smethod_0("Qį幌岧瞣鶓炎䤲䝛餎鉿䗦琅ഹย༡") + SurveyHelper.CircleACode + Environment.NewLine + global::GClass0.smethod_0("Qį幌岧瞣鶓炎䤲䝛餎鉿媀垽ഹย༡") + SurveyHelper.CircleACodeText + Environment.NewLine + global::GClass0.smethod_0("Lĭ幎岡瞥鶑炌墭箩椾潴ହఢഡ") + SurveyHelper.CircleBCount.ToString() + Environment.NewLine + global::GClass0.smethod_0("Rį幌岧瞣鶓炎塚婅羃喬碪楴ഹย༡") + SurveyHelper.CircleBCurrent.ToString() + Environment.NewLine + global::GClass0.smethod_0("Rį幌岧瞣鶓炎䤲䝛餎鉿䗦琅ഹย༡") + SurveyHelper.CircleBCode + Environment.NewLine + global::GClass0.smethod_0("Rį幌岧瞣鶓炎䤲䝛餎鉿媀垽ഹย༡") + SurveyHelper.CircleBCodeText;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x000A0FF0 File Offset: 0x0009F1F0
		public static string ShowPageAnswer(List<VEAnswer> list_0)
		{
			string text = global::GClass0.smethod_0("'Ĥȥ̪ЫԨةܮ࠯ऱ啃奂鑻癙晄༫့ᄴስጺᐻᔸᘹ᜾ᠿ᤼");
			foreach (VEAnswer veanswer in list_0)
			{
				text = string.Concat(new string[]
				{
					text,
					Environment.NewLine,
					global::GClass0.smethod_0("颜勴ȸ̡"),
					veanswer.QUESTION_NAME,
					global::GClass0.smethod_0("(ħḀ̇笒紂ظܡ"),
					veanswer.CODE
				});
			}
			text = text + Environment.NewLine + SurveyHelper.ShowInfo();
			return text;
		}

		// Token: 0x04000C4A RID: 3146
		public static string SurveyStart = global::GClass0.smethod_0("GŊɖ͘");

		// Token: 0x04000C4B RID: 3147
		public static string SurveyFirstPage = global::GClass0.smethod_0("Xşɛ͞т՟ٚݑࡐे੓");

		// Token: 0x04000C4C RID: 3148
		public static string SurveyLastPage = global::GClass0.smethod_0("@");

		// Token: 0x04000C4D RID: 3149
		public static string SurveyCodePage = global::GClass0.smethod_0("Xşɛ͞т՟ٚ݇ࡌॆ੄");

		// Token: 0x04000C4E RID: 3150
		public static int SurveySequence = 0;

		// Token: 0x04000C4F RID: 3151
		public static string NavCurPage = global::GClass0.smethod_0("");

		// Token: 0x04000C50 RID: 3152
		public static string CurPageName = global::GClass0.smethod_0("");

		// Token: 0x04000C51 RID: 3153
		public static int NavLoad = 0;

		// Token: 0x04000C52 RID: 3154
		public static string NavOperation = global::GClass0.smethod_0("HŪɶͮѣխ");

		// Token: 0x04000C53 RID: 3155
		public static int NavGoBackTimes = 0;

		// Token: 0x04000C54 RID: 3156
		public static string RoadMapVersion = global::GClass0.smethod_0("1");

		// Token: 0x04000C55 RID: 3157
		public static string IsTouch = global::GClass0.smethod_0("DſɟͥѼիٯݙࡣ॥੯ୱ౤");

		// Token: 0x04000C56 RID: 3158
		public static bool HistoryMode = false;

		// Token: 0x04000C57 RID: 3159
		public static bool OptionMode = false;

		// Token: 0x04000C58 RID: 3160
		public static string DebugFlagFile = global::GClass0.smethod_0("Jķɐ͟я՚ٜݔࡃ॔ਪୗౚൕ");

		// Token: 0x04000C59 RID: 3161
		public static bool Debug = false;

		// Token: 0x04000C5A RID: 3162
		public static bool TestVersion = false;

		// Token: 0x04000C5B RID: 3163
		public static bool IsDevelopment = false;

		// Token: 0x04000C5C RID: 3164
		public static string ShowAutoFill = global::GClass0.smethod_0("BŸɠ͹ьչٿݥࡏॡ੫୪ౚ൰๱ཷၤ");

		// Token: 0x04000C5D RID: 3165
		public static string FillMode = global::GClass0.smethod_0("2");

		// Token: 0x04000C5E RID: 3166
		public static bool AutoFill = false;

		// Token: 0x04000C5F RID: 3167
		public static bool AutoCapture = false;

		// Token: 0x04000C60 RID: 3168
		public static double Screen_LeftTop = 0.0;

		// Token: 0x04000C61 RID: 3169
		public static string StopFillPage = global::GClass0.smethod_0(" ĳȢ");

		// Token: 0x04000C62 RID: 3170
		public static string Only1CodeMode1 = global::GClass0.smethod_0(" ĳȢ");

		// Token: 0x04000C63 RID: 3171
		public static string Only1CodeMode2 = global::GClass0.smethod_0(" İȢ");

		// Token: 0x04000C64 RID: 3172
		public static string Only1CodeMode3 = global::GClass0.smethod_0(" ıȢ");

		// Token: 0x04000C65 RID: 3173
		public static string Only1CodeMode4 = global::GClass0.smethod_0(" ĶȢ");

		// Token: 0x04000C66 RID: 3174
		public static string ShowAutoDo = global::GClass0.smethod_0("\\Ŧɢͻъտٽݧࡃ३ਗ਼୰౱൷๤");

		// Token: 0x04000C67 RID: 3175
		public static bool AutoDo = false;

		// Token: 0x04000C68 RID: 3176
		public static List<string> AutoDo_listCity = new List<string>();

		// Token: 0x04000C69 RID: 3177
		public static int AutoDo_CityOrder = 1;

		// Token: 0x04000C6A RID: 3178
		public static int AutoDo_StartOrder = 0;

		// Token: 0x04000C6B RID: 3179
		public static int AutoDo_Total = 0;

		// Token: 0x04000C6C RID: 3180
		public static int AutoDo_Count = 1;

		// Token: 0x04000C6D RID: 3181
		public static int AutoDo_Create = 0;

		// Token: 0x04000C6E RID: 3182
		public static int AutoDo_Exist = 0;

		// Token: 0x04000C6F RID: 3183
		public static int AutoDo_Filled = 0;

		// Token: 0x04000C70 RID: 3184
		public static string Survey_Status = global::GClass0.smethod_0("");

		// Token: 0x04000C71 RID: 3185
		public static DateTime AutoDo_Start;

		// Token: 0x04000C72 RID: 3186
		public static bool RecordIsRunning = false;

		// Token: 0x04000C73 RID: 3187
		public static string RecordFileName = global::GClass0.smethod_0("");

		// Token: 0x04000C74 RID: 3188
		public static int RecordHz = 22050;

		// Token: 0x04000C75 RID: 3189
		public static int RecordDevice = 0;

		// Token: 0x04000C76 RID: 3190
		public static string MP3MaxMinutes = global::GClass0.smethod_0("2ĺȱ");

		// Token: 0x04000C77 RID: 3191
		public static DateTime RecordStartTime = DateTime.Now;

		// Token: 0x04000C78 RID: 3192
		public static string QueryEditSurveyId = global::GClass0.smethod_0("");

		// Token: 0x04000C79 RID: 3193
		public static string QueryEditQn = global::GClass0.smethod_0("");

		// Token: 0x04000C7A RID: 3194
		public static string QueryEditQTitle = global::GClass0.smethod_0("");

		// Token: 0x04000C7B RID: 3195
		public static string QueryEditQName = global::GClass0.smethod_0("");

		// Token: 0x04000C7C RID: 3196
		public static string QueryEditDetailID = global::GClass0.smethod_0("");

		// Token: 0x04000C7D RID: 3197
		public static string QueryEditCODE = global::GClass0.smethod_0("");

		// Token: 0x04000C7E RID: 3198
		public static string QueryEditCODE_TEXT = global::GClass0.smethod_0("");

		// Token: 0x04000C7F RID: 3199
		public static int QueryEditSequence = 0;

		// Token: 0x04000C80 RID: 3200
		public static bool QueryEditConfirm = false;

		// Token: 0x04000C81 RID: 3201
		public static bool QueryEditMemModel = false;

		// Token: 0x04000C82 RID: 3202
		public static string AttachSurveyId = global::GClass0.smethod_0("");

		// Token: 0x04000C83 RID: 3203
		public static string AttachQName = global::GClass0.smethod_0("");

		// Token: 0x04000C84 RID: 3204
		public static string AttachPageId = global::GClass0.smethod_0("");

		// Token: 0x04000C85 RID: 3205
		public static int AttachCount = 0;

		// Token: 0x04000C86 RID: 3206
		public static bool AttachReadOnlyModel = false;

		// Token: 0x04000C87 RID: 3207
		public static string SurveyID = global::GClass0.smethod_0("");

		// Token: 0x04000C88 RID: 3208
		public static string SurveyCity = global::GClass0.smethod_0("");

		// Token: 0x04000C89 RID: 3209
		public static string SurveyCityText = global::GClass0.smethod_0("");

		// Token: 0x04000C8B RID: 3211
		public static int CircleACount = 0;

		// Token: 0x04000C8C RID: 3212
		public static int CircleACurrent = 0;

		// Token: 0x04000C8D RID: 3213
		public static string CircleACode = global::GClass0.smethod_0("");

		// Token: 0x04000C8E RID: 3214
		public static string CircleACodeText = global::GClass0.smethod_0("");

		// Token: 0x04000C8F RID: 3215
		public static int CircleBCount = 0;

		// Token: 0x04000C90 RID: 3216
		public static int CircleBCurrent = 0;

		// Token: 0x04000C91 RID: 3217
		public static string CircleBCode = global::GClass0.smethod_0("");

		// Token: 0x04000C92 RID: 3218
		public static string CircleBCodeText = global::GClass0.smethod_0("");

		// Token: 0x04000C93 RID: 3219
		public static string SurveyExtend1 = global::GClass0.smethod_0("");

		// Token: 0x04000C94 RID: 3220
		public static string SurveyExtend2 = global::GClass0.smethod_0("");

		// Token: 0x04000C95 RID: 3221
		public static string SurveyExtend3 = global::GClass0.smethod_0("");

		// Token: 0x04000C96 RID: 3222
		public static string Answer = global::GClass0.smethod_0("");

		// Token: 0x04000C97 RID: 3223
		public static List<VEAnswer> SurveyExtend = new List<VEAnswer>();

		// Token: 0x04000C98 RID: 3224
		public static int BtnFontSize = 26;

		// Token: 0x04000C99 RID: 3225
		public static int BtnHeight = 46;

		// Token: 0x04000C9A RID: 3226
		public static int BtnWidth = 280;

		// Token: 0x04000C9B RID: 3227
		public static int BtnMediumFontSize = 22;

		// Token: 0x04000C9C RID: 3228
		public static int BtnMediumHeight = 40;

		// Token: 0x04000C9D RID: 3229
		public static int BtnMediumWidth = 280;

		// Token: 0x04000C9E RID: 3230
		public static int BtnSmallFontSize = 18;

		// Token: 0x04000C9F RID: 3231
		public static int BtnSmallHeight = 36;

		// Token: 0x04000CA0 RID: 3232
		public static int BtnSmallWidth = 280;

		// Token: 0x04000CA1 RID: 3233
		public static List<SurveyDefine> G_SurveyDefine;

		// Token: 0x04000CA2 RID: 3234
		public static List<SurveyDetail> G_SurveyDetail;

		// Token: 0x04000CA3 RID: 3235
		public static List<SurveyRoadMap> G_SurveyRoadMap;

		// Token: 0x04000CA4 RID: 3236
		public static List<SurveyLogic> G_SurveyLogic;
	}
}
