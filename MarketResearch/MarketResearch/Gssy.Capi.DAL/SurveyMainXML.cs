using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000020 RID: 32
	public class SurveyMainXML : XmlDB<SurveyMain>
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00002335 File Offset: 0x00000535
		public SurveyMainXML(string string_1, string string_2)
		{
			base.InitDB(string_1, string_2);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000135D0 File Offset: 0x000117D0
		public bool Exists(string string_1)
		{
			SurveyMainXML.Class1 @class = new SurveyMainXML.Class1();
			@class.surveyID = string_1;
			string name = typeof(SurveyAnswer).Name;
			XElement xelement = base.XDB.Elements(name).Where(new Func<XElement, bool>(@class.method_0)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		// Token: 0x02000024 RID: 36
		[CompilerGenerated]
		private sealed class Class1
		{
			// Token: 0x060001E8 RID: 488 RVA: 0x0000236D File Offset: 0x0000056D
			internal bool method_0(XElement xelement_0)
			{
				return xelement_0.Element(global::GClass0.smethod_0("Zŝɕ͐р՝ٜ݋ࡅ")).Value == this.surveyID;
			}

			// Token: 0x0400002C RID: 44
			public string surveyID;
		}
	}
}
