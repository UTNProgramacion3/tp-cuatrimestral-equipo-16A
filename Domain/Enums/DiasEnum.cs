using System.ComponentModel;

namespace Domain
{
    public enum DiasEnum
    {
        [Description("Domingo")]
        Domingo = 1,

        [Description("Lunes")]
        Lunes = 2,

        [Description("Martes")]
        Martes = 3,

        [Description("Miércoles")]
        Miercoles = 4,

        [Description("Jueves")]
        Jueves = 5,

        [Description("Viernes")]
        Viernes = 6,

        [Description("Sábado")]
        Sabado = 7,
    }
}
