﻿using DTO;
using DTO.EnumsDTO;
namespace DTOTest
{
	[TestClass]
	public class TransaccionIngresoDTOTest
	{
		[TestMethod]
		public void TransaccionIngreso_No_Es_Null()
		{
			TransaccionDTO transaccionDTO = new TransaccionIngresoDTO();
			Assert.IsNotNull(transaccionDTO);
		}
	}
}