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
                <asp:TextBox ID="tarihilk" runat="server" /> 
                <asp:TextBox ID="tarihson" runat="server" />
            </div>

            <div>
                <asp:TextBox ID="txtMalArama" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="btnAra" runat="server" Text="Ara" OnClick="btnAra_Click" />
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
