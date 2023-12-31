﻿using Excepcion;
using Dominio;
using Repositorio;
using LogicaNegocio;

namespace LogicaNegocioTest
{
	[TestClass]
	public class UsuarioLogicaTest
	{
		private IRepositorio<Usuario> _repository;
		private UsuarioLogica _usuarioLogic;
		private Usuario _usuario1;
		private Usuario _usuario2;
		private FintracDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void Setup()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoriaRepositorio(_context);
			_usuarioLogic = new UsuarioLogica(_repository);
			_usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};

			_usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
			_context = null;
			_repository = null;
			_usuarioLogic = null;
		}

		[TestMethod]
		public void Nuevo_UsuarioLogic()
		{
			Assert.IsNotNull(_usuarioLogic);
		}

		[TestMethod]
		public void Agregar_Usuario()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			var usuarioAgregado = _repository.Find(u => u.Correo == _usuario1.Correo);
			bool resultado = _usuario1.Equals(usuarioAgregado);
			Assert.IsNotNull(usuarioAgregado);
			Assert.AreEqual(_usuario1.Correo, usuarioAgregado.Correo);
			Assert.AreEqual(_usuario1.Contrasena, usuarioAgregado.Contrasena);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Contrasena_invalida_UL()
		{
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.com";
			usuario1.Contrasena = "1234567890";
			_usuarioLogic.AgregarUsuario(usuario1);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Correo_invalido_UL()
		{
			Usuario usuario1 = new Usuario();
			usuario1.Correo = "xxxx@yyyy.co";
			usuario1.Contrasena = "123456789Aa";
			_usuarioLogic.AgregarUsuario(usuario1);
		}

		[TestMethod]
		public void Buscar_Todos_Usuarios()
		{

			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			var usuarios = _usuarioLogic.DarUsuarios();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(2, usuarios.Count);
		}

		[TestMethod]
		public void Eliminar_Usuario()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			_usuarioLogic.BorrarUsuario(_usuario1.Correo);
			var usuarios = _usuarioLogic.DarUsuarios();
			Assert.IsNotNull(usuarios);
			Assert.AreEqual(1, usuarios.Count);
		}

		[TestMethod]
		public void Buscar_Usuario()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			var usuario = _usuarioLogic.EncontrarUsuario(_usuario1.Correo);
			Assert.IsNotNull(usuario);
			Assert.AreEqual(_usuario1.Correo, usuario.Correo);
			Assert.AreEqual(_usuario1.Contrasena, usuario.Contrasena);
		}

		[TestMethod]
		public void Agregar_Usuario_Valido()
		{
			var usuarioAgregado1 = _usuarioLogic.AgregarUsuario(_usuario1);
			var usuarioAgregado2 = _usuarioLogic.AgregarUsuario(_usuario2);
			Assert.IsNotNull(usuarioAgregado1);
			Assert.AreEqual(_usuario1, usuarioAgregado1);
			Assert.IsNotNull(usuarioAgregado2);
			Assert.AreEqual(_usuario2, usuarioAgregado2);
		}


		[TestMethod]
		[ExpectedException(typeof(LogicaNegocioUsuarioExcepcion))]
		public void Agregar_Usuario_Invalido_Duplicado()
		{
			var usuario1 = new Usuario
			{
				Nombre = "Juan",
				Apellido = "Perez",
				Direccion = "street 56 av rety",
				Correo = "Juan@xxxx.com",
				Contrasena = "123456aasaU",
			};
			var usuario2 = new Usuario
			{
				Nombre = "Juan",
				Apellido = "Perez",
				Direccion = "street 56 av rety",
				Contrasena = "123456aasaU",
				Correo = "Juan@xxxx.com"
			};
			_usuarioLogic.AgregarUsuario(usuario1);
			_usuarioLogic.AgregarUsuario(usuario2);
		}

		[TestMethod]
		public void Ingreso_Correo_Contrasena_Valida_Entrega_Usuario()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			Usuario usuario = _usuarioLogic.UsuarioPorCorreoContrasena("hola@gmail.com", "123456789Aaa");
			bool resultado = usuario.Equals(_usuario1);
			bool contrasenaIgual = usuario.Contrasena == _usuario1.Contrasena;
			Assert.IsTrue(resultado);
			Assert.IsTrue(contrasenaIgual);
		}

		[TestMethod]
		[ExpectedException(typeof(LogicaNegocioUsuarioExcepcion))]
		public void Excepcion_Ingreso_Correo_Valido_Contrasena_No_Existente()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			Usuario usuario = _usuarioLogic.UsuarioPorCorreoContrasena("hola@gmail.com", "");
		}

		[TestMethod]
		[ExpectedException(typeof(LogicaNegocioUsuarioExcepcion))]
		public void Excepcion_Ingreso_Correo_No_Existente()
		{
			_usuarioLogic.AgregarUsuario(_usuario2);
			_usuarioLogic.AgregarUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioPorCorreoContrasena("", "");
		}

		[TestMethod]
		[ExpectedException(typeof(LogicaNegocioUsuarioExcepcion))]
		public void Excepcion_Ingreso_Correo_Contrasena_Nula()
		{
			_usuarioLogic.AgregarUsuario(_usuario2);
			_usuarioLogic.AgregarUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioPorCorreoContrasena(null, null);
		}

		[TestMethod]
		[ExpectedException(typeof(LogicaNegocioUsuarioExcepcion))]
		public void Excepcion_Ingreso_Correo_No_Existente_4()
		{
			_usuarioLogic.AgregarUsuario(_usuario1);
			_usuarioLogic.AgregarUsuario(_usuario2);
			Usuario resultado = _usuarioLogic.UsuarioPorCorreoContrasena("123456789Aaa", null);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Nombre()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AgregarUsuario(usuario);

			_usuarioLogic.ModificarNombre(usuario.Correo, "Juan");

			Usuario usuarioModificado = _usuarioLogic.EncontrarUsuario(usuario.Correo);
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Apellido()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AgregarUsuario(usuario);

			_usuarioLogic.ModificarApellido(usuario.Correo, "Perez");

			Usuario usuarioModificado = _usuarioLogic.EncontrarUsuario(usuario.Correo);
			Assert.AreEqual("Perez", usuarioModificado.Apellido);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Contrasena()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AgregarUsuario(usuario);

			_usuarioLogic.ModificarContrasena(usuario.Correo, "123456789Aaa");

			Usuario usuarioModificado = _usuarioLogic.EncontrarUsuario(usuario.Correo);
			Assert.AreEqual("123456789Aaa", usuarioModificado.Contrasena);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Direccion()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "alberto@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			_usuarioLogic.AgregarUsuario(usuario);

			_usuarioLogic.ModificarDireccion(usuario.Correo, "street 56 av rety");

			Usuario usuarioModificado = _usuarioLogic.EncontrarUsuario(usuario.Correo);
			Assert.AreEqual("street 56 av rety", usuarioModificado.Direccion);
		}

		[TestMethod]
		public void CrearUsuario_ConCorreoValido_Correctamente()
		{
			var usuario = new Usuario()
			{
				Correo = "norberto@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety",
				IdEspacioPrincipal = 1
			};
			_usuarioLogic.CrearUsuario(usuario);
			Usuario usuarioCreado =_usuarioLogic.EncontrarUsuario("norberto@gmail.com");
			Assert.IsNotNull(usuarioCreado);
			Assert.AreEqual("norberto@gmail.com", usuarioCreado.Correo);
		}

		[TestMethod]
		public void UsuariosNoPresentesEnEspacio_Devuelve_Lista_Valida()
		{ 
			Usuario usuario = new Usuario()
			{
				Correo = "test@gmail.com",
				Nombre = "Alberto",
				Apellido = "Lopez",
				Contrasena = "HOLAhola123",
				Direccion = "Bv España 5566"
			};
			Usuario usuarioTest1 = new Usuario()
			{
				Correo = "test2@gmail.com",
				Nombre = "Roberto",
				Apellido = "Ramirez",
				Contrasena = "HOLAeehola123",
				Direccion = "Bv España 4444"
			};
			Usuario usuarioTest2 = new Usuario()
			{
				Correo = "test3@gmail.com",
				Nombre = "Julio",
				Apellido = "Martinez",
				Contrasena = "HOLee4ehola123",
				Direccion = "Bv España 546"
			};
			_usuarioLogic.AgregarUsuario(usuario);
			_usuarioLogic.AgregarUsuario(usuarioTest1);
			_usuarioLogic.AgregarUsuario(usuarioTest2);
			Espacio espacio = new Espacio()
			{
				Id = 1,
				Nombre = "Principal " + usuario.Nombre,
				Admin = usuario
			};
			List<Usuario> usuariosNoPresentes = _usuarioLogic.UsuariosNoPresentesEspacio(espacio);
			Assert.IsNotNull(usuariosNoPresentes);
			Assert.AreEqual(2, usuariosNoPresentes.Count);
		}
	}
}
