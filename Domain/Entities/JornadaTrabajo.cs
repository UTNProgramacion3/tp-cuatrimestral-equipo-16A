using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JornadaTrabajo
    {
        public int Id { get; set; } 
        public Sede Sede { get; set; }
        public List<DiaLaboral> Jornada { get; set; }
        public int SedeId { get; set; }

        public JornadaTrabajo()
        {
            SedeId = SedeEnum.Central.GetHashCode();
            Jornada = GenerarSemanaLaboralBase();
        }

        private List<DiaLaboral> GenerarSemanaLaboralBase()
        {
            List<int> diasSemana = new List<int>()
            {
                (int)DiasEnum.Lunes,
                (int)DiasEnum.Martes,
                (int)DiasEnum.Miercoles,
                (int)DiasEnum.Jueves,
                (int)DiasEnum.Viernes,
                (int)DiasEnum.Sabado
            };
            List <DiaLaboral>  jornadasSemana = new List<DiaLaboral>();
            diasSemana.ForEach(dia =>
            {
                jornadasSemana.Add(new DiaLaboral()
                {
                    Dia = dia,
                    Inicio = new TimeSpan(8, 0, 0),
                    Fin = new TimeSpan(dia == (int)DiasEnum.Sabado ? 
                    12 : 17, 0, 0)
                });
            });
            return jornadasSemana;
        }
    }
}
