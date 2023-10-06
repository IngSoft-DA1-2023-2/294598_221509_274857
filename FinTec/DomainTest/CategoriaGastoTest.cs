﻿using Domain;

namespace DomainTest
{
    [TestClass]
    public class CategoriaGastoTest
    {
        [TestMethod]
        public void CategoriaGasto_No_Nulo()
        {
            var _categoria = new CategoriaGasto();
            Assert.IsNotNull(_categoria);
        }

        [TestMethod]
        public void CategoriaGasto_Constructor_No_Nulo()
        {
            Categoria cat = new Categoria();
            var categoria = new CategoriaGasto(cat);
            Assert.IsNotNull(categoria.Categoria);
        }

        [TestMethod]
        public void CategoriaGasto_Misma_Categoria()
        {
            var categoria = new Categoria();
            CategoriaGasto cg = new CategoriaGasto(categoria);
            Assert.AreNotEqual(cg.Categoria, categoria);
        }
    }
}
