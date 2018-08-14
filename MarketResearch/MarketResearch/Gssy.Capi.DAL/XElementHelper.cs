using System;
using System.Reflection;
using System.Xml.Linq;

namespace Gssy.Capi.DAL
{
	// Token: 0x02000021 RID: 33
	public static class XElementHelper
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00013628 File Offset: 0x00011828
		public static T ToEntity<T>(this XElement xelement_0)
		{
			T t = default(T);
			Type typeFromHandle = typeof(T);
			PropertyInfo[] properties = typeFromHandle.GetProperties();
			t = (T)((object)Activator.CreateInstance(typeFromHandle));
			foreach (PropertyInfo propertyInfo in properties)
			{
				Type type = propertyInfo.PropertyType;
				type = (Nullable.GetUnderlyingType(type) ?? type);
				object value = (xelement_0.Element(propertyInfo.Name).Value == null) ? null : Convert.ChangeType(xelement_0.Element(propertyInfo.Name).Value, type);
				propertyInfo.SetValue(t, value, null);
			}
			return t;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000136E0 File Offset: 0x000118E0
		public static XElement ToXElement<T>(T gparam_0)
		{
			Type typeFromHandle = typeof(T);
			XElement xelement = new XElement(typeFromHandle.Name);
			PropertyInfo[] properties = typeFromHandle.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				xelement.Add(new XElement(propertyInfo.Name, propertyInfo.GetValue(gparam_0, null)));
			}
			return xelement;
		}
	}
}
