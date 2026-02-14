<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Auth.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card">
    <h2>Iniciar sesión</h2>

    <div class="field">
      <label>Usuario</label>
      <asp:TextBox ID="txtUser" runat="server" />
      <asp:RequiredFieldValidator ID="rfvUser" runat="server"
        ControlToValidate="txtUser" ErrorMessage="Usuario requerido" CssClass="error" Display="Dynamic" />
    </div>

    <div class="field">
      <label>Clave</label>
      <asp:TextBox ID="txtPass" runat="server" TextMode="Password" />
      <asp:RequiredFieldValidator ID="rfvPass" runat="server"
        ControlToValidate="txtPass" ErrorMessage="Clave requerida" CssClass="error" Display="Dynamic" />
    </div>

    <asp:Label ID="lblMsg" runat="server" CssClass="error" />
    <br />

    <asp:Button ID="btnLogin" runat="server" Text="Entrar" CssClass="btn" OnClick="btnLogin_Click" />
  </div>
</asp:Content>
