<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="SeleccionarFechaHora.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.SeleccionarFechaHora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="conteiner m-3">
    <div class="row g-4 d-flex justify-content-center">
        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label fw-bold" runat="server">Seleccionar Fecha de Turno</label>
                <asp:TextBox CssClass="form-control" TextMode="Date" runat="server" ID="txtBoxFechaTurno" />
            </div>
            <div>
               <asp:GridView ID="dgvFechaHorario" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-hover table-striped table-bordered">
                    <Columns>
                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                        <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</div>




</asp:Content>
