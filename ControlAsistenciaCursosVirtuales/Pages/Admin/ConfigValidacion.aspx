<%@ Page Title="Validación IP/Ubicación" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="ConfigValidacion.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Admin.ConfigValidacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card">
    <h2>Configuración de Validación</h2>
    <p>Validaremos por IP exacta. primera etapa.</p>

    <div class="field">
      <label>IP permitida</label>
      <asp:TextBox ID="txtIpPermitida" runat="server" />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtIpPermitida"
        ErrorMessage="Ingrese una IP" CssClass="error" Display="Dynamic" />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn" OnClick="btnGuardar_Click" />
    <br /><br />
    <asp:Label ID="lblMsg" runat="server" />
  </div>
</asp:Content>
