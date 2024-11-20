<%@ Page Title="Validar Email" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="ValidarEmail.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.ValidarEmail" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            border: 1px solid #ccc;
            border-radius: 5px;
            background: #f9f9f9;
        }
        .error {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
        <asp:Panel ID="pnlPassword" runat="server" Visible="false">
            <asp:Label ID="lblNewPassword" runat="server" Text="Nueva Contraseña:"></asp:Label>
            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Enviar" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
        </asp:Panel>
    </div>
</asp:Content>
