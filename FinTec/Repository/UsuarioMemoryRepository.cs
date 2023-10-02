﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Repository
{
    public class UsuarioMemoryRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();
        public Usuario Add(Usuario oneElement)
        {
            var usuario = Find(u => u.Correo == oneElement.Correo);
            if (usuario != null)
            {
                throw new Exception("El usuario ya existe");
            }
            _usuarios.Add(oneElement);
            return oneElement;
        }

        public Usuario? Find(Func<Usuario, bool> filter)
        {
            return _usuarios.FirstOrDefault(filter);
        }
    }
}
