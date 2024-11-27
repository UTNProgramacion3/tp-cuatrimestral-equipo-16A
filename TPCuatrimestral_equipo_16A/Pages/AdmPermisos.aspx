<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="AdmPermisos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.AdmPermisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* General card styling */
        .card {
            border: 1px solid #ddd;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .card-header {
            background-color: #f8f9fa;
            border-bottom: 1px solid #ddd;
            padding: 15px;
            font-size: 1.25em;
            font-weight: bold;
            text-align: center;
        }
        .btn-toggle-permisos {
         font-size: 0.6em; /* Más pequeño */
        background-color: #f8f8f0; /* Blanco hueso */
        color: #333; /* Texto negro */
        border: 1px solid #ccc; /* Borde sutil */
        padding: 4px 8px; /* Menos espaciado */
        border-radius: 20px; /* Bordes más redondeados */
        text-transform: none; /* Sin mayúsculas */
        transition: background-color 0.3s ease; /* Transición suave */
                             }

    .btn-toggle-permisos:hover {
        background-color: #e2e2d9; /* Color ligeramente más oscuro al pasar el mouse */
    }

        .card-body {
            padding: 20px;
        }

        .list-group-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;
            padding: 10px 0;
            border-bottom: 1px solid #ddd;
        }

            .list-group-item:last-child {
                border-bottom: none;
            }

            /* Label and input styles */
            .list-group-item .me-2 {
                margin-right: 10px;
                font-size: 1em;
            }

        .form-control {
            width: 60%;
        }

        /* Button styling */
        .btn-sm {
            font-size: 0.9em;
        }

        /* Margin for permission panel */
        .mt-3 {
            margin-top: 20px;
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
                            <h4>
                                <%# Eval("Nombre") %>
                               <asp:Button ID="btnTogglePermisos" runat="server" 
                                Text="Mostrar Permisos" 
                                CssClass= "btn-toggle-permisos"
                                OnClick="btnTogglePermisos_Click" 
                                CommandArgument='<%# Eval("Id") %>' />
                            </h4>
                        </div>
                        <div class="card-body">
                            <asp:Panel ID="pnlPermisos" runat="server" Visible="false">
                            <asp:Repeater ID="rptPermisos" runat="server" OnItemCommand="rptPermisos_ItemCommand">
                                <ItemTemplate>
                                    <div class="list-group-item">
                                        <asp:Label ID="lblPermiso" runat="server" Text='<%# Eval("Nombre") %>' CssClass="me-2"></asp:Label>
                                        <asp:TextBox ID="txtPermiso" runat="server" CssClass="form-control me-2" Visible="false"></asp:TextBox>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-sm btn-primary" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-sm btn-secondary" CommandName="Cancelar" CommandArgument='<%# Eval("Id") %>' Visible="false" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Panel ID="pnlAgregarPermiso" runat="server" Visible="false" CssClass="mt-3">
                                <asp:Label ID="lblError" runat="server" Text="" CssClass="text-danger"></asp:Label>
                                <asp:TextBox ID="txtNuevoPermiso" runat="server" CssClass="form-control" Placeholder="Nombre del permiso"></asp:TextBox>
                                <div class="mt-2">
                                    <asp:Button ID="btnGuardarPermiso" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarPermiso_Click" />
                                    <asp:Button ID="btnCancelarPermiso" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelarPermiso_Click" />
                                </div>
                            </asp:Panel>
                            <asp:Button ID="btnAgregarPermiso" runat="server" Text="Agregar Permiso" CssClass="btn btn-primary mt-3" OnClick="btnAgregarPermiso_Click" CommandArgument='<%# Eval("Id") %>' />

                                 </asp:Panel>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="container text-center mt-4">
        <asp:Button Text="Administrar Permisos de Roles" ID="btnPermisosPorRol" runat="server" OnClick="btnPermisosPorRol_Click" CssClass="btn btn-primary" />
    </div>
</asp:Content>

