﻿using DTO;
using DTO.EnumsDTO;

namespace DTOTest
{
	[TestClass]
	public class TransaccionDTOTest
	{
		[TestMethod]
		public void TransaccionDTO_Tiene_Id()
		{
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Id = 1;
			Assert.AreEqual(1, transaccionDTO.Id);
		}

		[TestMethod]
		public void TransaccionDTO_Tiene_Titulo()
		{
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Titulo = "Titulo";
			Assert.AreEqual("Titulo", transaccionDTO.Titulo);
		}

		[TestMethod]
		public void TransaccionDTO_Tiene_Monto()
		{ 
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Monto = 1;
			Assert.AreEqual(1, transaccionDTO.Monto);
		}

		[TestMethod]
		public void TransaccionDTO_Tiene_Moneda()
		{
			TransaccionDTO transaccionDTO = new TransaccionDTO();
			transaccionDTO.Moneda = TipoCambiarioDTO.Euro;
			TransaccionDTO transaccionDTO2 = new TransaccionDTO();
			transaccionDTO2.Moneda = TipoCambiarioDTO.Dolar;
			TransaccionDTO transaccionDTO3	 = new TransaccionDTO();
			transaccionDTO3.Moneda = TipoCambiarioDTO.PesosUruguayos;
			Assert.AreEqual(TipoCambiarioDTO.PesosUruguayos, transaccionDTO3.Moneda);
			Assert.AreEqual(TipoCambiarioDTO.Dolar, transaccionDTO2.Moneda);
			Assert.AreEqual(TipoCambiarioDTO.Euro, transaccionDTO.Moneda);
		}
	}
}
