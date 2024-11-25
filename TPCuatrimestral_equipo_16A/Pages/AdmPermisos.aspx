<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="AdmPermisos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.AdmPermisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card {
            margin-bottom: 20px;
        }

        .card-header {
            background-color: #f8f9fa;
            padding: 10px;
            text-align: center;
        }

        .card-body {
            padding: 10px;
        }

        .list-group-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .btn-sm {
            font-size: 0.8em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server" ID="UpdatePanelModulos">
        <ContentTemplate>

            <asp:Repeater ID="rptModulos" runat="server" OnItemDataBound="rptModulos_ItemDataBound">
                <ItemTemplate>
                    <div class="card">
                        <div class="card-header">
                            <h4><%# Eval("Nombre") %></h4>
                        </div>
                        <div class="card-body">
                            <!-- Repeater para los permisos de cada módulo -->
                            <asp:Repeater ID="rptPermisos" runat="server">
                                <ItemTemplate>
                                    <div class="list-group-item">
                                        <span><%# Eval("Nombre") %></span>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <!-- Sección para agregar un nuevo permiso -->
                            <asp:Panel ID="pnlAgregarPermiso" runat="server" Visible="false" CssClass="mt-3">
                                <asp:TextBox ID="txtNuevoPermiso" runat="server" CssClass="form-control" Placeholder="Nombre del permiso"></asp:TextBox>
                                <div class="mt-2">
                                    <asp:Button ID="btnGuardarPermiso" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarPermiso_Click" />
                                    <asp:Button ID="btnCancelarPermiso" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarPermiso_Click" />
                                </div>
                            </asp:Panel>

                            <asp:Button ID="btnAgregarPermiso" runat="server" Text="Agregar Permiso" CssClass="btn btn-primary mt-3" OnClick="btnAgregarPermiso_Click" CommandArgument='<%# Eval("Id") %>' />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container text-center mt-4">
        <asp:Button Text="Administrar Permisos de Roles" ID="btnPermisosPorRol" runat="server" OnClick="btnPermisosPorRol_Click" />
    </div>
</asp:Content>
