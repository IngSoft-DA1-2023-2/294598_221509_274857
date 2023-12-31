﻿using Excepcion;
using Dominio;

namespace DominioTest
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
		[ExpectedException(typeof(DominioEspacioExcepcion))]
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
		[ExpectedException(typeof(DominioEspacioExcepcion))]
		public void Aplicar_Monto_Menor_A_Cero_Al_Objetivo()
		{
			_objetivo.MontoMaximo = -1;
		}

		[TestMethod]
		[ExpectedException(typeof(DominioEspacioExcepcion))]
		public void Excepcion_Aplicar_Lista_Categorias_Vacia_Objetivo()
		{
			_objetivo.Categorias = new List<Categoria>();
		}

		[TestMethod]
		[ExpectedException(typeof(DominioEspacioExcepcion))]
		public void Excepcion_Aplicar_Lista_Categorias_Nula_Objetivo()
		{
			_objetivo.Categorias = null;
		}

		[TestMethod]
		public void Contiene_Categoria()
		{
			var categoria = new Categoria()
			{
				Nombre = "Sueldo",
				Tipo = TipoCategoria.Ingreso,
				EstadoActivo = true
			};

			var objetivo = new Objetivo()
			{
				Titulo = "Objetivo 1",
				MontoMaximo = 1000,
			};
			objetivo.Categorias.Add(categoria);
			var resultado = objetivo.ContieneCategoria(categoria);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		public void Objetivo_Tiene_Id()
		{
			_objetivo.Id = 1;
			Assert.AreEqual(1, _objetivo.Id);
		}

		[TestMethod]
		public void Objetivo_Tiene_EspacioId()
		{
			_objetivo.EspacioId = 1;
			Assert.AreEqual(1, _objetivo.EspacioId);
		}

		[TestMethod]
		public void Objetivo_Tiene_Espacio()
		{
			var espacio = new Espacio()
			{
				Nombre = "Espacio 1",
				Admin = new Usuario()
				{
					Correo = "test@gmail.com",
					Contrasena = "HOLAhola123",
					Nombre = "Test",
					Apellido = "Test",
					Direccion = "Test"
				}
			};
			_objetivo.Espacio = espacio;
			Assert.AreEqual(espacio, _objetivo.Espacio);
		}

		[TestMethod]
		public void Objetivo_Get_Token() 
		{
			_objetivo.Token = "token";
			Assert.AreEqual("token", _objetivo.Token);
		}
	}
}
