﻿using DTO;

namespace DTOTest
{
	[TestClass]
	public class CategoriaDTOTest
	{
		[TestMethod]
		public void CategoriaDTO_Tiene_Nombre()
		{
			CategoriaDTO categoria = new CategoriaDTO();
			categoria.Nombre = "Categoria";
			Assert.AreEqual("Categoria", categoria.Nombre);
		}
	}
}
