﻿using DTO.EnumsDTO;

namespace DTO
{
	public class CambioDTO
	{
		public int Id { get; set; }
		public TipoCambiarioDTO Moneda { get; set; }
		public DateTime FechaDeCambio { get; set; }
		public double Pesos { get; set; }
	}
}
