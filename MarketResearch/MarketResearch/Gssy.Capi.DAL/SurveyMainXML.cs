using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Gssy.Capi.Entities;

namespace Gssy.Capi.DAL
{
	public class SurveyMainXML : XmlDB<SurveyMain>
	{
		public SurveyMainXML(string string_1, string string_2)
		{
			base.InitDB(string_1, string_2);
		}

		public bool Exists(string string_1)
		{
			SurveyMainXML.Class1 @class = new SurveyMainXML.Class1();
			@class.surveyID = string_1;
			string name = typeof(SurveyAnswer).Name;
			XElement xelement = base.XDB.Elements(name).Where(new Func<XElement, bool>(@class.method_0)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		[CompilerGenerated]
		private sealed class Class1
		{
			internal bool method_0(XElement xelement_0)
			{
				return xelement_0.Element("SURVEY_ID").Value == this.surveyID;
			}

			public string surveyID;
		}
	}
}
