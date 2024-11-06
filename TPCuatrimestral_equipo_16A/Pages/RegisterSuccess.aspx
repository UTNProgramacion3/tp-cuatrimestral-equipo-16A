<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="RegisterSuccess.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.RegisterSuccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bg-success {
            background-color: #28a745 !important; 
        }

        .text-white {
            color: #ffffff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="card text-white bg-success mb-3" style="max-width: 100%;">
            <div class="card-body">
                <h5 class="card-title">Usuario Creado Exitosamente</h5>
                <p class="card-text">
                    <asp:Label ID="lblSuccessMessage" runat="server" CssClass="text-white"></asp:Label>
                </p>
            </div>
        </div>
    </div>
</asp:Content>
