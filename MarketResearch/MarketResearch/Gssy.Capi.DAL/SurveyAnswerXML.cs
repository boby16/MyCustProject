using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x0200001F RID: 31
	public class SurveyAnswerXML : XmlDB<SurveyAnswer>
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x00002325 File Offset: 0x00000525
		public SurveyAnswerXML(string string_1, string string_2)
		{
			base.InitDB(string_1, string_2);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00013570 File Offset: 0x00011770
		public bool Exists(string string_1, string string_2)
		{
			SurveyAnswerXML.Class0 @class = new SurveyAnswerXML.Class0();
			@class.surveyID = string_1;
			@class.questionName = string_2;
			string name = typeof(SurveyAnswer).Name;
			XElement xelement = base.XDB.Elements(name).Where(new Func<XElement, bool>(@class.method_0)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		// Token: 0x02000023 RID: 35
		[CompilerGenerated]
		private sealed class Class0
		{
			// Token: 0x060001E6 RID: 486 RVA: 0x000139B8 File Offset: 0x00011BB8
			internal bool method_0(XElement xelement_0)
			{
				return xelement_0.Element(global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")).Value == this.surveyID && xelement_0.Element(global::GClass0.smethod_0("\\řɎ͙ѝՁو݈࡚ॊੂ୏ౄ")).Value == this.questionName;
			}

			// Token: 0x0400002A RID: 42
			public string surveyID;

			// Token: 0x0400002B RID: 43
			public string questionName;
		}
	}
}
