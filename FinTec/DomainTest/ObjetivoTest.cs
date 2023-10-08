﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DomainTest
{
	[TestClass]
	public class ObjetivoTest
	{
		private Categoria _categoria;
		private List<Categoria> _categorias = new List<Categoria>();
		private Objetivo _objetivo;

		[TestInitialize]
		public void InitTest()
		{
			_categoria = new Categoria()
			{
				Nombre = "Sueldo",
				Tipo = TipoCategoria.Ingreso,
				EstadoActivo = true
			};
			_categorias.Add(_categoria);

			_objetivo = new Objetivo()
			{
				Titulo = "Objetivo 1",
				MontoMaximo = 1000,
				Categorias = _categorias,
			};
		}

		[TestMethod]
		public void Nuevo_Objetivo()

		{
			var objetivo = new Objetivo();
			Assert.IsNotNull(objetivo);
		}

		[TestMethod]
		public void Tiene_Titulo()
		{
			_objetivo.Titulo = "Objetivo 1";
			Assert.AreEqual("Objetivo 1", _objetivo.Titulo);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainObjetivoException))]
		public void Tiene_Titulo_Vacio()

		{
			_objetivo.Titulo = "";
		}

		[TestMethod]
		public void Monto_Maximo()

		{
			_objetivo.MontoMaximo = 1000;
			Assert.AreEqual(1000, _objetivo.MontoMaximo);
		}

		[TestMethod]
		public void Aplicar_Ojetivo_Categoria()

		{
			_objetivo.Categorias = _categorias;
			var resultado = _objetivo.Categorias;
			Assert.AreEqual(_categorias, resultado);
			Assert.AreEqual(1, resultado.Count);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainObjetivoException))]
		public void Aplicar_Monto_Menor_A_Cero_Al_Objetivo()
		{
			_objetivo.MontoMaximo = -1;
		}

		[TestMethod]
		[ExpectedException(typeof(DomainObjetivoException))]
		public void Excepcion_Aplicar_Lista_Categorias_Vacia_Objetivo()
		{
			_objetivo.Categorias = new List<Categoria>();
		}

		[TestMethod]
		[ExpectedException(typeof(DomainObjetivoException))]
		public void Excepcion_Aplicar_Lista_Categorias_Nula_Objetivo()
		{
			_objetivo.Categorias = null;
		}

	}
}
