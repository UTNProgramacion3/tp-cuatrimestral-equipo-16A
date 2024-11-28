<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm3" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Estilos de la carta */
        .profile-card {
            border-radius: 15px;
            background-color: #6096B4;
            color: white;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }

        /* Estilo de la imagen */
        .profile-img {
            border-radius: 50%;
            width: 150px;
            height: 150px;
            object-fit: cover;
            margin-bottom: 20px;
        }

        /* Botones */
        .btn-custom {
            font-weight: 700;
            color: black;
            padding: 9px 25px;
            background: #EEF7FF;
            border: none;
            border-radius: 50px;
            cursor: pointer;
            transition: all 0.3s ease 0s;
        }

            .btn-custom:hover {
                background: #BDCDD6;
                color: #EEE9DA;
                transform: scale(1.2);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container my-5">
        <div class="container my-5">
            <div class="card profile-card">
                <div class="row g-0">
                    <!-- Imagen y botón -->
                    <div class="col-md-4 text-center p-4">
                        <asp:Image ID="imgPerfil" runat="server" ImageUrl="~/ImagesFolder/default-profile.png" CssClass="profile-img" />
                        <asp:FileUpload ID="fileUpload" runat="server" accept="image/*" class="form-control mt-2" />
                        <asp:Button ID="btnSubirFoto" runat="server" Text="Editar Foto" CssClass="btn-custom mt-2" OnClick="btnSubirFoto_Click"/>
                    </div>
                    <!-- Datos de perfil -->
                    <div class="col-md-8">
                        <div class="card-body">
                            <h3 class="card-title">Mi Perfil</h3>
                            <hr />
                            <p>
                                <strong>Nombre y Apellido:</strong>
                                <asp:Label ID="lblNombreApellido" runat="server" Text="Juan Pérez"></asp:Label>
                            </p>
                            <p>
                                <strong>Email:</strong>
                                <asp:Label ID="lblEmail" runat="server" Text="juan.perez@example.com"></asp:Label>
                            </p>
                            <p>
                                <strong>Teléfono:</strong>
                                <asp:Label ID="lblTelefono" runat="server" Text="+54 11 1234 5678"></asp:Label>
                            </p>
                            <p>
                                <strong>Fecha de Nacimiento:</strong>
                                <asp:Label ID="lblFechaNacimiento" runat="server" Text="01/01/1990"></asp:Label>
                            </p>
                            <!-- Botones -->
                            <div class="mt-4 d-flex flex-wrap gap-2">
                                <asp:Button Text="Ver Mis Turnos" id="btnVerTurnos" CssClass="btn btn-custom" runat="server" OnClick="btnVerTurnos_Click" />
                                <asp:Button Text="Cambiar Contraseña" id="btnCambiarPass" CssClass="btn btn-custom" runat="server" Onclick="btnCambiarPass_Click"/>
                                <asp:Button Text="Editar" id="btnEditar" CssClass="btn-custom btn-custom" runat="server" OnClick="btnEditar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
