using System.ComponentModel;

namespace Domain
{
    public enum DiasEnum
    {
        [Description("Lunes")]
        Lunes = 1,

        [Description("Martes")]
        Martes = 2,

        [Description("Miércoles")]
        Miercoles = 3,

        [Description("Jueves")]
        Jueves = 4,

        [Description("Viernes")]
        Viernes = 5,

        [Description("Sábado")]
        Sabado = 6,

        [Description("Domingo")]
        Domingo = 7,
    }
}
