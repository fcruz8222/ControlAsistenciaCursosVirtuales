<%@ Page Title="Cursos" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="ControlAsistenciaCursosVirtuales.Pages.Cursos.Cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="card">
    <h2>Listado de Cursos</h2>
    <asp:GridView ID="gvCursos" runat="server" AutoGenerateColumns="false" CssClass="grid">
      <Columns>
        <asp:BoundField DataField="Codigo" HeaderText="Código" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Instructor" HeaderText="Instructor" />
        <asp:BoundField DataField="Activo" HeaderText="Activo" />
      </Columns>
    </asp:GridView>
  </div>
</asp:Content>
