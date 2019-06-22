using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aliyun.OpenServices.OpenStorageService;

namespace Gssy.Capi.BIZ
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

		public OssClient ossClient { get; set; }

		public string OutMessage { get; private set; }

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

		public bool CreateOss()
		{
			this.ossClient = new OssClient(this.endpoint, this.accessId, this.accessKey);
			bool flag = false;
			try
			{
				IEnumerable<Bucket> enumerable = this.ossClient.ListBuckets();
				foreach (Bucket bucket in enumerable)
				{
					if (bucket.Name == this.bucketName)
					{
						flag = true;
						break;
					}
				}
			}
			catch (Exception)
			{
				this.OutMessage = "未能连接服务器,请检查网络!";
				return false;
			}
			bool result;
			if (!flag)
			{
				this.OutMessage = "存储服务器已过期!";
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		public string GetNewUploadSequence(string string_0)
		{
			string text = string_0;
			int num = 100;
			if (text == null)
			{
				text = "";
			}
			if (text != "")
			{
				num = int.Parse(text);
			}
			for (int i = num; i < 999; i++)
			{
				ObjectListing objectListing = this.ossClient.ListObjects(this.bucketName, this.bucketDir + "/" + i.ToString());
				int num2 = objectListing.ObjectSummaries.Count<OssObjectSummary>();
				if (num2 < 70)
				{
					text = i.ToString();
					return text;
				}
			}
			return text;
		}

		public bool UploadToOss(string string_0, string string_1)
		{
			string text = this.bucketDir + "/" + string_1;
			ObjectMetadata objectMetadata = new ObjectMetadata();
			objectMetadata.UserMetadata.Add("SurveyData", string_1);
			string text2 = string_0 + string_1;
			bool flag = false;
			FileInfo fileInfo = new FileInfo(text2);
			if (fileInfo.Length > (long)(1024 * this.BigFilePartSize))
			{
				flag = true;
			}
			try
			{
				if (flag)
				{
					this.mutiPartUpload(this.ossClient, text2, text, this.bucketName, this.BigFilePartSize);
				}
				else
				{
					using (FileStream fileStream = File.Open(text2, FileMode.Open))
					{
						this.ossClient.PutObject(this.bucketName, text, fileStream, objectMetadata);
					}
				}
			}
			catch (Exception)
			{
				this.OutMessage = "文件上传失败,请检查网络!";
				return false;
			}
			return true;
		}

		public void mutiPartUpload(OssClient ossClient_0, string string_0, string string_1, string string_2, int int_0)
		{
			InitiateMultipartUploadRequest initiateMultipartUploadRequest = new InitiateMultipartUploadRequest(string_2, string_1);
			InitiateMultipartUploadResult initiateMultipartUploadResult = ossClient_0.InitiateMultipartUpload(initiateMultipartUploadRequest);
			int num = 1024 * int_0;
			FileInfo fileInfo = new FileInfo(string_0);
			int num2 = (int)(fileInfo.Length / (long)num);
			if (fileInfo.Length % (long)num != 0L)
			{
				num2++;
			}
			List<PartETag> list = new List<PartETag>();
			for (int i = 0; i < num2; i++)
			{
				FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open);
				long num3 = (long)(num * i);
				fileStream.Position = num3;
				long value = ((long)num < fileInfo.Length - num3) ? ((long)num) : (fileInfo.Length - num3);
				UploadPartResult uploadPartResult = ossClient_0.UploadPart(new UploadPartRequest(string_2, string_1, initiateMultipartUploadResult.UploadId)
				{
					InputStream = fileStream,
					PartSize = new long?(value),
					PartNumber = new int?(i + 1)
				});
				list.Add(uploadPartResult.PartETag);
				fileStream.Close();
			}
			CompleteMultipartUploadRequest completeMultipartUploadRequest = new CompleteMultipartUploadRequest(string_2, string_1, initiateMultipartUploadResult.UploadId);
			foreach (PartETag item in list)
			{
				completeMultipartUploadRequest.PartETags.Add(item);
			}
			ossClient_0.CompleteMultipartUpload(completeMultipartUploadRequest);
		}

		public int BigFilePartSize = 150;
	}
}
