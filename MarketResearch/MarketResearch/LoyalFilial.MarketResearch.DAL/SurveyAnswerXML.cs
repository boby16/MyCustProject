using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using LoyalFilial.MarketResearch.Entities;

namespace LoyalFilial.MarketResearch.DAL
{
	public class SurveyAnswerXML : XmlDB<SurveyAnswer>
	{
		public SurveyAnswerXML(string string_1, string string_2)
		{
			base.InitDB(string_1, string_2);
		}

		public bool Exists(string string_1, string string_2)
		{
			SurveyAnswerXML.Class0 @class = new SurveyAnswerXML.Class0();
			@class.surveyID = string_1;
			@class.questionName = string_2;
			string name = typeof(SurveyAnswer).Name;
			XElement xelement = base.XDB.Elements(name).Where(new Func<XElement, bool>(@class.method_0)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		[CompilerGenerated]
		private sealed class Class0
		{
			internal bool method_0(XElement xelement_0)
			{
				return xelement_0.Element("SURVEY_ID").Value == this.surveyID && xelement_0.Element("QUESTION_NAME").Value == this.questionName;
			}

			public string surveyID;

			public string questionName;
		}
	}
}
