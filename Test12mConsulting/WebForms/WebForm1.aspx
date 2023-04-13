<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Test12mConsulting.Scripts.WebForms.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <label>Aramak için Tarih Girilecek <br /> <br /> Başlangıç Tarihi:&nbsp;&nbsp;&nbsp; </label>&nbsp;<asp:TextBox ID="tarihilk" runat="server" Text="42000" /> 
                <label>&nbsp;&nbsp;&nbsp; Son Tarih:&nbsp;&nbsp;&nbsp; </label>
                &nbsp;<asp:TextBox ID="tarihson" runat="server" Text="42789" />
                <asp:Button ID="btn" runat="server" Text="Ara" OnClick="btn_click" Width="92px" style="margin-left: 46px" />
                <br />
            </div>
            <br />
            <div>
                <label>Ürün Adı Giriniz:&nbsp;&nbsp;&nbsp; </label>
&nbsp;<asp:TextBox ID="txtMalArama" runat="server" Text="10086 SİEMENS" Height="26px" Width="157px"></asp:TextBox>
                <asp:Button ID="btnAra" runat="server" Text="Ara" OnClick="btnAra_Click" Width="92px" style="margin-left: 48px" />
                <br />
            </div>
            <div>
                <br />
                <label>Sonuç:</label>
                <br />
                <asp:GridView ID="gridView" runat="server"></asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
