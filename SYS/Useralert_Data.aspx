<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Useralert_Data.aspx.cs" Inherits="SYS_Useralert_Data" %>
<%@ Register TagPrefix="rade" Namespace="Telerik.WebControls" Assembly="RadEditor" %>
<HTML>
	<HEAD>
		<title></title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		 <LINK href="../Common/style.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../common/common.js"></script>
	</HEAD>
	<body bgcolor="#FFFFFF" topmargin="0" leftmargin="0">
	<div class="formWrapper">
		<form runat="server" ID="Form1">
			<table border="0" cellpadding="3" cellspacing="3" style="BORDER-COLLAPSE: collapse" bordercolor="#111111"
				width="100%">
				<TBODY>
					<tr>
						<td width="100%" bgcolor="#D2D7DA"><font size="3" color="#000000"><b>
									<asp:Label id="Label1" runat="server" Font-Bold="True" Font-Size="10pt"> THÔNG BÁO</asp:Label></b></font></td>
					</tr>
					<tr>
						<td width="100%">
							<TABLE id="AutoNumber2" style="BORDER-COLLAPSE: collapse" borderColor="#111111" cellSpacing="0"
								cellPadding="3" width="100%" border="0">
								<TBODY>
									<TR>
										<TD noWrap>
                                            UserName:</TD>
										<TD width="100%">
											<asp:TextBox id="txtUserName" runat="server" Width="304px"></asp:TextBox></TD>
									</TR>
									<TR>
										<TD noWrap>Mô tả:
										</TD>
										<TD width="100%">
                                            <rade:radeditor id="txtContent" runat="server" allowscripts="True" allowthumbgeneration="true"
                                                cachelocalization="True" deletedocumentspaths="~/archive/images/" deleteflashpaths="~/archive/images/"
                                                deleteimagespaths="~/archive/images/" deletemediapaths="~/archive/images/" deletetemplatepaths="~/archive/images/"
                                                documentspaths="~/archive/images/" editable="True" flashpaths="~/archive/images/"
                                                haspermission="True" height="372px" imagespaths="~/archive/images/" maxmediasize="524288000"
                                                mediafilters="*.asf,*.asx,*.wm,*.wmx,*.wmp,*.wma,*.wax,*.wmv,*.wvx,*.avi,*.wav,*.mpeg,*.mpg,*.mpe,*.mov,*.m1v,*.mp2,*.mpv2,*.mp2v,*.mpa,*.flv,*.mp3,*.m3u,*.mid,*.midi,*.rm,*.rma,*.rmi,*.rmv,*.aif,*.aifc,*.aiff,*.au,*.snd"
                                                mediapaths="~/archive/images/" showsubmitcancelbuttons="False" templatepaths="~/archive/images/"
                                                thumbappendix="thumb" toolsfile="~/common/RadControls/Editor/ToolsFile.xml" toolsheight="230px"
                                                uploaddocumentspaths="~/archive/images/" uploadflashpaths="~/archive/images/"
                                                uploadimagespaths="~/archive/images/" uploadmediapaths="~/archive/images/" uploadtemplatepaths="~/archive/images/"
                                                width="784px">&nbsp;</rade:radeditor>
                                        </TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2"></TD>
									</TR>
									<TR>
										<TD noWrap colSpan="2">
											<asp:Button id="cmdSave" runat="server" CssClass="formSubmitBtn" Text="Lưu" onclick="cmdSave_Click"></asp:Button></TD>
									</TR>
		</form>

		</div>
	</body>
</HTML>
