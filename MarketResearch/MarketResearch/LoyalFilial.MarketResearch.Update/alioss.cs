using System;

namespace LoyalFilial.MarketResearch.Update
{
    /// <summary>
    /// �������ļ��ϴ�
    /// </summary>
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

		private void method_0(string server, bool debug, string bucketDir)
		{
			this.accessId = "JvHEr8eZMOo0JOy7";
			this.accessKey = "vxcNrCYZL06Zb9xaJkcpr450QvVnHA";
			this.BigFilePartSize = 150;
			if (debug)
			{
				this.bucketDir = "debug" + bucketDir;
			}
			else
			{
				this.bucketDir = bucketDir;
			}
			this.bucketDirUpdate = bucketDir + "update";
			string uriString = "";
			string uriString2 = "";

			if (!(server == "hz"))
			{
				if (!(server == "qd"))
				{
					if (!(server == "bj"))
					{
						if (!(server == "hk"))
						{
                            //���ڷ�����
							uriString = "http://oss-cn-shenzhen.aliyuncs.com";
							uriString2 = "http://oss-cn-shenzhen-internal.aliyuncs.com";
							this.bucketName = "marketresearch";
							this.bucketNameUpdate = "marketresearchupdate";
						}
						else
						{
                            //��۷�����
							uriString = "http://oss-cn-hongkong.aliyuncs.com";
							uriString2 = "http://oss-cn-hongkong-internal.aliyuncs.com";
							this.bucketName = "marketresearchhk";
							this.bucketNameUpdate = "marketresearchupdatehk";
						}
					}
					else
					{
                        //����������
						uriString = "http://oss-cn-beijing.aliyuncs.com";
						uriString2 = "http://oss-cn-beijing-internal.aliyuncs.com";
						this.bucketName = "marketresearchbj";
						this.bucketNameUpdate = "marketresearchupdatebj";
					}
				}
				else
				{
                    //�ൺ������
					uriString = "http://oss-cn-qingdao.aliyuncs.com";
					uriString2 = "http://oss-cn-qingdao-internal.aliyuncs.com";
					this.bucketName = "marketresearchqd";
					this.bucketNameUpdate = "marketresearchupdateqd";
				}
			}
			else
			{
                //���ݰ�����
				uriString = "http://oss.aliyuncs.com";
				uriString2 = "http://oss-internal.aliyuncs.com";
				this.bucketName = "marketresearchhz";
				this.bucketNameUpdate = "marketresearchupdatehz";
			}
			this.endpoint = new Uri(uriString);
			this.endpointInner = new Uri(uriString2);
		}

		public int BigFilePartSize = 150;
	}
}
