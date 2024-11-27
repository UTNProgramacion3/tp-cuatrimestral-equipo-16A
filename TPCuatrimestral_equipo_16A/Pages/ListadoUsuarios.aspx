<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ListadoUsuarios.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ListadoUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <body>
        <form id="listadoEmpleadosForm">
            <div class="container">
                <h1>Lista de Usuarios</h1>
                <h3>Filtrar usuarios por rol</h3>

                <div class="form-group">
                    <label for="ddlRoles">Seleccione un Rol:</label>
                    <asp:DropDownList ID="ddlRoles" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged" class="form-control">
                        <asp:ListItem Text="Todos los Roles" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group">
                    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="false" class="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre completo" SortExpression="Nombre" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="Rol" HeaderText="Rol" SortExpression="Rol" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </body>
</asp:Content>
