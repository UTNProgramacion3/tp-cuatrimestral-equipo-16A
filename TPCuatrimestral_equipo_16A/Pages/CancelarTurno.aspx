<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="CancelarTurno.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.CancelarTurno" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="/Stylesheets/Home.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <section class="d-flex justify-content-center align-items-center vh-50">
    <div class="section">
            <div class="row justify-content-center">
                <div class="col">
                    <div class="card card-style card-style-movement">
                        <div class="card-body text-center">
                            <h5 class="card-title">Cancelar Turno</h5>
                            <p class="card-text">¿Está seguro de que desea cancelar un turno?</p>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:Button ID="btnCancelar" CssClass="btn btn-secondary button-style" Text="Cancelar" runat="server" OnClick="btnCancelar_Click" />
                                <asp:Button ID="btnAceptar" CssClass="btn btn-secondary button-style" Text="Aceptar" runat="server" OnClick="btnAceptar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</section>

</asp:Content>
