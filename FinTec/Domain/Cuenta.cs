﻿namespace Domain
{
	public enum TipoCambiario
	{
		PesosUruguayos,
		Dolar
	}
	public class Cuenta
	{
		public TipoCambiario Moneda { get; set; } 
		private readonly DateTime _fechaCreacion = DateTime.Now;
		public DateTime FechaCreacion
		{
			get { return _fechaCreacion; }
		}
		public Cuenta()
		{
		}
	}
}