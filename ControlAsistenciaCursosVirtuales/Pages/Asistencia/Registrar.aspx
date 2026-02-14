<%@ Page Title="Registrar Asistencia" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Registrar.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Asistencia.Registrar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card">
    <h2>Registrar Asistencia</h2>

    <div class="field">
      <label>Curso</label>
      <asp:DropDownList ID="ddlCurso" runat="server" />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCurso"
        InitialValue="" ErrorMessage="Seleccione un curso" CssClass="error" Display="Dynamic" />
    </div>

    <div class="field">
      <label>Nombre del estudiante</label>
      <asp:TextBox ID="txtNombre" runat="server" />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
        ErrorMessage="Nombre requerido" CssClass="error" Display="Dynamic" />
    </div>

    <div class="field">
      <label>Carnet / ID</label>
      <asp:TextBox ID="txtCarnet" runat="server" />
      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCarnet"
        ErrorMessage="Carnet requerido" CssClass="error" Display="Dynamic" />
    </div>

    <div class="field">
      <label>IP detectada</label>
      <asp:TextBox ID="txtIp" runat="server" ReadOnly="true" />
    </div>

    <asp:CustomValidator ID="cvIp" runat="server"
      OnServerValidate="cvIp_ServerValidate"
      ErrorMessage="IP no permitida para registrar asistencia"
      CssClass="error" Display="Dynamic" />

    <br />
    <asp:Label ID="lblOk" runat="server" />
    <br /><br />

    <asp:Button ID="btnGuardar" runat="server" Text="Registrar" CssClass="btn" OnClick="btnGuardar_Click" />
  </div>
</asp:Content>
