<%@ Page language="c#" Codebehind="SYSUserSet.aspx.cs" AutoEventWireup="false" Inherits="Sunlike.Web.DRP.SYSUserSet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SYSUserSet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="SYSUserSet" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%" style="BACKGROUND-POSITION:50% bottom; BACKGROUND-REPEAT:repeat-x"
				background="IMAGES/install_05.jpg">
				<tr height="10">
					<td><img src="IMAGES/zh-cn/install_01.jpg"></td>
					<td background="IMAGES/install_02.jpg" width="100%" style="HEIGHT: 15px"></td>
					<td style="HEIGHT: 15px"><img src="IMAGES/install_03.jpg"></td>
				</tr>
				<tr vAlign="top" height="100%">
					<td align="center" colspan="3" style="BACKGROUND-POSITION: left bottom; BACKGROUND-REPEAT: no-repeat"
						background="IMAGES/install_04.jpg">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0" height="100%">
							<tr valign="top">
								<td height="50" align="center" valign="bottom"><img src="IMAGES/zh-cn/install_user.gif"></td>
							</tr>
							<tr valign="top">
								<td align="center"><br>
									<table cellSpacing="0" cellPadding="0" width="180" border="0">
										<tr>
											<td align="left" style="height: 38px"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblUser" runat="server">用户</asp:label>:</td>
											<td align="left" style="height: 38px"><asp:textbox id="txtUser" runat="server" Width="130px"></asp:textbox></td>
										</tr>
										<tr>
											<td align="left" height="30"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblPswd" runat="server">密码</asp:label>:</td>
											<td><asp:textbox id="txtPassword" runat="server" TextMode="Password" Width="130px"></asp:textbox>
												<asp:TextBox id="txtHide" runat="server" style="DISPLAY:none"></asp:TextBox></td>
										</tr>
										<tr>
											<td align="left" style="height: 38px"><IMG alt="" src="IMAGES/Icon2.gif">&nbsp;<asp:label id="lblDomain" runat="server">域名</asp:label>:</td>
											<td style="height: 38px"><asp:textbox id="txtDomain" runat="server" Width="130px"></asp:textbox></td>
										</tr>
									</table>
									<P>
										<BR>
										<asp:Button id="btnSetValue" runat="server" Text="模拟用户测试" CssClass="button" Width="118px"></asp:Button></P>
									<P>
										<asp:Label id="lblMsg" runat="server" ForeColor="red"></asp:Label></P>
									<P>
										<asp:Button id="btnNext" runat="server" Text="下一步" CssClass="button"></asp:Button></P>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
