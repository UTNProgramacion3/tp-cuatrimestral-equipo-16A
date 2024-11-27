<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="RolesPermisos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.RolesPermisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

    <asp:Repeater ID="rptRoles" runat="server" OnItemCommand="rptRoles_ItemCommand" OnItemDataBound="rptRoles_ItemDataBound">
        <ItemTemplate>
            <div class="card mb-3">
                <div class="card-header">
                    <h5><%# Eval("Nombre") %></h5>
                    <asp:HiddenField ID="hfRolId" runat="server" Value='<%# Eval("Id") %>' />
                </div>
                <div class="card-body">
                    <!-- Lista de permisos -->
                    <ul class="list-group">
                        <asp:Repeater ID="rptPermisos" runat="server" DataSource='<%# Eval("Permisos") %>'>
                            <ItemTemplate>
                                <li class="list-group-item d-flex justify-content-between align-items-center">
                                    <%# Eval("Nombre") %>
                                    <asp:HiddenField ID="hfPermisoId" runat="server" Value='<%# Eval("Id") %>' />
                                    <asp:Button ID="btnEliminarPermiso" runat="server" Text="X" CssClass="btn btn-danger btn-sm"
                                        CommandName="EliminarPermiso"
                                        OnClick="btnEliminarPermiso_Click"
                                        />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>

                    <!-- Dropdown para agregar permisos -->
                    <div class="mt-3">
                        <asp:DropDownList ID="ddlPermisosDisponibles" runat="server" CssClass="form-control"></asp:DropDownList>
                        <asp:Button ID="btnAgregarPermiso" runat="server" Text="Agregar Permiso" CssClass="btn btn-success btn-sm mt-2"
                            CommandName="AgregarPermiso" CommandArgument='<%# Eval("Id") %>' />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
