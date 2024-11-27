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
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <!-- Botón de editar -->
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'>
                        <img src="https://img.icons8.com/ios/50/000000/edit.png" alt="Editar" style="width: 20px; height: 20px; margin-right: 10px;" />
                    </asp:LinkButton>

                                    <!-- Botón de visualizar -->
                                    <asp:LinkButton ID="lnkView" runat="server" CommandName="View" CommandArgument='<%# Eval("Id") %>'>
                        <img src="https://img.icons8.com/ios/50/000000/visible.png" alt="Ver" style="width: 20px; height: 20px; margin-right: 10px;" />
                    </asp:LinkButton>

                                    <!-- Botón de eliminar -->
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('¿Está seguro de que desea eliminar este registro?');">
                        <img src="https://img.icons8.com/ios/50/000000/trash.png" alt="Eliminar" style="width: 20px; height: 20px;" />
                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </form>
    </body>
</asp:Content>
