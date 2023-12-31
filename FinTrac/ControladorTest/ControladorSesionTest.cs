﻿using LogicaNegocio;
using Dominio;
using Repositorio;
using Controlador;
using DTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorSesionTest
	{
		private IRepositorio<Usuario> _repositorioUsuario;
		private UsuarioLogica _usuarioLogic;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();
		private IRepositorio<Espacio> _repositorioEspacio;
		private EspacioLogica _espacioLogic;

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repositorioUsuario = new UsuarioMemoriaRepositorio(_context);
			_usuarioLogic = new UsuarioLogica(_repositorioUsuario);
			_repositorioEspacio = new EspacioMemoriaRepositorio(_context);
			_espacioLogic = new EspacioLogica(_repositorioEspacio);

			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			var usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			_usuarioLogic.AgregarUsuario(usuario1);
			_usuarioLogic.AgregarUsuario(usuario2);
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorSesion_Valida_Usuario_Por_Correo_Contrasena()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};
			_espacioLogic.AgregarEspacio(espacio);
			ControladorSesion controladorTest = new ControladorSesion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.ValidarInicioSesion(usuario.Correo, usuario.Contrasena);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorSesion_No_Valida_Usuario_Por_Correo_Contrasena_Mensaje_Excepcion()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};
			_espacioLogic.AgregarEspacio(espacio);
			ControladorSesion controladorTest = new ControladorSesion(_usuarioLogic, _espacioLogic);
			string mensaje = controladorTest.ValidarInicioSesion("pedro@gmail.com", "AAAAaaaHd");
			Assert.AreEqual("El usuario o la contraseña no son válidos.", mensaje);
		}

		[TestMethod]
		public void ControladorSesion_Da_UsuarioDTO_Logeado_Por_Correo_Contrasena()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};
			_espacioLogic.AgregarEspacio(espacio);
			ControladorSesion controladorTest = new ControladorSesion(_usuarioLogic, _espacioLogic);
			UsuarioDTO resultado = controladorTest.DarUsuarioLogeado("test@gmail.com", "TestTest12");
			Assert.AreEqual("Usuario", resultado.Nombre);
			Assert.AreEqual("Test", resultado.Apellido);
			Assert.AreEqual("test@gmail.com", resultado.Correo);
			Assert.AreEqual("Av test", resultado.Direccion);
			Assert.AreEqual("TestTest12", resultado.Contrasena);
		}

		[TestMethod]
		public void ControladorSesion_No_Da_Usuario_No_Existente_Logeado_Por_Correo_Contrasena()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};
			_espacioLogic.AgregarEspacio(espacio);
			ControladorSesion controladorTest = new ControladorSesion(_usuarioLogic, _espacioLogic);
			UsuarioDTO resultado = controladorTest.DarUsuarioLogeado("pedro@gmail.com", "AAAAaaaHd");
			Assert.AreEqual(null, resultado);
		}

		[TestMethod]
		public void ControladorSesion_Da_EspacioDTO_Actual_Con_Espacio_Id()
		{
			Usuario usuario = new Usuario
			{
				Nombre = "Usuario",
				Apellido = "Test",
				Correo = "test@gmail.com",
				Contrasena = "TestTest12",
				Direccion = "Av test"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			Espacio espacio = new Espacio
			{
				Nombre = "Espacio",
				Id = 1,
				Admin = usuario
			};
			_espacioLogic.AgregarEspacio(espacio);
			ControladorSesion controladorTest = new ControladorSesion(_usuarioLogic, _espacioLogic);
			EspacioDTO resultado = controladorTest.EspacioActual(1);
			Assert.AreEqual("Espacio", resultado.Nombre);
			Assert.AreEqual(1, resultado.Id);
			Assert.AreEqual(usuario.Correo, resultado.Admin.Correo);
		}
	}
}
