﻿using LogicaNegocio;
using Dominio;
using Repositorio;
using DTO;
using Controlador;
using DTO.EnumsDTO;

namespace ControladorTest
{
	[TestClass]
	public class ControladorTransaccionTest
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
		public void TransaccionesDatosTest()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Id = 1,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 2,
			};
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 3,
			};
			espacio.Cuentas.Add(ahorro3);
			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test",
					Id = 1,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Costo,
				},
			};
			Transaccion transaccion2 = new TransaccionIngreso()
			{
				CuentaMonetaria = ahorro,
				Id = 2,
				Monto = 1000,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Titulo = "Test2",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test2",
					Id = 2,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Ingreso,
				},
			};
			Transaccion transaccion3 = new TransaccionIngreso()
			{
				CuentaMonetaria = ahorro3,
				Id = 3,
				Monto = 10300,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Test3",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test3",
					Id = 3,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Ingreso,
				},
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			espacio.Transacciones.Add(transaccion2);
			espacio.Transacciones.Add(transaccion3);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			List<TransaccionDTO> transacciones = controladorTransaccion.TransaccionesDatos(1);

			Assert.AreEqual(3, transacciones.Count);
		}

		[TestMethod]
		public void ControladorTransacciones_Tiene_DatosCuenta()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro,
				Id = 1,
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 2,
			};
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 3,
			};
			espacio.Cuentas.Add(ahorro3);
			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = new Categoria()
				{
					Nombre = "Test",
					Id = 1,
					EstadoActivo = true,
					FechaCreacion = DateTime.Now,
					Tipo = TipoCategoria.Costo,
				},
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			List<string> transacciones = controladorTransaccion.DatosCuentasEspacio(1);

			Assert.AreEqual(3, transacciones.Count);
		}

		[TestMethod]
		public void ControladorTransaccion_CreaTransaccion_De_Ingreso()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionIngreso(1, transaccion);

			Espacio resultado = _espacioLogic.EncontrarEspacio(1);
			List<Transaccion> resultadoTransacciones = resultado.Transacciones;
			Assert.AreEqual(1, resultadoTransacciones.Count);
			Assert.AreEqual(100, resultadoTransacciones[0].Monto);
			Assert.AreEqual(TipoCambiario.PesosUruguayos, resultadoTransacciones[0].Moneda);
			Assert.AreEqual("Test", resultadoTransacciones[0].Titulo);
			Assert.AreEqual(categoria.Nombre, resultadoTransacciones[0].CategoriaTransaccion.Nombre);
			Assert.AreEqual(ahorro, resultadoTransacciones[0].CuentaMonetaria);
			Assert.AreEqual("Ingreso", resultadoTransacciones[0].Tipo());
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorTransaccion_Lanza_Mensaje_Excepcion_Al_CreaTransaccion_De_Ingreso()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionIngreso(1, transaccion);

			Assert.AreEqual("No hay cotización cambiaria de Dolar para la fecha de hoy", mensaje);
		}

		[TestMethod]
		public void ControladorTransaccion_CreaTransaccion_De_Costo()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionCosto(1, transaccion);

			Espacio resultado = _espacioLogic.EncontrarEspacio(1);
			List<Transaccion> resultadoTransacciones = resultado.Transacciones;
			Assert.AreEqual(1, resultadoTransacciones.Count);
			Assert.AreEqual(100, resultadoTransacciones[0].Monto);
			Assert.AreEqual(TipoCambiario.PesosUruguayos, resultadoTransacciones[0].Moneda);
			Assert.AreEqual("Test", resultadoTransacciones[0].Titulo);
			Assert.AreEqual(categoria.Nombre, resultadoTransacciones[0].CategoriaTransaccion.Nombre);
			Assert.AreEqual(ahorro, resultadoTransacciones[0].CuentaMonetaria);
			Assert.AreEqual("Costo", resultadoTransacciones[0].Tipo());
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorTransaccion_Lanza_Mensaje_Excepcion_Al_CreaTransaccion_De_Costo()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			TransaccionDTO transaccion = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.Dolar,
				Titulo = "Test",
				CategoriaTransaccion = categoria.Nombre,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);
			string mensaje = controladorTransaccion.CrearTransaccionCosto(1, transaccion);

			Assert.AreEqual("No hay cotización cambiaria de Dolar para la fecha de hoy", mensaje);
		}

		[TestMethod]
		public void ControladorTransaccion_Da_Lista_De_TransaccionesDTOCosto_Del_Espacio()
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
			Categoria categoriaCosto = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			Categoria categoriaIngreso = new Categoria()
			{
				Nombre = "Test2",
				Id = 2,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Ingreso,
			};
			espacio.Categorias.Add(categoriaCosto);
			espacio.Categorias.Add(categoriaIngreso);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			List<CategoriaDTO> categoriasDTO = controladorTransaccion.DarCategoriasCosto(1);

			Assert.AreEqual(1, categoriasDTO.Count);
			Assert.AreEqual("Test", categoriasDTO[0].Nombre);
			Assert.AreEqual(TipoCategoriaDTO.Costo, categoriasDTO[0].Tipo);
			Assert.AreEqual(true, categoriasDTO[0].EstadoActivo);
			Assert.AreEqual(1, categoriasDTO[0].Id);
		}

		[TestMethod]
		public void ControladorTransaccion_Da_Lista_De_TransaccionesDTOIngreso_Del_Espacio()
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
			Categoria categoriaCosto = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			Categoria categoriaIngreso = new Categoria()
			{
				Nombre = "Test2",
				Id = 2,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Ingreso,
			};
			espacio.Categorias.Add(categoriaCosto);
			espacio.Categorias.Add(categoriaIngreso);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			List<CategoriaDTO> categoriasDTO = controladorTransaccion.DarCategoriasIngreso(1);

			Assert.AreEqual(1, categoriasDTO.Count);
			Assert.AreEqual("Test2", categoriasDTO[0].Nombre);
			Assert.AreEqual(TipoCategoriaDTO.Ingreso, categoriasDTO[0].Tipo);
			Assert.AreEqual(true, categoriasDTO[0].EstadoActivo);
			Assert.AreEqual(2, categoriasDTO[0].Id);
		}

		[TestMethod]
		public void Dar_IdEspacio_IdTransaccion_Retorna_TransaccionDTO()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria,
			};

			espacio.Transacciones.Add(transaccion);

			_espacioLogic.AgregarEspacio(espacio);

			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);
			TransaccionDTO transaccionDTO = controladorTransaccion.DarTransaccion(1, 1);
			Assert.IsNotNull(transaccionDTO);
			Assert.AreEqual(1, transaccionDTO.Id);
			Assert.AreEqual("Test", transaccionDTO.Titulo);
			Assert.AreEqual(100, transaccionDTO.Monto);
			Assert.AreEqual(TipoCambiarioDTO.PesosUruguayos, transaccionDTO.Moneda);
		}

		[TestMethod]
		public void ControladorTransaccion_Modifica_Monto_Y_Categoria_Correctamente()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			Categoria categoria2 = new Categoria()
			{
				Nombre = "TestCat2",
				Id = 2,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria2);

			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria,
			};

			TransaccionDTO transModificada = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 200,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = "TestCat2",
			};
			espacio.Transacciones.Add(transaccion);

			_espacioLogic.AgregarEspacio(espacio);

			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			string mensaje = controladorTransaccion.ModificarTransaccion(1, transModificada);

			Assert.AreEqual("", mensaje);
			Assert.AreEqual(transaccion.CategoriaTransaccion, categoria2);
			Assert.AreEqual(transModificada.Monto, transaccion.Monto);
		}

		[TestMethod]
		public void ControladorTransaccion_No_Modifica_Monto_Y_Categoria_Da_Mensaje_Excepcion()
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

			Ahorro ahorro = new Ahorro
			{
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Id = 1,
			};

			espacio.Cuentas.Add(ahorro);

			Categoria categoria = new Categoria()
			{
				Nombre = "Test",
				Id = 1,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria);

			Categoria categoria2 = new Categoria()
			{
				Nombre = "TestCat2",
				Id = 2,
				EstadoActivo = true,
				FechaCreacion = DateTime.Now,
				Tipo = TipoCategoria.Costo,
			};

			espacio.Categorias.Add(categoria2);

			Transaccion transaccion = new TransaccionCosto()
			{
				CuentaMonetaria = ahorro,
				Id = 1,
				Monto = 100,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = categoria,
			};

			TransaccionDTO transModificada = new TransaccionDTO()
			{
				CuentaMonetaria = ahorro.ToString(),
				Id = 1,
				Monto = 200,
				FechaTransaccion = DateTime.Now,
				Moneda = TipoCambiarioDTO.PesosUruguayos,
				Titulo = "Test",
				CategoriaTransaccion = "",
			};
			espacio.Transacciones.Add(transaccion);

			_espacioLogic.AgregarEspacio(espacio);

			ControladorTransaccion controladorTransaccion = new ControladorTransaccion(_espacioLogic);

			string mensaje = controladorTransaccion.ModificarTransaccion(1, transModificada);

			Assert.AreEqual("La categoria no puede ser nula", mensaje);
			Assert.AreEqual(transaccion.CategoriaTransaccion, categoria);
			Assert.AreEqual(transaccion.Monto, 100);
		}
	}
}
