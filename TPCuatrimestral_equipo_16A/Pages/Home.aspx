<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.Home" Debug=true Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="/Stylesheets/Home.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />

    </p>

    <section>
        <div class="section">
            <div class="custom-container">
                <div class="row">
                    <asp:Repeater ID="rptTarjetas" runat="server">
                        <ItemTemplate>
                            <div class="col-md-4 mt-5">
                                <div class="card card-style card-style-movement">
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                        <p class="card-text"><%# Eval("Descripcion") %></p>
                                        <a href='<%# Eval("Url") %>' class="btn btn-secondary button-style">+</a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </section>

    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Nuevo usuario" />
</asp:Content>
