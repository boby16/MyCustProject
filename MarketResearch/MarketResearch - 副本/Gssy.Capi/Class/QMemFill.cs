using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Gssy.Capi.BIZ;
using Gssy.Capi.Entities;

namespace Gssy.Capi.Class
{
	public class QMemFill : QFill
	{
		public void Init(string string_0, string string_1, int int_0, List<SurveyDefine> list_0)
		{
			QMemFill.Class70 @class = new QMemFill.Class70();
			@class.PageId = string_1;
			@class.CombineIndex = int_0;
			List<SurveyDefine> list = list_0.Where(new Func<SurveyDefine, bool>(@class.method_0)).ToList<SurveyDefine>();
			base.QDefine = list[0];
		}

		[CompilerGenerated]
		private sealed class Class70
		{
			internal bool method_0(SurveyDefine surveyDefine_0)
			{
				return surveyDefine_0.PAGE_ID == this.PageId && surveyDefine_0.COMBINE_INDEX == this.CombineIndex;
			}

			public string PageId;

			public int CombineIndex;
		}
	}
}
