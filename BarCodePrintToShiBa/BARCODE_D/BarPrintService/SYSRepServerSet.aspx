<%@ Page language="c#" Codebehind="SYSRepServerSet.aspx.cs" AutoEventWireup="false" Inherits="Sunlike.Web.DRP.SYSRepServerSet" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>SYSRepServerSet</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="style.css" type="text/css" rel="stylesheet" >
	</HEAD>
	<body>
		<form id="SYSRepServerSet" method="post" encType="multipart/form-data" runat="server">
			<table style="BACKGROUND-POSITION: 50% bottom; BACKGROUND-REPEAT: repeat-x" height="100%"
				cellSpacing="0" cellPadding="0" width="100%" background="IMAGES/install_05.jpg" border="0">
				<tr height="10">
					<td><IMG src="IMAGES/zh-cn/install_01.jpg" ></td>
					<td style="HEIGHT: 15px" width="100%" background="IMAGES/install_02.jpg"></td>
					<td style="HEIGHT: 15px"><IMG src="IMAGES/install_03.jpg"></td>
				</tr>
				<tr height="100%">
					<td style="BACKGROUND-POSITION: left bottom; BACKGROUND-REPEAT: no-repeat" align="center"
						background="IMAGES/install_04.jpg" colSpan="3">
						<table height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
						    <tr vAlign="top">
								<td height="50" vAlign="bottom" align="center"><IMG src="IMAGES/zh-cn/install_final.jpg"></td>
							</tr>
							<tr>
								<td valign="top" align="center">
									<table cellSpacing="0" cellPadding="0" width="300" border="0" height="100">
										<tr>
											<td align="center"><asp:button id="btnPre" runat="server" Text="上一步" CssClass="button"></asp:button>&nbsp;<asp:button id="btnSave" runat="server" Text="保存" CssClass="button"></asp:button></td>
										</tr>
										<tr>
											<td align="center"><asp:label id="lblMsg" ForeColor="Red" Runat="server"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
