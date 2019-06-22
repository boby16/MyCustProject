using System;
using System.Reflection;
using System.Xml.Linq;

namespace Gssy.Capi.DAL
{
	public static class XElementHelper
	{
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
