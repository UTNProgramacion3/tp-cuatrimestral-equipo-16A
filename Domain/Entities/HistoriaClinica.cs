using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoriaClinica
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public string GrupoSanguineo { get; set; }
        public string Alergias { get; set; }
        public string Medicacion { get; set; }
        public string EnfermedadesCronicas { get; set; }
        public string AntecedentesFamiliares { get; set; }
        public string AntecedentesPersonales { get; set; }
        public string Habitos { get; set; }
    }
}
