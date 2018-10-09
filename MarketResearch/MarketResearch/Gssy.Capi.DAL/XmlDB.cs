using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace LoyalFilial.MarketResearch.DAL
{
	public abstract class XmlDB<T> where T : class
	{
		public XElement XDB
		{
			get
			{
				return this.m_XDB.Root;
			}
		}

		public string DBFileName
		{
			get
			{
				return this.m_DBFileName;
			}
		}

		protected void InitDB(string string_1, string string_2)
		{
			this.m_DBFileName = Path.Combine(string_1, string_2);
			this.m_XDB = XDocument.Load(this.m_DBFileName);
		}

		public virtual bool Exists(int int_0)
		{
			XmlDB<T>.Class2 @class = new XmlDB<T>.Class2();
			@class.key = int_0;
			string name = typeof(T).Name;
			XElement xelement = this.XDB.Elements(name).Where(new Func<XElement, bool>(@class.Exists)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		public virtual T GetByID(int int_0)
		{
			XmlDB<T>.Class3 @class = new XmlDB<T>.Class3();
			@class.key = int_0;
			T result;
			if (!this.Exists(@class.key))
			{
				result = default(T);
			}
			else
			{
				string name = typeof(T).Name;
				XElement xelement_ = this.XDB.Elements(name).Where(new Func<XElement, bool>(@class.GetByID)).FirstOrDefault<XElement>();
				result = xelement_.ToEntity<T>();
			}
			return result;
		}

		public virtual bool Add(T gparam_0)
		{
			bool result;
			if (gparam_0 == null)
			{
				result = false;
			}
			else
			{
				try
				{
					XElement xelement = XElementHelper.ToXElement<T>(gparam_0);
					int.Parse(xelement.Element("ID").Value);
					this.XDB.Add(xelement);
					this.m_XDB.Save(this.m_DBFileName);
					result = true;
				}
				catch
				{
					result = false;
				}
			}
			return result;
		}

		public virtual bool Update(T gparam_0)
		{
			bool result;
			if (gparam_0 == null)
			{
				result = false;
			}
			else
			{
				XElement xelement = XElementHelper.ToXElement<T>(gparam_0);
				int int_ = int.Parse(xelement.Element("ID").Value);
				this.Delete(int_);
				result = this.Add(gparam_0);
			}
			return result;
		}

		public virtual bool Delete(int int_0)
		{
			XmlDB<T>.Class4 @class = new XmlDB<T>.Class4();
			@class.key = int_0;
			bool result = false;
			try
			{
				string name = typeof(T).Name;
				XElement xelement = this.XDB.Elements(name).Where(new Func<XElement, bool>(@class.Delete)).FirstOrDefault<XElement>();
				if (xelement != null)
				{
					xelement.Remove();
					this.m_XDB.Save(this.m_DBFileName);
					result = true;
				}
			}
			catch
			{
			}
			return result;
		}

		private XDocument m_XDB;

		private string m_DBFileName;

		[CompilerGenerated]
		private sealed class Class2
		{
			internal bool Exists(XElement xelement_0)
			{
				return xelement_0.Element("ID").Value == this.key.ToString();
			}

			public int key;
		}

		[CompilerGenerated]
		private sealed class Class3
		{
			internal bool GetByID(XElement xelement_0)
			{
				return xelement_0.Element("ID").Value == this.key.ToString();
			}

			public int key;
		}

		[CompilerGenerated]
		private sealed class Class4
		{
			internal bool Delete(XElement xelement_0)
			{
				return xelement_0.Element("ID").Value == this.key.ToString();
			}

			public int key;
		}
	}
}
