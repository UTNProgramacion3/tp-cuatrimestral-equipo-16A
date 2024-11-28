<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ConfirmarTurno.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ConfirmarTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="conteiner m-3">
    <div class="row g-4 d-flex justify-content-center">
        <div class="col-md-4">
            <div Id="cardHeader" runat="server" class="m4 d-flex justify-content-center">
                <h2>Confirmar Turno</h2>
            </div>
            <div>
                <div class="card">
                    <div class="card-header">
                        <h4>Datos de Turno</h4>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Nombre Paciente: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblNombrePaciente" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Apellido Paciente: " runat="server" />
                             <asp:Label Text="" runat="server" ID="lblApellidoPaciente" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Nombre Medico: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblNombreMedico" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Apellido Medico: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblApellidoMedico" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Especialidad: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblEspecialidad" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Sede: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblNombreSede" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Direccion: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblDireccionSede" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-bold="true" Text="Fecha: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblFecha" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Font-Bold="true" Text="Hora: " runat="server" />
                            <asp:Label Text="" runat="server" ID="lblHora" />
                        </div>
                        <div class="mb-3">
                            <label for="txtbObservaciones" class="form-label">Observaciones</label>
                            <textarea class="form-control" runat="server"  id="txtbObservaciones" rows="3"></textarea>
                        </div>
                        <div class="mb-3">
                            <asp:Button class="btn btn-dark" Type="button" ID="btnAtras" OnClick="btnAtras_Click" Text="Atrás" runat="server" />
                            <asp:Button class="btn btn-dark" Type="button" ID="btnCancelar" OnClick="btnCancelar_Click" Text="Cancelar" runat="server" />
                            <asp:Button class="btn btn-dark" Type="button" ID="btnConfirmar" OnClick="btnConfirmar_Click" Text="Confirmar" runat="server" />
                            <div>
                                <div id="turnoSuccess" class="alert alert-success mt-3" role="alert" runat="server" visible="false">
                                    ¡Turno Confirmado!, volviendo al menu principal...
                                </div>
                                <div id="turnoCancelado" class="alert alert-danger mt-3" role="alert" runat="server" visible="false">
                                    ¡Turno Cancelado!, volviendo al menu principal...
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
    </div>





</asp:Content>
