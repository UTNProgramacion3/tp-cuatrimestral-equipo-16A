<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListadoMedicos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ListadoMedicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Lista de Médicos</h2>

        <!-- Filtros -->
        <div class="row mb-4">
            <div class="col-md-3">
                <label for="txtNombre" class="form-label">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="txtApellido" class="form-label">Apellido:</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="txtMatricula" class="form-label">Matrícula:</label>
                <asp:TextBox ID="txtMatricula" runat="server" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <label for="ddlEspecialidad" class="form-label">Especialidad:</label>
                <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="true" CssClass="form-control">
                    <asp:ListItem Text="Seleccionar" Value="" />
                    <asp:ListItem Text="Todas las especialidades" Value="0" />
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-3">
            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros" CssClass="btn btn-secondary" OnClick="btnLimpiarFiltros_Click" />
        </div>

        <!-- Botón de Filtro -->
        <div class="row mb-4">
            <div class="col-md-12 text-end">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" />
            </div>
        </div>


        <asp:GridView ID="gvMedicos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="table table-bordered table-striped" DataKeyNames="Matricula"
            OnRowEditing="gvMedicos_RowEditing" OnRowUpdating="gvMedicos_RowUpdating" OnRowCancelingEdit="gvMedicos_RowCancelingEdit" OnRowDataBound="gvMedicos_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Matrícula">
                    <ItemTemplate>
                        <asp:Label ID="lblMatricula" runat="server" Text='<%# Eval("Matricula") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblMatriculaEdit" runat="server" Text='<%# Eval("Matricula") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("PersonaNombre") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblNombreEdit" runat="server" Text='<%# Eval("PersonaNombre") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Apellido">
                    <ItemTemplate>
                        <asp:Label ID="lblApellido" runat="server" Text='<%# Eval("PersonaApellido") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="lblApellidoEdit" runat="server" Text='<%# Eval("PersonaApellido") %>'/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Especialidad">
                    <ItemTemplate>
                        <asp:Label ID="lblEspecialidad" runat="server" Text='<%# Eval("EspecialidadNombre") %>' />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlEspecialidad" runat="server" CssClass="form-control" DataTextField="Nombre" DataValueField="Id" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:CommandField ShowEditButton="True" />
            </Columns>
        </asp:GridView>
</asp:Content>
