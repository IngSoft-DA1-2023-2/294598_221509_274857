﻿using DTO;
using Controlador;
using LogicaNegocio;
using Dominio;
using Repositorio;
using System.Linq;

namespace ControladorTest
{
	[TestClass]
	public class ControladorCuentaTest
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
		}

		[TestCleanup]
		public void Cleanup()
		{
			_context.Database.EnsureDeleted();
			_context.Dispose();
		}

		[TestMethod]
		public void ControladorCuenta_Inicializa_Correctamente()
		{
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			Assert.IsNotNull(controladorTest);
		}

		[TestMethod]
		public void ControladorCuenta_AhorrosDeEspacio_Retorna_ListaDeAhorros()
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
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			Ahorro ahorro3 = new Ahorro
			{
				Nombre = "AhorroTest3",
				Saldo = 1050,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Euro
			};
			espacio.Cuentas.Add(ahorro3);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta( _espacioLogic);

			List<AhorroDTO> ahorros = controladorTest.AhorrosDeEspacio(espacio.Id);
			Assert.AreEqual(3, ahorros.Count);
			Assert.AreEqual(ahorro.Nombre, ahorros[0].Nombre);
			Assert.AreEqual(ahorro.Saldo, ahorros[0].Saldo);
			Assert.AreEqual(ahorro.FechaCreacion, ahorros[0].FechaCreacion);
			Assert.AreEqual(ahorro2.Nombre, ahorros[1].Nombre);
			Assert.AreEqual(ahorro2.Saldo, ahorros[1].Saldo);
			Assert.AreEqual(ahorro2.FechaCreacion, ahorros[1].FechaCreacion);
			Assert.AreEqual(ahorro3.Nombre, ahorros[2].Nombre);
			Assert.AreEqual(ahorro3.Saldo, ahorros[2].Saldo);
			Assert.AreEqual(ahorro3.FechaCreacion, ahorros[2].FechaCreacion);
		}

		[TestMethod]
		public void ControladorCuenta_CreditosDeEspacio_Retorna_ListaDeCreditos()
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
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				Moneda = TipoCambiario.Dolar
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
				Moneda = TipoCambiario.PesosUruguayos
			};
			espacio.Cuentas.Add(credito1);
			Credito credito2 = new Credito()
			{
				NumeroTarjeta = "1255",
				BancoEmisor = "Credito2Test",
				Saldo = 1070,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
				Moneda = TipoCambiario.Euro
			};
			espacio.Cuentas.Add(credito2);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);

			List<CreditoDTO> creditos = controladorTest.CreditosDeEspacio(espacio.Id);
			Assert.AreEqual(2, creditos.Count);
			Assert.AreEqual(credito1.BancoEmisor, creditos[0].BancoEmisor);
			Assert.AreEqual(credito1.NumeroTarjeta, creditos[0].NumeroTarjeta);
			Assert.AreEqual(credito1.FechaCreacion, creditos[0].FechaCreacion);
			Assert.AreEqual(credito1.FechaCierre, creditos[0].FechaCierre);
			Assert.AreEqual(credito1.Saldo, creditos[0].Saldo);
			Assert.AreEqual(credito2.BancoEmisor, creditos[1].BancoEmisor);
			Assert.AreEqual(credito2.NumeroTarjeta, creditos[1].NumeroTarjeta);
			Assert.AreEqual(credito2.FechaCreacion, creditos[1].FechaCreacion);
			Assert.AreEqual(credito2.FechaCierre, creditos[1].FechaCierre);
			Assert.AreEqual(credito2.Saldo, creditos[1].Saldo);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarAhorro_Elimina_Ahorro_De_Espacio()
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
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO = new AhorroDTO
			{
				Id = ahorro2.Id,
				Nombre = ahorro2.Nombre,
				Saldo = ahorro2.Saldo,
				FechaCreacion = ahorro2.FechaCreacion,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.EliminarAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual(2, espacio.Cuentas.Count);
			Assert.IsFalse(espacio.Cuentas.Contains(ahorro2));
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarAhorro_Retorna_Mensaje_Excepcion()
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
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO = new AhorroDTO
			{
				Id = ahorro2.Id,
				Nombre = ahorro2.Nombre,
				Saldo = ahorro2.Saldo,
				FechaCreacion = ahorro2.FechaCreacion,
			};
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = ahorro2,
				Id = 1,
				Monto = 100,
				Titulo = "Test",
			};
			espacio.Cuentas.Add(ahorro2);
			espacio.Transacciones.Add(transaccion);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.EliminarAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.IsTrue(espacio.Cuentas.Contains(ahorro2));
			Assert.AreEqual("No se puede borrar una cuenta que tiene transacciones asociadas", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarCredito_Elimina_Credito_De_Espacio()
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
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.EliminarCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(2, espacio.Cuentas.Count);
			Assert.IsFalse(espacio.Cuentas.Contains(credito1));
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_EliminarCredito_Retorna_Mensaje_Excepcion()
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
			};
			espacio.Cuentas.Add(ahorro);
			Ahorro ahorro2 = new Ahorro
			{
				Nombre = "AhorroTest2",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			CreditoDTO creditoDTO = new CreditoDTO
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};

			espacio.Cuentas.Add(ahorro2);
			Credito credito1 = new Credito()
			{
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito1);
			Transaccion transaccion = new Transaccion()
			{
				CuentaMonetaria = credito1,
				Id = 1,
				Monto = 100,
				Titulo = "Test",
			};
			espacio.Transacciones.Add(transaccion);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.EliminarCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.IsTrue(espacio.Cuentas.Contains(credito1));
			Assert.AreEqual("No se puede borrar una cuenta que tiene transacciones asociadas", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modifica_Ahorro_Exitosamente()
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
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			AhorroDTO ahorroModificado = new AhorroDTO
			{
				Id = 3,
				Nombre = "AhorroModificado",
				Saldo = 105550,
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarAhorro(espacio.Id, ahorroModificado);

			Assert.AreEqual(ahorroModificado.Nombre, ahorro.Nombre);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Modifica_Ahorro_Mensaje_Excepcion()
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
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			AhorroDTO ahorroModificado = new AhorroDTO
			{
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 105550,
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarAhorro(espacio.Id, ahorroModificado);

			Assert.AreEqual(ahorroModificado.Nombre, ahorro.Nombre);
			Assert.AreEqual("No se puede Modificar, hay cuentas ya registradas con ese nombre", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modifica_Credito_Exitosamente()
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
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 1,
				NumeroTarjeta = "1434",
				BancoEmisor = "ModificadoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarCredito(espacio.Id, creditoDTO);

			Assert.AreEqual(creditoDTO.NumeroTarjeta, credito.NumeroTarjeta);
			Assert.AreEqual(creditoDTO.BancoEmisor, credito.BancoEmisor);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Modifica_Credito_Mensaje_Excepcion()
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
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
			};
			espacio.Cuentas.Add(ahorro);
			Credito credito = new Credito()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 1,
				NumeroTarjeta = "1234",
				BancoEmisor = "CreditoTest",
				Saldo = 100,
				FechaCierre = new DateTime(2025, 4, 20),
				FechaCreacion = new DateTime(2010, 4, 20),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarCredito(espacio.Id, creditoDTO);

			Assert.AreEqual(creditoDTO.NumeroTarjeta, credito.NumeroTarjeta);
			Assert.AreEqual(creditoDTO.BancoEmisor, credito.BancoEmisor);
			Assert.AreEqual("No se puede Modificar, hay cuentas ya registradas con ese nombre", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Crea_CuentaAhorro_Con_Exito()
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
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest1",
				Saldo = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Dolar,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO2 = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest2",
				Saldo = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Euro,
				FechaCreacion = DateTime.Now,
			};
			AhorroDTO ahorroDTO3 = new AhorroDTO()
			{
				Id = 3,
				Nombre = "AhorroTest3",
				Saldo = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.PesosUruguayos,
				FechaCreacion = DateTime.Now,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearAhorro(espacio.Id, ahorroDTO);
			string mensaje2 = controladorTest.CrearAhorro(espacio.Id, ahorroDTO2);
			string mensaje3 = controladorTest.CrearAhorro(espacio.Id, ahorroDTO3);
			Assert.AreEqual(3, espacio.Cuentas.Count);
			Assert.AreEqual("", mensaje);
			Assert.AreEqual("", mensaje2);
			Assert.AreEqual("", mensaje3);
		}

		[TestMethod]
		public void ControladorCuenta_LNo_Crea_CuentaAhorro_Mensaje_Excepcion()
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
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				Id = 3,
				Nombre = "",
				Saldo = 100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Dolar,
				FechaCreacion = DateTime.Now,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual("El nombre de la cuenta no puede ser vacío", mensaje);
		}


		[TestMethod]
		public void ControladorCuenta_Crea_CuentaCredito_Con_Exito()
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
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "BancoTest",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(1, espacio.Cuentas.Count);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Crea_CuentaCredito_Mensaje_Excepcion()
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
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(0, espacio.Cuentas.Count);
			Assert.AreEqual("El banco emisor no puede ser vacío", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Crea_Cuenta_Ahorro_Saldo_Menor_Igual_Cero_Mensaje_Excepcion()
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
			AhorroDTO ahorroDTO = new AhorroDTO()
			{
				Id = 3,
				Nombre = "cuenta",
				Saldo = -100,
				Moneda = DTO.EnumsDTO.TipoCambiarioDTO.Dolar,
				FechaCreacion = DateTime.Now,
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearAhorro(espacio.Id, ahorroDTO);
			Assert.AreEqual(0, espacio.Cuentas.Count);
			Assert.AreEqual("El saldo de la cuenta debe ser mayor a 0", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_No_Crea_Cuenta_Credito_Saldo_Menor_Igual_Cero_Mensaje_Excepcion()
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
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "test",
				NumeroTarjeta = "1234",
				Saldo = -100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};

			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.CrearCredito(espacio.Id, creditoDTO);
			Assert.AreEqual(0, espacio.Cuentas.Count);
			Assert.AreEqual("El saldo de la cuenta debe ser mayor a 0", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modifica_FechaCierre()
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
			Credito credito = new Credito()
			{
				Id = 3,
				BancoEmisor = "test",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "test",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2056, 4, 5),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarCreditoFechaCierre(espacio.Id, creditoDTO);
			Assert.AreEqual(creditoDTO.FechaCierre, credito.FechaCierre);
			Assert.AreEqual("", mensaje);
		}

		[TestMethod]
		public void ControladorCuenta_Modificar_FechaCierr_Lanza_Mensaje_De_Excepcion()
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
			Credito credito = new Credito()
			{
				Id = 3,
				BancoEmisor = "test",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2026, 4, 5),
			};
			CreditoDTO creditoDTO = new CreditoDTO()
			{
				Id = 3,
				BancoEmisor = "test",
				NumeroTarjeta = "1234",
				Saldo = 100,
				FechaCreacion = DateTime.Now,
				FechaCierre = new DateTime(2001, 4, 5),
			};
			espacio.Cuentas.Add(credito);
			_espacioLogic.AgregarEspacio(espacio);
			ControladorCuenta controladorTest = new ControladorCuenta(_espacioLogic);
			string mensaje = controladorTest.ModificarCreditoFechaCierre(espacio.Id, creditoDTO);
			Assert.AreEqual("La fecha de cierre no puede ser menor a la fecha actual", mensaje);
		}
	}
}
