﻿using LogicaNegocio;
using DTO;
using DTO.EnumsDTO;
using Dominio;
using Excepcion;
using EspacioReporte;
using System;

namespace Controlador
{
	public class ControladorReporte
	{
		private EspacioLogica _reporte;

		public ControladorReporte(EspacioLogica reporte)
		{
			_reporte = reporte;
		}

		public List<ObjetivoGastoDTO> ReporteObjetivosGastos(int id)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			List<ObjetivoGasto> reporteObjetivoGasto = reporte.ReporteObjetivosDeGastos();
			List<ObjetivoGastoDTO> reporteEnDTO = new List<ObjetivoGastoDTO>();
			foreach (ObjetivoGasto objetivoGasto in reporteObjetivoGasto)
			{
				ObjetivoGastoDTO obj = new ObjetivoGastoDTO()
				{
					MontoAcumulado = objetivoGasto.MontoAcumulado,
					MontoEsperado = objetivoGasto.MontoEsperado,
					Objetivo = new ObjetivoDTO()
					{
						Categorias = CategoriasA_DTO(objetivoGasto.Objetivo.Categorias),
						Id = objetivoGasto.Objetivo.Id,
						MontoMaximo = objetivoGasto.Objetivo.MontoMaximo,
						Titulo = objetivoGasto.Objetivo.Titulo,
						Token = objetivoGasto.Objetivo.Token,
					}
				};
				reporteEnDTO.Add(obj);
			}
			return reporteEnDTO;
		}

		public List<CategoriaGastoDTO> ReporteCategroriaPorMes(int id, int mes)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			List<CategoriaGasto> reporteObjetivoGasto = reporte.ReporteGastosCategoriaPorMes(mes);
			List<CategoriaGastoDTO> reporteEnDTO = new List<CategoriaGastoDTO>();
			foreach (CategoriaGasto categoriaGasto in reporteObjetivoGasto)
			{
				CategoriaGastoDTO categoriaGastoDTO = new CategoriaGastoDTO()
				{
					Categoria = new CategoriaDTO()
					{
						EstadoActivo = categoriaGasto.Categoria.EstadoActivo,
						FechaCreacion = categoriaGasto.Categoria.FechaCreacion,
						Id = categoriaGasto.Categoria.Id,
						Nombre = categoriaGasto.Categoria.Nombre,
						Tipo = TipoCategoriaDTO.Costo,
					},
					MontoUsado = categoriaGasto.MontoUsado,
					Porcentaje = categoriaGasto.Porcentaje,
				};
				reporteEnDTO.Add(categoriaGastoDTO);
			}
			return reporteEnDTO;
		}

		public List<TransaccionDTO> ReporteDeGastos(int id, CategoriaDTO _catFiltro, DateTime _fechaIni, DateTime _fechaFin, CuentaDTO _cuentaFiltro)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			Categoria categoria = CategoriaDTO_A_Categoria(_catFiltro);
			Cuenta cuenta;
			if (_cuentaFiltro is CreditoDTO)
			{
				cuenta = CreditoDTO_A_Credito((CreditoDTO)_cuentaFiltro);
			}
			else
			{
				cuenta = AhorroDTO_A_Ahorro((AhorroDTO)_cuentaFiltro);
			}
			List<Transaccion> reporteDeGastos = reporte.ListadoGastos(categoria, _fechaIni, _fechaFin, cuenta);
			List<TransaccionDTO> transaccionDTOs = new List<TransaccionDTO>();
			foreach (Transaccion transaccion in reporteDeGastos)
			{
				TransaccionDTO transaccionDTO;
				Cuenta CuentaTransaccion;
				if (transaccion.CuentaMonetaria is Credito)
				{
					Credito CuentaAuxiliar = (Credito)transaccion.CuentaMonetaria;
					if (transaccion.Moneda.Equals(TipoCambiario.Dolar))
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.BancoEmisor,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.Dolar,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
					else if (transaccion.Moneda.Equals(TipoCambiario.Euro))
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.BancoEmisor,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.Euro,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
					else
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.BancoEmisor,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.PesosUruguayos,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
				} else
				{
					Ahorro CuentaAuxiliar = (Ahorro)transaccion.CuentaMonetaria;
					if (transaccion.Moneda.Equals(TipoCambiario.Dolar))
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.Nombre,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.Dolar,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
					else if (transaccion.Moneda.Equals(TipoCambiario.Euro))
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.Nombre,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.Euro,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
					else
					{
						transaccionDTO = new TransaccionDTO()
						{
							CategoriaTransaccion = "Costo",
							CuentaMonetaria = CuentaAuxiliar.Nombre,
							FechaTransaccion = transaccion.FechaTransaccion,
							Id = transaccion.Id,
							Moneda = TipoCambiarioDTO.PesosUruguayos,
							Monto = transaccion.Monto,
							Titulo = transaccion.Titulo,
						};
					}
				}
				transaccionDTOs.Add(transaccionDTO);
			}
			return transaccionDTOs;
		}

		public List<TransaccionDTO> ReporteGastosTarjeta(int id, string nroTarjeta)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			List<Transaccion> reporteTransacciones = reporte.ReporteGastosTarjeta(nroTarjeta);
			List<TransaccionDTO> transaccionesDTO = new List<TransaccionDTO>();
			foreach (Transaccion transaccion in reporteTransacciones)
			{
				TransaccionDTO transaccionDTO;
				Credito CuentaTransaccion = (Credito)transaccion.CuentaMonetaria;
				if (transaccion.Moneda.Equals(TipoCambiario.Dolar))
				{
					transaccionDTO = new TransaccionDTO()
					{
						CuentaMonetaria = CuentaTransaccion.BancoEmisor,
						FechaTransaccion = transaccion.FechaTransaccion,
						Id = transaccion.Id,
						Moneda = TipoCambiarioDTO.Dolar,
						Monto = transaccion.Monto,
						Titulo = transaccion.Titulo,
					};
				}
				else if (transaccion.Moneda.Equals(TipoCambiario.Euro))
				{
					transaccionDTO = new TransaccionDTO()
					{
						CategoriaTransaccion = "Costo",
						CuentaMonetaria = CuentaTransaccion.BancoEmisor,
						FechaTransaccion = transaccion.FechaTransaccion,
						Id = transaccion.Id,
						Moneda = TipoCambiarioDTO.Euro,
						Monto = transaccion.Monto,
						Titulo = transaccion.Titulo,
					};
				}
				else
				{
					transaccionDTO = new TransaccionDTO()
					{
						CategoriaTransaccion = "Costo",
						CuentaMonetaria = CuentaTransaccion.BancoEmisor,
						FechaTransaccion = transaccion.FechaTransaccion,
						Id = transaccion.Id,
						Moneda = TipoCambiarioDTO.PesosUruguayos,
						Monto = transaccion.Monto,
						Titulo = transaccion.Titulo,
					};
				}
				transaccionesDTO.Add(transaccionDTO);
			}
			return transaccionesDTO;
		}

		public double BalanceDeCuentas(int id, AhorroDTO account)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			Ahorro accountNormal = AhorroDTO_A_Ahorro(account);
			double balance = reporte.BalanceCuentas(accountNormal);
			return balance;
		}

		public List<IngresoEgresoDTO> ReporteIngresosEgresos(int id, int mes)
		{
			Espacio espacio = _reporte.EncontrarEspacio(id);
			Reporte reporte = new Reporte(espacio);
			List<IngresoEgreso> reporteIngresoEgreso = reporte.ReporteIngresosEgresos(mes);
			List<IngresoEgresoDTO> reporteEnDTO = new List<IngresoEgresoDTO>();
			foreach (IngresoEgreso ingresoEgreso in reporteIngresoEgreso)
			{
				IngresoEgresoDTO ingresoEgresoDTO = new IngresoEgresoDTO()
				{
					Egresos = ingresoEgreso.Egresos,
					Ingresos = ingresoEgreso.Ingresos,
					Fecha = ingresoEgreso.Fecha,
				};
				reporteEnDTO.Add(ingresoEgresoDTO);
			}
			return reporteEnDTO;
		}


		private Ahorro AhorroDTO_A_Ahorro(AhorroDTO account)
		{
			Ahorro cuenta;
			if (account.Moneda.Equals(TipoCambiarioDTO.Euro))
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.Euro,
					Saldo = account.Saldo,
					Nombre = account.Nombre,
				};
			}
			else if (account.Moneda.Equals(TipoCambiarioDTO.Dolar))
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.Dolar,
					Saldo = account.Saldo,
					Nombre = account.Nombre,
				};
			}
			else
			{
				cuenta = new Ahorro()
				{
					FechaCreacion = account.FechaCreacion,
					Moneda = TipoCambiario.PesosUruguayos,
					Saldo = account.Saldo,
					Nombre = account.Nombre,
				};
			}
			return cuenta;
		}

		private Credito CreditoDTO_A_Credito(CreditoDTO credit)
		{
			Credito cuenta;
			if (credit.Moneda.Equals(TipoCambiarioDTO.Euro))
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					Saldo = credit.Saldo,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.Euro,
				};
			}
			else if (credit.Moneda.Equals(TipoCambiarioDTO.Dolar))
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					Saldo = credit.Saldo,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.Dolar,
				};
			}
			else
			{
				cuenta = new Credito()
				{
					NumeroTarjeta = credit.NumeroTarjeta,
					BancoEmisor = credit.BancoEmisor,
					Saldo = credit.Saldo,
					FechaCierre = credit.FechaCierre,
					Moneda = TipoCambiario.PesosUruguayos,
				};
			}
			return cuenta;
		}

		private List<CategoriaDTO> CategoriasA_DTO(List<Categoria> categorias)
		{
			List<CategoriaDTO> categoriaDTOs = new List<CategoriaDTO>();
			foreach (Categoria cat in categorias)
			{
				CategoriaDTO categoria = new CategoriaDTO()
				{
					Id = cat.Id,
					EstadoActivo = cat.EstadoActivo,
					FechaCreacion = cat.FechaCreacion,
					Nombre = cat.Nombre,
					Tipo = TipoCategoriaDTO.Costo,
				};
				categoriaDTOs.Add(categoria);
			}
			return categoriaDTOs;
		}

		private Categoria CategoriaDTO_A_Categoria(CategoriaDTO categoria)
		{
			Categoria categoriaRetornar;
			categoriaRetornar = new Categoria()
			{
				EstadoActivo = categoria.EstadoActivo,
				FechaCreacion = categoria.FechaCreacion,
				Id = categoria.Id,
				Nombre = categoria.Nombre,
				Tipo = TipoCategoria.Costo,
			};
			return categoriaRetornar;
		}

	}
}
