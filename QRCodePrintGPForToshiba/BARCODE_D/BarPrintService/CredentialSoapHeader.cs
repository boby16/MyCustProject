using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace BarPrintService
{
    public class CredentialSoapHeader : System.Web.Services.Protocols.SoapHeader
    {
        private string _userName;
        private string _userPassword;

        public CredentialSoapHeader()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string UserPassword
        {
            get { return _userPassword; }
            set { _userPassword = value; }
        }
    }
}
