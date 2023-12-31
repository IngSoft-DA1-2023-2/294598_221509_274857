﻿using Excepcion;

namespace Dominio
{
	public class Cambio
	{
		public int Id { get; set; }
		public int EspacioId { get; set; }
		public Espacio Espacio { get; set; }
		private DateTime _fechaDeCambio = DateTime.Now.Date;
		private double _pesos;
		public DateTime FechaDeCambio
		{
			get { return _fechaDeCambio; }
			set { _fechaDeCambio = value; }
		}
		public TipoCambiario Moneda { get; set; }
		public double Pesos
		{
			get
			{
				return _pesos;
			}
			set
			{
				if (value <= 0)
					throw new DominioEspacioExcepcion("El monto en pesos uruguayos debe ser mayor a 0");
				_pesos = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			Cambio cambio = (Cambio)obj;
			return FechaDeCambio.Date == cambio.FechaDeCambio.Date && Moneda == cambio.Moneda;
		}
	}
}
