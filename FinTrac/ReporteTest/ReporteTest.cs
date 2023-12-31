﻿using Dominio;
using EspacioReporte;
using Excepcion;

namespace DominioTest
{
	[TestClass]
	public class ReporteTest
	{
		[TestMethod]
		public void Reporte_No_Nulo()
		{
			var rep1 = new Reporte();

			Assert.IsNotNull(rep1);
		}

		[TestMethod]
		public void Reporte_Tiene_Espacio()
		{
			var _EspacioRepo = new Espacio();
			Reporte miReporte = new Reporte();
			miReporte.MiEspacio = _EspacioRepo;
			Assert.AreEqual(miReporte.MiEspacio, _EspacioRepo);
		}

		[TestMethod]
		public void ReporteObjetivosGastos_Vacio()
		{
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			var _reporte = new Reporte();
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> _reporteGastos = _reporte.ReporteObjetivosDeGastos();
			Assert.IsTrue(_reporteGastos.Count == 0);
		}

		[TestMethod]
		public void ReporteObjetivosGastos_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cambio cambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(cambio);
			Cuenta _cuenta = new Cuenta { Moneda = TipoCambiario.Dolar };
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			Assert.IsTrue(ret.First().MontoAcumulado == 400);
		}

		[TestMethod]
		public void ReporteObjetivosGastos_No_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			Assert.IsTrue(ret.Count == 1);
		}


		[TestMethod]
		public void buscarCambioActual_Equals()
		{
			Espacio espacio = new Espacio();
			espacio.Admin = new Usuario();
			Cambio miCambio = new Cambio
			{
				FechaDeCambio = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(miCambio);
			Reporte reporte = new Reporte { MiEspacio = espacio };
			Cambio cambioRet = reporte.BuscarCambioActual(DateTime.Today, TipoCambiario.Dolar);
			Assert.AreEqual(cambioRet, miCambio);
		}



		[TestMethod]
		public void buscarCambioActual_No_Nulo()
		{
			Espacio espacio = new Espacio();
			espacio.Admin = new Usuario();
			Cambio miCambio = new Cambio
			{
				FechaDeCambio = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(miCambio);
			Reporte reporte = new Reporte { MiEspacio = espacio };
			Cambio cambioRet = reporte.BuscarCambioActual(DateTime.Today, TipoCambiario.Dolar);
			Assert.IsNotNull(cambioRet);
		}

		[TestMethod]
		public void buscarCambio_Fecha_Otro_Dia()
		{
			Espacio espacio = new Espacio();
			espacio.Admin = new Usuario();
			DateTime fecha = new DateTime(2023, 10, 10);
			Cambio miCambio = new Cambio
			{
				FechaDeCambio = fecha,
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(miCambio);
			Reporte reporte = new Reporte { MiEspacio = espacio };
			Cambio cambioRet = reporte.BuscarCambioActual(DateTime.Today, TipoCambiario.Dolar);
			Assert.IsFalse(cambioRet.Equals(espacio.Cambios.First()));
		}

		[TestMethod]
		public void buscarCambio_Fecha_Otro_Mes()
		{
			Espacio espacio = new Espacio();
			espacio.Admin = new Usuario();
			DateTime fecha = new DateTime(2023, 6, 11);
			Cambio miCambio = new Cambio
			{
				FechaDeCambio = fecha,
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(miCambio);
			Reporte reporte = new Reporte { MiEspacio = espacio };
			Cambio cambioRet = reporte.BuscarCambioActual(DateTime.Today, TipoCambiario.Dolar);
			Assert.IsFalse(cambioRet.Equals(espacio.Cambios.First()));
		}

		[TestMethod]
		public void buscarCambio_Fecha_Otro_Year()
		{
			Espacio espacio = new Espacio();
			espacio.Admin = new Usuario();
			DateTime fecha = new DateTime(2022, 10, 11);
			Cambio miCambio = new Cambio
			{
				//FechaDeCambio = fecha,
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			espacio.AgregarCambio(miCambio);
			Reporte reporte = new Reporte { MiEspacio = espacio };
			Cambio cambioRet = reporte.BuscarCambioActual(DateTime.Today, TipoCambiario.Dolar);
			Assert.IsTrue(cambioRet.Equals(espacio.Cambios.First()));
		}

		[TestMethod]
		public void SumatoriaCostos_Monto_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cambio miCambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(miCambio);
			Ahorro cuenta = new Ahorro
			{
				Saldo = 200,
				Nombre = "cuenta",
				Moneda = TipoCambiario.Dolar,
			};
			_miEspacio.AgregarCuenta(cuenta);
			Categoria categoria = new Categoria
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "nombreCar",
				Tipo = TipoCategoria.Costo,
			};
			_miEspacio.AgregarCategoria(categoria);
			Transaccion t = new Transaccion
			{
				CategoriaTransaccion = categoria,
				CuentaMonetaria = cuenta,
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 1,
				Titulo = "transaccion",
			};
			_miEspacio.AgregarTransaccion(t);
			Reporte reporte = new Reporte { MiEspacio = _miEspacio };
			double monto = reporte.SumatoriaCostos(cuenta);
			Assert.IsTrue(monto == 40);
		}
		[TestMethod]
		public void SumatoriaIngresos_Monto_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cambio miCambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(miCambio);
			Ahorro cuenta = new Ahorro
			{
				Saldo = 200,
				Nombre = "cuenta",
				Moneda = TipoCambiario.Dolar,
			};
			_miEspacio.AgregarCuenta(cuenta);
			Categoria categoria = new Categoria
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "nombreCar",
				Tipo = TipoCategoria.Ingreso,
			};
			_miEspacio.AgregarCategoria(categoria);
			Transaccion t = new Transaccion
			{
				CategoriaTransaccion = categoria,
				CuentaMonetaria = cuenta,
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 1,
				Titulo = "transaccion",
			};
			_miEspacio.AgregarTransaccion(t);
			Reporte reporte = new Reporte { MiEspacio = _miEspacio };
			double monto = reporte.SumatoriaIngresos(cuenta);
			Assert.IsTrue(monto == 40);
		}

		[TestMethod]
		public void BalanceCuentas_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cambio miCambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(miCambio);
			Ahorro cuenta = new Ahorro
			{
				Saldo = 1,
				Nombre = "cuenta",
				Moneda = TipoCambiario.Dolar,
			};
			_miEspacio.AgregarCuenta(cuenta);
			Categoria categoria = new Categoria
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "nombreCar",
				Tipo = TipoCategoria.Ingreso,
			};
			_miEspacio.AgregarCategoria(categoria);
			Transaccion t = new Transaccion
			{
				CategoriaTransaccion = categoria,
				CuentaMonetaria = cuenta,
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 1,
				Titulo = "transaccion",
			};
			_miEspacio.AgregarTransaccion(t);
			Reporte reporte = new Reporte { MiEspacio = _miEspacio };
			double monto = reporte.BalanceCuentas(cuenta);
			Assert.IsTrue(40 == monto);
		}

		[TestMethod]
		public void CalcularMontoTotal_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cambio miCambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(miCambio);
			Ahorro cuenta = new Ahorro
			{
				Saldo = 1,
				Nombre = "cuenta",
				Moneda = TipoCambiario.Dolar,
			};
			_miEspacio.AgregarCuenta(cuenta);
			Categoria categoria = new Categoria
			{
				EstadoActivo = true,
				FechaCreacion = DateTime.Today,
				Nombre = "nombreCar",
				Tipo = TipoCategoria.Costo,
			};
			_miEspacio.AgregarCategoria(categoria);
			Transaccion t = new Transaccion
			{
				CategoriaTransaccion = categoria,
				CuentaMonetaria = cuenta,
				FechaTransaccion = DateTime.Today,
				Moneda = TipoCambiario.Dolar,
				Monto = 1,
				Titulo = "transaccion",
			};
			_miEspacio.AgregarTransaccion(t);
			Reporte reporte = new Reporte { MiEspacio = _miEspacio };
			double monto = reporte.Calcular_MontoTotal(DateTime.Today.Month);
			Assert.IsTrue(40 == monto);
		}

		[TestMethod]
		public void ReporteObjetivoGasto_Mismo_ObjetivoGasto()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			ObjetivoGasto og = new ObjetivoGasto(100, 10);
			og.Objetivo = _objetivo;
			List<ObjetivoGasto> lista = _reporte.ReporteObjetivosDeGastos();
			ObjetivoGasto toCompare = lista.First();
			Assert.AreEqual(og, toCompare);
		}

		[TestMethod]
		public void ReporteObjetivoGasto_Monto_No_Cumple()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 1000,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			ObjetivoGasto toAnalize = ret.First();
			Assert.IsFalse(toAnalize.MontoCumpido());
		}

		[TestMethod]
		public void ReporteObjetivoGasto_Monto_Cumple()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 1,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			ObjetivoGasto toAnalize = ret.First();
			Assert.IsTrue(toAnalize.MontoCumpido());
		}

		[TestMethod]
		public void ReporteObjetivoGasto_Sin_Transacciones_Misma_Categoria()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria2);
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria2);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			Assert.IsTrue(ret.Count == 0);
		}

		[TestMethod]
		public void ReporteObjetivoGasto_Sin_Transacciones_Costo()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			List<Categoria> _listCat = new List<Categoria>();
			_listCat.Add(_categoria2);
			Objetivo _objetivo = new Objetivo
			{
				Categorias = _listCat,
				MontoMaximo = 100,
				Titulo = "Menos gastos",
			};
			_miEspacio.AgregarObjetivo(_objetivo);
			_reporte.MiEspacio = _miEspacio;
			List<ObjetivoGasto> ret = _reporte.ReporteObjetivosDeGastos();
			Assert.IsTrue(ret.Count == 0);
		}

		[TestMethod]
		public void CalcularMontoTotal_Cero()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			double monto = _reporte.Calcular_MontoTotal(DateTime.Now.Month);
			Assert.IsTrue(monto == 0);
		}

		[TestMethod]
		public void CalcularMontoTotal_No_Cero()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			double monto = _reporte.Calcular_MontoTotal(DateTime.Now.Month);
			Assert.IsTrue(monto > 0);
		}

		[TestMethod]
		public void CalcularMontoTotal_Otro_Mes()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			double monto = _reporte.Calcular_MontoTotal(DateTime.Now.Month + 1);
			Assert.IsTrue(monto == 0);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Cuenta_Ahorro()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Ahorro ahorro = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Saldo = 1,
				Nombre = "CuentaAhorro",
			};
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(ahorro);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Distinta_Cuenta_Credito()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit2 = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1122",
			};
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit2,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit2.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Categoria_Ingreso()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit2 = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1122",
			};
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit2,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count != 1);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_CuentaCredito_DistintaCuenta_Ingreso()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void TransaccionDentroDelScope_False()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			bool control = _reporte.TransaccionDentroDelScope(transaccion1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(-4));
			Assert.IsTrue(!control);
		}

		[TestMethod]
		public void TransaccionDentroDelScope_True()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			bool control = _reporte.TransaccionDentroDelScope(transaccion1, DateTime.Now.AddMonths(-5), DateTime.Now.AddMonths(+1));
			Assert.IsFalse(!control);
		}

		[TestMethod]
		public void TransaccionDentroDelScope_FirstDate_Es_Mayor()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(+2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1111",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			bool control = _reporte.TransaccionDentroDelScope(transaccion1, DateTime.Now.AddMonths(+1), DateTime.Now.AddDays(+1));
			Assert.IsFalse(control);
		}

		[TestMethod]
		public void ReporteCategoriaPorMes_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(10);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ReporteCategoriaPorMes_No_Nulo()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 1,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(5);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ReporteGastosCategoriaPorMes_Dolares()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Ahorro _cuenta = new Ahorro
			{
				Moneda = TipoCambiario.Dolar,
			};
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 1,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
				//FechaTransaccion = DateTime.Today,
			};
			Cambio cambio = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 40,
			};
			_miEspacio.AgregarCambio(cambio);
			_miEspacio.AgregarTransaccion(transaccion);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(DateTime.Today.Month);
			Assert.IsTrue(toAnalize.First().MontoUsado == 40);
		}


		[TestMethod]
		public void ReporteCategoriaPorMes_Misma_CategoriaGasto()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(DateTime.Today.Month);
			double montoTotal = 10;
			CategoriaGasto cg = new CategoriaGasto
			{
				Categoria = _categoria,
				MontoUsado = 10,
				Porcentaje = (10 * 100) / montoTotal,
			};
			Assert.AreEqual(toAnalize.First(), cg);
		}

		[TestMethod]
		public void ReporteCategoriaPorMes_Cubre_100()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(DateTime.Today.Month);
			Assert.IsTrue(toAnalize.First().Porcentaje == 100);
		}

		[TestMethod]
		public void ReporteCategoriaPorMes_Cubre_50()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria);
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = _cuenta,
			};
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			List<CategoriaGasto> toAnalize = _reporte.ReporteGastosCategoriaPorMes(DateTime.Today.Month);
			Assert.IsTrue(toAnalize.First().Porcentaje == 50);
		}

		[TestMethod]
		public void ListadoGastos_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			_reporte.MiEspacio = _miEspacio;
			DateTime fIni = DateTime.Now;
			DateTime fEnd = new DateTime(fIni.Year, fIni.Month, 30);
			List<Transaccion> toAnalize = _reporte.ListadoGastos(_categoria, fIni, fEnd, _cuenta);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ListadoGastos_No_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoria);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion = new Transaccion
			{
				CategoriaTransaccion = _categoria,
				Monto = 1,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion);
			_reporte.MiEspacio = _miEspacio;
			DateTime fIni = new DateTime(2023, 9, 1);
			DateTime fEnd = new DateTime(2023, 12, 31);
			List<Transaccion> toAnalize = _reporte.ListadoGastos(_categoria, fIni, fEnd, _cuenta);
			Assert.IsTrue(toAnalize.Count != 0);
		}

		[TestMethod]
		public void ListadoGastos_Un_Elemento()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Categoria _categoria1 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			Categoria _categoria2 = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoria1);
			_miEspacio.AgregarCategoria(_categoria2);
			Cuenta _cuenta = new Cuenta();
			_miEspacio.AgregarCuenta(_cuenta);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoria1,
				Monto = 1,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba",
				CuentaMonetaria = _cuenta,
			};
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoria2,
				Monto = 1,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = _cuenta,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			DateTime fIni = new DateTime(2023, 9, 1);
			DateTime fEnd = new DateTime(2023, 12, 31);
			List<Transaccion> toAnalize = _reporte.ListadoGastos(_categoria1, fIni, fEnd, _cuenta);
			Assert.IsTrue(toAnalize.Count == 1);
		}

		[TestMethod]
		public void BalanceCuentas_Cero()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Ahorro ahorro = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Saldo = 1,
				Nombre = "Cuenta Ahorro",
			};
			_miEspacio.AgregarCuenta(ahorro);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			double balance = _reporte.BalanceCuentas(ahorro);
			Assert.IsTrue(balance == 1);
		}

		[TestMethod]
		public void BalanceCuenta_Devuelve_Bien_Pesos()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Ahorro ahorro = new Ahorro
			{
				Moneda = TipoCambiario.PesosUruguayos,
				Saldo = 1,
				Nombre = "Cuenta Ahorro",
			};
			double montoInicial = ahorro.Saldo;
			_miEspacio.AgregarCuenta(ahorro);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			_reporte.MiEspacio = _miEspacio;
			double balance = _reporte.BalanceCuentas(ahorro);
			Assert.IsTrue(balance == montoInicial);
		}

		[TestMethod]
		public void BalanceCuenta_Dolar_Sin_Cambio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Ahorro ahorro = new Ahorro
			{
				Moneda = TipoCambiario.Dolar,
				Saldo = 1,
				Nombre = "Cuenta Ahorro",
			};
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria 2",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Cambio cambio = new Cambio()
			{
				Moneda = TipoCambiario.Dolar,
				FechaDeCambio = DateTime.Today,
			};
			_miEspacio.AgregarCambio(cambio);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 10,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = ahorro,
			};
			_miEspacio.AgregarTransaccion(transaccion2);
			double montoInicial = ahorro.Saldo;
			_miEspacio.AgregarCuenta(ahorro);
			_reporte.MiEspacio = _miEspacio;
			double balance = _reporte.BalanceCuentas(ahorro);
			Console.WriteLine(balance);
			Assert.IsTrue(balance == montoInicial);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = new DateTime(DateTime.Now.Year, 12, 31),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count == 0);
		}

		[TestMethod]
		public void ReporteGastosTarjeta_No_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = new DateTime(DateTime.Now.Year, 12, 31),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);

			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count != 0);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioEspacioExcepcion))]
		public void ReporteGastosTarjeta_CuentaExpirada()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddMonths(-2),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			//Assert.IsTrue(toAnalize.First().CategoriaTransaccion == null);
		}

		[TestMethod]
		public void ReportegastosTarjeta_Cuenta_Por_Vencer()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			Transaccion transaccion2 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 2",
				CuentaMonetaria = credit,
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_miEspacio.AgregarTransaccion(transaccion2);

			_reporte.MiEspacio = _miEspacio;
			List<Transaccion> toAnalize = _reporte.ReporteGastosTarjeta(credit.NumeroTarjeta);
			Assert.IsTrue(toAnalize.Count == 2);
		}

		[TestMethod]
		public void TransaccionMismoYearYMes_Otro_Year()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_reporte.MiEspacio = _miEspacio;
			DateTime fecha = new DateTime(2024, DateTime.Now.Month, DateTime.Now.Day);
			Assert.IsTrue(!_reporte.TransaccionMismoAnioYMes(transaccion1, fecha));
		}

		[TestMethod]
		public void TransaccionMismoYearYMes_Otro_Mes()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Usuario _admin = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			_miEspacio.Admin = _admin;
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
			};
			_reporte.MiEspacio = _miEspacio;
			DateTime fecha = new DateTime(DateTime.Now.Year, 1, DateTime.Now.Day);
			Assert.IsTrue(!_reporte.TransaccionMismoAnioYMes(transaccion1, fecha));
		}

		[TestMethod]
		public void ReporteIngresosEgresos_No_Vacio()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(11);
			Assert.IsNotNull(ingresoEgreso);
		}

		[TestMethod]
		public void ReporteIngresosEgresos_Noviembre_Treinta_Datos()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(11);
			Assert.IsTrue(ingresoEgreso.Count == 30);
		}

		[TestMethod]
		public void ReporteIngresosEgresos_Transaccion_Pesos()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
				FechaTransaccion = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			double totalGastado = 10;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(DateTime.Today.Month);
			Assert.IsTrue(ingresoEgreso.First().Egresos == totalGastado);
		}

		[TestMethod]
		public void ReporteIngresosEgresos_Meses_Distintos()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaCosto = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Costo,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaCosto);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaCosto,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
				FechaTransaccion = new DateTime(DateTime.Today.Year, DateTime.Today.Month+1, 1)
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(DateTime.Today.Month);
			double totalGastado = 0;
			foreach (IngresoEgreso ie in ingresoEgreso)
			{
				totalGastado += ie.Egresos;
			}
			Assert.IsTrue(totalGastado == 0);
		}

		[TestMethod]
		public void ReporteIngresoEgreso_Solo_Ingreso()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.PesosUruguayos,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 10,
				Moneda = TipoCambiario.PesosUruguayos,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
				FechaTransaccion = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(DateTime.Today.Month);
			double totalIngresado = 0;
			foreach (IngresoEgreso ie in ingresoEgreso)
			{
				totalIngresado += ie.Ingresos;
			}
			Assert.IsTrue(totalIngresado == 10);
		}

		[TestMethod]
		public void ReporteIngresoEgreso_Ingreso_Euro()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.Euro,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Cambio euro = new Cambio
			{
				Pesos = 40,
				Moneda = TipoCambiario.Euro,
				FechaDeCambio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
			};
			_miEspacio.AgregarCambio(euro);
			Cambio euro2 = new Cambio
			{
				Pesos = 40,
				Moneda = TipoCambiario.Euro,
			};
			_miEspacio.AgregarCambio(euro2);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 1,
				Moneda = TipoCambiario.Euro,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
				FechaTransaccion = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(DateTime.Today.Month);
			double total = 0;
			foreach (IngresoEgreso ie in ingresoEgreso)
			{
				total += ie.Ingresos;
			}
			Assert.IsTrue(total == 40);
		}

		[TestMethod]
		public void ReporteIngresoEgreso_Ingreso_Dolar()
		{
			var _reporte = new Reporte();
			Espacio _miEspacio = new Espacio();
			Credito credit = new Credito
			{
				BancoEmisor = "Santander",
				Saldo = 1000,
				FechaCierre = DateTime.Now.AddDays(+5),
				Moneda = TipoCambiario.Euro,
				NumeroTarjeta = "1234",
			};
			_miEspacio.AgregarCuenta(credit);
			Categoria _categoriaIngreso = new Categoria
			{
				EstadoActivo = true,
				Tipo = TipoCategoria.Ingreso,
				Nombre = "Una categoria",
			};
			_miEspacio.AgregarCategoria(_categoriaIngreso);
			Cambio dolar = new Cambio
			{
				Pesos = 40,
				Moneda = TipoCambiario.Dolar,
				FechaDeCambio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1),
			};
			_miEspacio.AgregarCambio(dolar);
			Cambio dolar2 = new Cambio
			{
				Moneda = TipoCambiario.Dolar,
				Pesos = 33,
			};
			_miEspacio.AgregarCambio(dolar2);
			Transaccion transaccion1 = new Transaccion
			{
				CategoriaTransaccion = _categoriaIngreso,
				Monto = 1,
				Moneda = TipoCambiario.Dolar,
				Titulo = "Transaccion Prueba 1",
				CuentaMonetaria = credit,
				FechaTransaccion = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
			};
			_miEspacio.AgregarTransaccion(transaccion1);
			_reporte.MiEspacio = _miEspacio;
			List<IngresoEgreso> ingresoEgreso = _reporte.ReporteIngresosEgresos(DateTime.Today.Month);
			double total = 0;
			foreach (IngresoEgreso ie in ingresoEgreso)
			{
				total += ie.Ingresos;
			}
			Assert.IsTrue(total == 40);
		}
	}
}