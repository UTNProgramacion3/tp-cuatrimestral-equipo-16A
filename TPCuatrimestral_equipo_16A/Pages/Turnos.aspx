<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Turnos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Views.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container d-flex justify-content-center">
    <div class="row g-2">
        <div class="col-md-4" >
        <table class="table table-stripped" runat="server" id="tblTurnos">
          <thead>
              <tr class="m-2">
                  <th>Encabezado</th>
                  <th>Encabezado</th>
                  <th>Encabezado</th>
              </tr>
          </thead>
        </table>
        </div>
    </div>
    </div>

</asp:Content>
