﻿using Domain;
namespace DomainTest
{
	[TestClass]
	public class AhorroTest
	{
		private Ahorro cuentaAhorro;

		[TestInitialize]
		public void Inicializar()
		{
			cuentaAhorro = new Ahorro();
		}

		[TestMethod]
		public void Nueva_Cuenta_Ahorro_No_Nula()
		{
			Assert.IsNotNull(cuentaAhorro);
		}

		[TestMethod]
		public void CuentaAhorro_Tiene_Nombre()
		{
			string nombre = "CuentaAhorroPrueba";
			cuentaAhorro.Nombre = nombre;
			Assert.AreEqual(nombre, cuentaAhorro.Nombre);
		}

		[TestMethod]
		[ExpectedException(typeof(DomainCuentaException))]
		public void Excepcion_CuentaAhorro_Tiene_Nombre_Vacio()
		{
			string nombre = "";
			cuentaAhorro.Nombre = nombre;
		}

		[TestMethod]
		public void Cuenta_Tiene_Monto_Decimal() { 
			double monto = 100.01;
			cuentaAhorro.Monto = monto;
			Assert.AreEqual(monto, cuentaAhorro.Monto);
		}
	}
}