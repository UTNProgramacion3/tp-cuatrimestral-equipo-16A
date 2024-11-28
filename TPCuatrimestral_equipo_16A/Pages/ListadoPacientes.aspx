<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListadoPacientes.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ListadoPacientes1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Lista de Pacientes</h2>
        <div class="row mb-4">
            <div class="col-md-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="txtDocumento" class="form-label">Documento</label>
                <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="txtNroAfiliado" class="form-label">NroAfiliado</label>
                <asp:TextBox ID="txtNroAfiliado" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="ddlObraSocial" class="form-label">Obra Social</label>
                <asp:TextBox runat="server" ID="txtObraSocial" CssClass="form-control" />
            </div>
            <div class="col-md-3 d-flex align-items-end">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar Filtros" CssClass="btn btn-secondary" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <asp:GridView ID="gvPacientes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" DataKeyNames="PersonaId">
            <Columns>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
                <asp:BoundField DataField="Documento" HeaderText="Documento" SortExpression="Documento" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
                <asp:BoundField DataField="EmailPersonal" HeaderText="Email" SortExpression="EmailPersonal" />
                <asp:BoundField DataField="ObraSocial" HeaderText="Obra Social" SortExpression="ObraSocial" />
                <asp:BoundField DataField="NroAfiliado" HeaderText="Nro Afiliado" SortExpression="NroAfiliado" />

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" Text="Editar" CssClass="btn btn-warning" OnClick="btnEditar_Click" CommandArgument='<%# Eval("PersonaId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>



    </div>
</asp:Content>
