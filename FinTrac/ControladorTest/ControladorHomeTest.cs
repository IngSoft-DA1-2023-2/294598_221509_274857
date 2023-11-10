using BussinesLogic;
using Domain;
using Repository;
using Controlador;
using DTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorHomeTest
	{
		private IRepository<Usuario> _repository;
		private UsuarioLogic _usuarioLogic;
		private UsuariosDbContext _context;
		private readonly IDbContextFactory _contextFactory = new InMemoryDbContextFactory();

		[TestInitialize]
		public void TestInitialize()
		{
			_context = _contextFactory.CreateDbContext();
			_repository = new UsuarioMemoryRepository(_context);
			_usuarioLogic = new UsuarioLogic(_repository);


			var usuario2 = new Usuario()
			{
				Correo = "holaSoy2@gmail.com",
				Nombre = "Alberto",
				Apellido = "Rodriguez",
				Contrasena = "123tttt9Aaa",
				Direccion = "street 67 av white"
			};
			
			_usuarioLogic.AddUsuario(usuario2);
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
		public void ControladorHome_Tiene_UsuarioLogic()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorHome_Tiene_UsuarioDTO()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			UsuarioDTO usuario = controladorTest.Usuario;
			Assert.IsNotNull(usuario);
			Assert.AreEqual("Juan", usuario.Nombre);
			Assert.AreEqual("Perez", usuario.Apellido);
			Assert.AreEqual("123456789Aaa", usuario.Contrasena);
			Assert.AreEqual("street 56 av rety", usuario.Direccion);
			Assert.AreEqual("123456789Aaa", usuario.Contrasena);
		}

		[TestMethod]
		public void ControladorHome_Tiene_Constructor()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Nombre()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			string mensaje = "";
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			mensaje = controladorTest.ModificarNombre("Juan");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
			Assert.AreEqual("Sus datos han sido modificados correctamente.", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_No_Debe_Modificar_Nombre_Incorrecto()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			string mensaje = "";
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			mensaje = controladorTest.ModificarNombre("");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Juan", usuarioModificado.Nombre);
			Assert.AreEqual("El nombre es requerido", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Apellido()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			string mensaje = controladorTest.ModificarApellido("Nu�ez");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Nu�ez", usuarioModificado.Apellido);
			Assert.AreEqual("Sus datos han sido modificados correctamente.", mensaje);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Contrasena()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarContrasena("HOLAhola123");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("HOLAhola123", usuarioModificado.Contrasena);
		}

		[TestMethod]
		public void Modificar_Datos_Usuario_Debe_Modificar_Direccion()
		{
			var usuario1 = new Usuario()
			{
				Correo = "hola@gmail.com",
				Nombre = "Juan",
				Apellido = "Perez",
				Contrasena = "123456789Aaa",
				Direccion = "street 56 av rety"
			};
			_usuarioLogic.AddUsuario(usuario1);
			ControladorHome controladorTest = new ControladorHome(_usuarioLogic, "hola@gmail.com");
			controladorTest.ModificarDireccion("Av Bvar espa�a 3456");
			Usuario usuarioModificado = _usuarioLogic.FindUsuario("hola@gmail.com");
			Assert.AreEqual("Av Bvar espa�a 3456", usuarioModificado.Direccion);
		}
	}
}