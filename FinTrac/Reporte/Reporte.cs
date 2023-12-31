﻿using Dominio;

namespace EspacioReporte
{
	public class Reporte
	{
		private Espacio _MiEspacio;

		public Reporte()
		{

		}
		
		public Reporte(Espacio espacio) 
		{
			MiEspacio = espacio;
		}

		public Espacio MiEspacio { get { return _MiEspacio; } set { _MiEspacio = value; } }

		public List<ObjetivoGasto> ReporteObjetivosDeGastos()
		{
			List<ObjetivoGasto> ret = new List<ObjetivoGasto>();
			List<Objetivo> objetivos = MiEspacio.Objetivos;
			DateTime fechaActual = DateTime.Now;
			foreach (Objetivo o in objetivos)
			{
				List<Transaccion> transacciones = MiEspacio.Transacciones;
				ObjetivoGasto objetivo = new ObjetivoGasto(o.MontoMaximo);
				objetivo.Objetivo = o;
				double _montoAcumulado = 0;
				bool transaccionesAceptadas = false;
				foreach (Transaccion t in transacciones)
				{
					Cambio cambio = new Cambio();
					if (t.Moneda != TipoCambiario.PesosUruguayos)
					{
						cambio = t.EncontrarCambio(MiEspacio);
					}
					if (TransaccionEntraObjetivo(o, t) && TransaccionMismoAnioYMes(t, fechaActual) && TransaccionCategoriaCosto(t))
					{
						transaccionesAceptadas = true;
						if (t.Moneda != TipoCambiario.PesosUruguayos)
						{
							_montoAcumulado += t.Monto * cambio.Pesos;
						}
						else
						{
							_montoAcumulado += t.Monto;
						}
					}
				}
				if (transaccionesAceptadas)
				{
					objetivo.MontoAcumulado = _montoAcumulado;
					ret.Add(objetivo);
				}
			}
			return ret;
		}

		private bool TransaccionEntraObjetivo(Objetivo o, Transaccion t)
		{
			return (o.Categorias.Contains(t.CategoriaTransaccion));
		}

		public bool TransaccionMismoAnioYMes(Transaccion t, DateTime fecha)
		{
			return (t.FechaTransaccion.Month.Equals(fecha.Month)) && (t.FechaTransaccion.Year.Equals(fecha.Year));
		}

		private bool TransaccionCategoriaCosto(Transaccion t)
		{
			return (t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Costo));
		}

		public List<CategoriaGasto> ReporteGastosCategoriaPorMes(int mes)
		{
			List<CategoriaGasto> _reporteGastos = new List<CategoriaGasto>();
			List<Categoria> _categorias = MiEspacio.Categorias;
			double _montoTotal = Calcular_MontoTotal(mes);
			foreach (Categoria c in _categorias)
			{
				double _montoAcumulado = 0;
				List<Transaccion> transacciones = MiEspacio.Transacciones;
				bool transaccionAceptada = false;
				foreach (Transaccion t in transacciones)
				{
					Cambio cambio = new Cambio();
					if (t.Moneda != TipoCambiario.PesosUruguayos)
					{
						cambio = t.EncontrarCambio(MiEspacio);
					}
					if (TransaccionMismaCategoria(c, t) && TransaccionMismoMes(t, mes) && TransaccionCategoriaCosto(t))
					{
						transaccionAceptada = true;
						if (t.Moneda != TipoCambiario.PesosUruguayos)
						{
							_montoAcumulado += t.Monto * cambio.Pesos;
						}
						else
						{
							_montoAcumulado += t.Monto;
						}
					}
				}
				if (transaccionAceptada)
				{
					double _porcentaje = (_montoAcumulado * 100) / _montoTotal;
					CategoriaGasto cg = new CategoriaGasto(c, _montoAcumulado, _porcentaje);
					_reporteGastos.Add(cg);
				}
			}
			return _reporteGastos;
		}

		private bool TransaccionMismaCategoria(Categoria c, Transaccion t)
		{
			return (t.CategoriaTransaccion.Equals(c));
		}

		private bool TransaccionMismoMes(Transaccion t, int mes)
		{
			return (t.FechaTransaccion.Month.Equals(mes));
		}

		public double Calcular_MontoTotal(int mes)
		{
			double montoTotal = 0;
			List<Transaccion> _listTran = MiEspacio.Transacciones;
			foreach (Transaccion t in _listTran)
			{
				Cambio cambio = new Cambio();
				if (TransaccionMismoMes(t, mes) && TransaccionCategoriaCosto(t))
				{
					if (t.Moneda != TipoCambiario.PesosUruguayos)
					{
						cambio = t.EncontrarCambio(MiEspacio);
						montoTotal += t.Monto * cambio.Pesos;
					}
					else
					{
						montoTotal += t.Monto;
					}
				}
			}
			return montoTotal;
		}

		public List<Transaccion> ListadoGastos(Categoria _catFiltro, DateTime _fechaIni, DateTime _fechaFin, Cuenta _cuentaFiltro)
		{
			List<Transaccion> _transacciones = MiEspacio.Transacciones;
			List<Transaccion> _listaGastos = new List<Transaccion>();
			foreach (Transaccion t in _transacciones)
			{
				if (CumpleFiltroCategoria(t.CategoriaTransaccion, _catFiltro) && (TransaccionDentroDelScope(t, _fechaIni, _fechaFin)) && MismaCuenta(t.CuentaMonetaria, _cuentaFiltro))
				{
					_listaGastos.Add(t);
				}
			}
			return _listaGastos;
		}

		private bool CumpleFiltroCategoria(Categoria _catFiltro, Categoria _catTrans)
		{
			return _catTrans.Equals(_catFiltro) && _catTrans.Tipo.Equals(_catFiltro.Tipo);
		}

		private bool MismaCuenta(Cuenta _cuenta1, Cuenta _cuenta2)
		{
			return _cuenta1.Equals(_cuenta2);
		}

		public List<Transaccion> ReporteGastosTarjeta(string _nroTarjeta)
		{
			List<Transaccion> _reporteGastos = new List<Transaccion>();
			List<Cuenta> cuentas = MiEspacio.Cuentas;
			DateTime actualDate = DateTime.Now;
			foreach (Cuenta c in cuentas)
			{
				if (c is Credito)
				{
					Credito creditAccount = (Credito)c;
					if (creditAccount.NumeroTarjeta.Equals(_nroTarjeta))
					{
						List<Transaccion> listaTrans = MiEspacio.Transacciones;
						foreach (Transaccion t in listaTrans)
						{
							if ((t.CuentaMonetaria is Credito) && MismaCuenta((Credito)t.CuentaMonetaria, creditAccount) && TransaccionCategoriaCosto(t))
							{
								if (!CuentaVencida(creditAccount, actualDate))
								{
									DateTime firstDate;
									DateTime lastDate;
									if (CuentaVenceEseMes(creditAccount, actualDate))
									{
										DateTime fechaPrueba = new DateTime(2023, 10, 30);
										firstDate = new DateTime(creditAccount.FechaCierre.Year, creditAccount.FechaCierre.Month - 1, creditAccount.FechaCierre.Day + 1);
										lastDate = creditAccount.FechaCierre;
										if (TransaccionDentroDelScope(t, firstDate, lastDate))
										{
											_reporteGastos.Add(t);
										}
									}
									else
									{
										firstDate = new DateTime(actualDate.Year, actualDate.Month, 1);
										lastDate = firstDate.AddMonths(1);
										//lastDate = lastDate.AddDays(-1);
										if (TransaccionDentroDelScope(t, firstDate, lastDate))
										{
											_reporteGastos.Add(t);
										}
									}
								}
							}
						}
					}
				}
			}
			return _reporteGastos;
		}

		private bool CuentaVenceEseMes(Credito CA, DateTime actualDate)
		{
			DateTime fechaCierre = CA.FechaCierre;
			return fechaCierre.Month == actualDate.Month;
		}

		private bool CuentaVencida(Credito CA, DateTime actualDate)
		{
			DateTime fechaCierre = CA.FechaCierre;
			return fechaCierre < actualDate;
		}

		public bool TransaccionDentroDelScope(Transaccion t, DateTime firstDate, DateTime lastDate)
		{
			DateTime fechaTransaccion = t.FechaTransaccion;
			return (firstDate <= fechaTransaccion) && (fechaTransaccion <= lastDate);
		}

		public double BalanceCuentas(Ahorro account)
		{
			Cambio cambioUtilizado = BuscarCambioActual(account.FechaCreacion, account.Moneda);
			double saldoCuenta;

			if (account.Moneda.Equals(TipoCambiario.PesosUruguayos))
			{
				saldoCuenta = EncontrarMontoInicial(account, cambioUtilizado) + SumatoriaIngresos(account) - SumatoriaCostos(account);
			}
			else
			{
				saldoCuenta = EncontrarMontoInicial(account, cambioUtilizado) + SumatoriaIngresos(account) - SumatoriaCostos(account);
			}
			return saldoCuenta;
		}

		private double EncontrarMontoInicial(Ahorro account, Cambio cambioUtilizado)
		{
			if (account.Moneda.Equals(TipoCambiario.PesosUruguayos))
			{
				return account.Saldo + SumatoriaCostos(account) - SumatoriaIngresos(account);
			}
			else
			{
				if (cambioUtilizado.Pesos == 0)
				{
					return (account.Saldo) + SumatoriaCostos(account) - SumatoriaIngresos(account);
				}
				else
				{
					return (account.Saldo * cambioUtilizado.Pesos) + SumatoriaCostos(account) - SumatoriaIngresos(account);
				}
			}

		}

		public double SumatoriaIngresos(Ahorro account)
		{
			double _montoIngresos = 0;
			List<Transaccion> transacciones = MiEspacio.Transacciones;
			foreach (Transaccion t in transacciones)
			{
				if (t.CuentaMonetaria is Ahorro)
				{
					Cambio cambioTransaccion = t.EncontrarCambio(_MiEspacio);
					if (TransaccionCategoriaIngreso(t) && MismaCuentaAhorro(account, (Ahorro)t.CuentaMonetaria))
					{
						if (t.Moneda != TipoCambiario.PesosUruguayos)
						{
							if (cambioTransaccion.Pesos == 0)
							{
								_montoIngresos += t.Monto;
							}
							else
							{
								_montoIngresos += t.Monto * cambioTransaccion.Pesos;
							}
						}
						else
						{
							_montoIngresos += t.Monto;
						}
					}
				}
			}
			return _montoIngresos;
		}

		private bool TransaccionCategoriaIngreso(Transaccion t)
		{
			return t.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Ingreso);
		}

		private bool MismaCuentaAhorro(Ahorro c1, Ahorro c2)
		{
			return c1.Equals(c2);
		}

		public double SumatoriaCostos(Ahorro account)
		{
			double _montoCostos = 0;
			List<Transaccion> transacciones = MiEspacio.Transacciones;
			foreach (Transaccion t in transacciones)
			{
				if (t.CuentaMonetaria is Ahorro)
				{
					Cambio cambioTransaccion = t.EncontrarCambio(_MiEspacio);
					if (TransaccionCategoriaCosto(t) && MismaCuentaAhorro(account, (Ahorro)t.CuentaMonetaria))
					{
						if (t.Moneda != TipoCambiario.PesosUruguayos)
						{
							if (cambioTransaccion.Pesos == 0)
							{
								_montoCostos += t.Monto;
							}
							else
							{
								_montoCostos += t.Monto * cambioTransaccion.Pesos;
							}
						}
						else
						{
							_montoCostos += t.Monto;
						}
					}
				}
			}
			return _montoCostos;
		}

		public Cambio BuscarCambioActual(DateTime fecha, TipoCambiario moneda)
		{
			Cambio cambioRet = new Cambio();
			foreach (Cambio cambio in MiEspacio.Cambios)
			{
				if (moneda.Equals(cambio.Moneda) && cambio.FechaDeCambio.Day == fecha.Day && cambio.FechaDeCambio.Month == fecha.Month && cambio.FechaDeCambio.Year == fecha.Year)
				{
					cambioRet = cambio;
				}
			}
			return cambioRet;
		}

		public List<IngresoEgreso> ReporteIngresosEgresos(int mes)
		{
			List<IngresoEgreso> reporteIngresosEgresos = new List<IngresoEgreso> ();
			int ultimoDiaMes = new DateTime(DateTime.Today.Year, mes, 1).AddMonths(1).AddDays(-1).Day;
			for (int dia=1; dia<=ultimoDiaMes; dia++)
			{
				double ingresos = 0;
				double egresos = 0;
				foreach (Transaccion transaccion in MiEspacio.Transacciones)
				{
					if (transaccion.FechaTransaccion.Month == mes && transaccion.FechaTransaccion.Day == dia)
					{
						if (transaccion.CategoriaTransaccion.Tipo.Equals(TipoCategoria.Ingreso))
						{
							ingresos += DarValorEnPesos(transaccion);
						} else
						{
							egresos += DarValorEnPesos(transaccion);
						}
					}
				}
				DateTime fecha = new DateTime(DateTime.Today.Year, mes, dia);
				IngresoEgreso ingresoEgreso = new IngresoEgreso (fecha, ingresos, egresos);
				reporteIngresosEgresos.Add(ingresoEgreso);
			}
			return reporteIngresosEgresos;
		}

		private double DarValorEnPesos(Transaccion transaccion)
		{
			Cambio cambioUsado = transaccion.EncontrarCambio(MiEspacio);
			if (transaccion.Moneda.Equals(TipoCambiario.Dolar) || transaccion.Moneda.Equals(TipoCambiario.Euro))
			{
				return cambioUsado.Pesos * transaccion.Monto;
			} else
			{
				return transaccion.Monto;
			}
		}

	}
}
