<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="MisTurnos.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.MisTurnos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-comentario {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: white;
            border: 1px solid #ccc;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
            padding: 20px;
            z-index: 1000;
            width: 50%;
        }

        .panel-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .btn-cerrar {
            background: none;
            border: none;
            font-size: 1.2em;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:GridView ID="gvTurnos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
        EmptyDataText="No se encontraron turnos." OnRowCommand="gvTurnos_RowCommand" OnRowDataBound="gvTurnos_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Turno ID" SortExpression="Id" />
            <asp:BoundField DataField="MedicoNombre" HeaderText="Médico" SortExpression="MedicoNombre" />
            <asp:BoundField DataField="PacienteNombre" HeaderText="Paciente" SortExpression="PacienteNombre" />
            <asp:BoundField DataField="SedeNombre" HeaderText="Sede" SortExpression="SedeNombre" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
            <asp:BoundField DataField="Hora" HeaderText="Hora" SortExpression="Hora" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" SortExpression="Estado" />
            <asp:TemplateField>
                <ItemTemplate>
                     <asp:Button ID="btnComentario" runat="server" CommandName="Comentar" CommandArgument='<%# Eval("Id") %>' Text="Comentario" CssClass="btn btn-primary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Panel ID="pnlComentario" runat="server" CssClass="panel-comentario" Visible="false">
        <div class="panel-header">
            <h4>Dejar Comentario</h4>
            <asp:Button ID="btnCerrarPanel" runat="server" Text="X" CssClass="btn-cerrar" OnClick="btnCerrarPanel_Click" />
        </div>
        <div class="panel-body">
            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5" Width="100%"></asp:TextBox>
            <asp:Label ID="lblErrorComentario" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnGuardarComentario" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardarComentario_Click" />
            <asp:Button ID="btnCancelarComentario" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCerrarPanel_Click" />
        </div>
    </asp:Panel>
</asp:Content>
