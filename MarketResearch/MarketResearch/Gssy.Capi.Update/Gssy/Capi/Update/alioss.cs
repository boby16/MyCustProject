using System;

namespace Gssy.Capi.Update
{
	public class alioss
	{
		public string accessId { get; private set; }

		public string accessKey { get; private set; }

		public string bucketName { get; private set; }

		public string bucketNameUpdate { get; private set; }

		public Uri endpoint { get; private set; }

		public Uri endpointInner { get; private set; }

		public string bucketDir { get; private set; }

		public string bucketDirUpdate { get; private set; }

		public alioss(string string_0, bool bool_0, string string_1)
		{
			this.method_0(string_0, bool_0, string_1);
		}

		private void method_0(string string_0, bool bool_0, string string_1)
		{
			this.accessId = "JvHEr8eZMOo0JOy7";
			this.accessKey = "vxcNrCYZL06Zb9xaJkcpr450QvVnHA";
			this.BigFilePartSize = 150;
			if (bool_0)
			{
				this.bucketDir = "debug" + string_1;
			}
			else
			{
				this.bucketDir = string_1;
			}
			this.bucketDirUpdate = string_1 + "update";
			string uriString = "";
			string uriString2 = "";
			if (!(string_0 == "hz"))
			{
				if (!(string_0 == "qd"))
				{
					if (!(string_0 == "bj"))
					{
						if (!(string_0 == "hk"))
						{
							uriString = "http://oss-cn-shenzhen.aliyuncs.com";
							uriString2 = "http://oss-cn-shenzhen-internal.aliyuncs.com";
							this.bucketName = "capi";
							this.bucketNameUpdate = "capiupdate";
						}
						else
						{
							uriString = "http://oss-cn-hongkong.aliyuncs.com";
							uriString2 = "http://oss-cn-hongkong-internal.aliyuncs.com";
							this.bucketName = "capihk";
							this.bucketNameUpdate = "capiupdatehk";
						}
					}
					else
					{
						uriString = "http://oss-cn-beijing.aliyuncs.com";
						uriString2 = "http://oss-cn-beijing-internal.aliyuncs.com";
						this.bucketName = "capibj";
						this.bucketNameUpdate = "capiupdatebj";
					}
				}
				else
				{
					uriString = "http://oss-cn-qingdao.aliyuncs.com";
					uriString2 = "http://oss-cn-qingdao-internal.aliyuncs.com";
					this.bucketName = "capiqd";
					this.bucketNameUpdate = "capiupdateqd";
				}
			}
			else
			{
				uriString = "http://oss.aliyuncs.com";
				uriString2 = "http://oss-internal.aliyuncs.com";
				this.bucketName = "capihz";
				this.bucketNameUpdate = "capiupdatehz";
			}
			this.endpoint = new Uri(uriString);
			this.endpointInner = new Uri(uriString2);
		}

		public int BigFilePartSize = 150;
	}
}
