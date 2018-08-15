<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemMovie_Upload_Excel.aspx.cs" Inherits="OnKeeng_ItemMovie_Upload_Excel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../common/jquery-1.7.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <p>
        <a  id="id-filetem" href="javascript:;">Download file mẫu</a>
    </p>
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Has Header?" />
    <asp:RadioButtonList ID="rbHDR" runat="server">
        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
    </asp:RadioButtonList>
    <%if (1 < 0)
      {%>
    <p style='color: Red; font-weight: bold; font-size: large'>
        Thông tin dữ liệu sai</p>
    
    <%}
      else
      { %>
    <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="PageIndexChanging"
        AllowPaging="true">
    </asp:GridView>
    <asp:Label ID="lbError" runat="server" ForeColor="Red"></asp:Label>
    <%} %>
    </form>

    <script type="text/javascript">
        //$('#id-filetem').attr('href', '../doc/itemMovieGroup.xlsx');
        $('#id-filetem').click(function () {
            window.location = "../doc/ItemMovie.xlsx";
        });
    </script>
</body>
</html>
