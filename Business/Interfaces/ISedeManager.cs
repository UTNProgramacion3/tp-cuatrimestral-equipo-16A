﻿using Business.Dtos;
using Business.Managers;
using Domain.Entities;
using Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISedeManager
    {
        Response<List<SedeDto>> ObtenerTodos();

        Response<SedeDto> Crear(SedeDto entity);

        Sede Update(Sede entity);

        Response<Sede> ObeterSedeById(int id);
    }
}
