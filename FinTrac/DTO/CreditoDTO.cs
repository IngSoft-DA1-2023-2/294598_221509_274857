﻿namespace DTO
{
	public class CreditoDTO : CuentaDTO
	{
		public string BancoEmisor { get; set; }
		public string NumeroTarjeta { get; set; }
		public DateTime FechaCierre { get; set; }
	}
}
