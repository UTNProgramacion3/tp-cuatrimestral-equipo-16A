﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Paciente : Persona
    {
        public int Id { get; set; }
        public string ObraSocial { get; set; }
        public string NroAfiliado { get; set; }
        public List<Turno> Turnos { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }
        public int PersonaId { get; set; }

    }
}
