<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Main.Master" AutoEventWireup="true" CodeBehind="JornadaLaboralEmpleado.aspx.cs" Inherits="TPCuatrimestral_equipo_16A.Pages.JornadaLaboralEmpleado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validateTime(input) {
            var time = input.value.split(':');
            if (time[1] !== "00") {
                input.value = time[0] + ":00";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body>
    <form id="form1">
        <div class="container">
            <h1>Asignar Jornada Laboral</h1>
            <h3>Seleccione las horas de trabajo para cada día de la semana</h3>

            <div class="form-group">
                <label for="lunes">Lunes:</label>
                <input type="time" id="lunesInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="lunesFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="martes">Martes:</label>
                <input type="time" id="martesInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="martesFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="miercoles">Miércoles:</label>
                <input type="time" id="miercolesInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="miercolesFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="jueves">Jueves:</label>
                <input type="time" id="juevesInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="juevesFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="viernes">Viernes:</label>
                <input type="time" id="viernesInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="viernesFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="sabado">Sábado:</label>
                <input type="time" id="sabadoInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="sabadoFin" runat="server" onchange="validateTime(this)" />
            </div>
            <div class="form-group">
                <label for="domingo">Domingo:</label>
                <input type="time" id="domingoInicio" runat="server" onchange="validateTime(this)" /> - 
                <input type="time" id="domingoFin" runat="server" onchange="validateTime(this)" />
            </div>

            <div class="form-group">
                <button type="submit" runat="server" onserverclick="btnGuardar_Click" class="btn btn-primary">Guardar Jornada</button>
            </div>
        </div>
    </form>
</body>
</asp:Content>
