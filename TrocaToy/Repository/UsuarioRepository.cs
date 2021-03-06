﻿using Infrastructure;
using Infrastructure.Security;
using Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TrocaToy.Models;

namespace TrocaToy.Repository
{
    /// <summary>
    /// Repositorio crud Usuario
    /// </summary>
    public class UsuarioRepository : Infrastructure.Repository<Models.Usuario>, IUsuarioRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UsuarioRepository(DbContext context) : base(context)
        {

        }

    }
}
