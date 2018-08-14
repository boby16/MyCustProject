using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000022 RID: 34
	public abstract class XmlDB<T> where T : class
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00013754 File Offset: 0x00011954
		public XElement XDB
		{
			get
			{
				return this.m_XDB.Root;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00013770 File Offset: 0x00011970
		public string DBFileName
		{
			get
			{
				return this.m_DBFileName;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00002345 File Offset: 0x00000545
		protected void InitDB(string string_1, string string_2)
		{
			this.m_DBFileName = Path.Combine(string_1, string_2);
			this.m_XDB = XDocument.Load(this.m_DBFileName);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00013788 File Offset: 0x00011988
		public virtual bool Exists(int int_0)
		{
			XmlDB<T>.Class2 @class = new XmlDB<T>.Class2();
			@class.key = int_0;
			string name = typeof(T).Name;
			XElement xelement = this.XDB.Elements(name).Where(new Func<XElement, bool>(@class.Exists)).FirstOrDefault<XElement>();
			return xelement != null;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000137E0 File Offset: 0x000119E0
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

		// Token: 0x060001E1 RID: 481 RVA: 0x0001385C File Offset: 0x00011A5C
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
					int.Parse(xelement.Element(global::GClass0.smethod_0("KŅ")).Value);
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

		// Token: 0x060001E2 RID: 482 RVA: 0x000138D8 File Offset: 0x00011AD8
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
				int int_ = int.Parse(xelement.Element(global::GClass0.smethod_0("KŅ")).Value);
				this.Delete(int_);
				result = this.Add(gparam_0);
			}
			return result;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001392C File Offset: 0x00011B2C
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

		// Token: 0x04000028 RID: 40
		private XDocument m_XDB;

		// Token: 0x04000029 RID: 41
		private string m_DBFileName;

		// Token: 0x02000025 RID: 37
		[CompilerGenerated]
		private sealed class Class2
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00002394 File Offset: 0x00000594
			internal bool Exists(XElement xelement_0)
			{
				return xelement_0.Element(global::GClass0.smethod_0("KŅ")).Value == this.key.ToString();
			}

			// Token: 0x0400002D RID: 45
			public int key;
		}

		// Token: 0x02000026 RID: 38
		[CompilerGenerated]
		private sealed class Class3
		{
			// Token: 0x060001EC RID: 492 RVA: 0x000023C0 File Offset: 0x000005C0
			internal bool GetByID(XElement xelement_0)
			{
				return xelement_0.Element(global::GClass0.smethod_0("KŅ")).Value == this.key.ToString();
			}

			// Token: 0x0400002E RID: 46
			public int key;
		}

		// Token: 0x02000027 RID: 39
		[CompilerGenerated]
		private sealed class Class4
		{
			// Token: 0x060001EE RID: 494 RVA: 0x000023EC File Offset: 0x000005EC
			internal bool Delete(XElement xelement_0)
			{
				return xelement_0.Element(global::GClass0.smethod_0("KŅ")).Value == this.key.ToString();
			}

			// Token: 0x0400002F RID: 47
			public int key;
		}
	}
}
