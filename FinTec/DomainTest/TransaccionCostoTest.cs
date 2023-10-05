﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTest
{
    [TestClass]
    public class TransaccionCostoTest
    {
        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Tipo_Categoria_Distinto_TransaccionC()
        {
            var transaccion = new TransaccionCosto();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Ingreso;
            categoria.EstadoActivo = true;
            transaccion.CategoriaTransaccion = categoria;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainTransaccionException))]
        public void Categoria_Inactiva_TransaccionC()
        {
            var transaccion = new TransaccionCosto();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = false;
            transaccion.CategoriaTransaccion = categoria;
        }

        [TestMethod]
        public void Categoria_Transaccion()
        {
            var transaccion = new TransaccionCosto();
            Categoria categoria = new Categoria();
            categoria.Nombre = "Categoria1";
            categoria.Tipo = TipoCategoria.Costo;
            categoria.EstadoActivo = true;
            transaccion.CategoriaTransaccion = categoria;
            Assert.AreEqual(categoria, transaccion.CategoriaTransaccion);
        }
    }
}
