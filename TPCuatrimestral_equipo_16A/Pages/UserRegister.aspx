<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.UserRegister" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-custom {
            background-color: #BDCDD6;
            border-color: #93BFCF;
        }

        .card-header-custom {
            background-color: #93BFCF;
            color: white;
        }

        .card-body-custom {
            background-color: #EEE9DA;
        }

        .btn-custom {
            background-color: #93BFCF;
            border-color: #93BFCF;
            color: white;
            font-weight: bold;
        }

            .btn-custom:hover {
                background-color: #BDCDD6;
                border-color: #93BFCF;
            }

        .message-container {
            display: block;
            padding: 15px;
            background-color: #93BFCF;
            color: #333;
            font-weight: bold;
            border-radius: 5px;
        }
    </style>
    <script type="text/javascript">
        function verificarCampos() {
            // Obtener los elementos de los campos
            var email = document.getElementById("<%= txtEmail.ClientID %>");
            var password = document.getElementById("<%= txtPassword.ClientID %>");
            var nombre = document.getElementById("<%= txtNombre.ClientID %>");
            var apellido = document.getElementById("<%= txtApellido.ClientID %>");
            var dni = document.getElementById("<%= txtDni.ClientID %>");
            var direccion = document.getElementById("<%= txtDireccion.ClientID %>");
            var btnRegister = document.getElementById("<%= btnRegister.ClientID %>");

            // Verifica si todos los campos están llenos
            if (email.value.trim() !== "" && password.value.trim() !== "" &&
                nombre.value.trim() !== "" && apellido.value.trim() !== "" &&
                dni.value.trim() !== "" && direccion.value.trim() !== "") {
                // Habilitar el botón de registrar si todos los campos están llenos
                btnRegister.disabled = false;
            } else {
                // Deshabilitar el botón de registrar si falta algún campo
                btnRegister.disabled = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container mt-5">
                <div class="row justify-content-center">
                    <div class="col-md-8">
                        <div class="card">
                            <div class="card-header text-center bg-primary text-white">
                                <h3>Registro de Usuario</h3>
                            </div>
                            <div class="card-body">

                                <asp:Panel runat="server" ID="pnlUserCreate" CssClass="mb-3">
                                    <div class="form-group">
                                        <asp:Label Text="Ingresa tu Email!" ID="lblEmail" runat="server" CssClass="font-weight-bold" AutoPostBack="true" />
                                        <asp:TextBox runat="server" type="email" ID="txtEmail" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label Text="Crea tu contraseña!" ID="lblPassword" runat="server" CssClass="font-weight-bold" />
                                        <asp:TextBox runat="server" type="password" ID="txtPassword" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary mt-3" Text="Siguiente" OnClick="btnNext_Click" />
                                </asp:Panel>

                                <asp:Panel runat="server" ID="pnlPersonaCreate" CssClass="mb-3" Visible="false">
                                    <div class="form-group">
                                        <asp:Label Text="Ingresa tu Nombre!" ID="lblNombre" runat="server" CssClass="font-weight-bold" />
                                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label Text="Ingresa tu Apellido!" ID="Label2" runat="server" CssClass="font-weight-bold" />
                                        <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label Text="Ingresa tu DNI!" runat="server" ID="lblDni" CssClass="font-weight-bold" />
                                        <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label Text="Ingresa tu Dirección Completa!" runat="server" ID="lblDireccion" CssClass="font-weight-bold" />
                                        <asp:TextBox runat="server" ID="txtDireccion" CssClass="form-control" onkeyup="verificarCampos()" />
                                    </div>
                                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-success mt-3" Text="Registrar" Enabled="false" OnClick="btnRegister_Click" />
                                </asp:Panel>

                                    <asp:Label ID="lblMensaje" runat="server" CssClass="message-container" Visible="false" Text="" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
