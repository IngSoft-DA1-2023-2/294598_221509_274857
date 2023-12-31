﻿using Dominio;
using Excepcion;

namespace DominioTest
{
	[TestClass]
	public class UsuarioTest
	{
		[TestMethod]
		public void Nuevo_Usuario()
		{
			var usuario = new Usuario();
			Assert.IsNotNull(usuario);
		}

		[TestMethod]

		public void Contrasena_Minimo_Diez()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Contrasena = "1234567890A";
			string contrasena = unUsuario.Contrasena;
			bool resultado = unUsuario.Validar_Contrasena(contrasena);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]

		public void Contrasena_Maximo_Treinta()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Contrasena = "1234567890123456789012345678901";
			string contrasena = unUsuario.Contrasena;
			bool resultado = unUsuario.Validar_Contrasena(contrasena);
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]

		public void Contrasena_Contiene_Mayuscula()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Contrasena = "contrasena";
			string contrasena = unUsuario.Contrasena;
			bool resultado = unUsuario.Validar_Contrasena(contrasena);
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		public void Validar_Correo_Contiene_Arroba()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Correo = "usfhu@dicsdc.com";
			string correo = unUsuario.Correo;
			bool resultado = unUsuario.Validar_Correo(correo);
			Assert.IsTrue(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Validar_Correo_No_Coincide_PuntoCom()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Correo = "usfhud@icsdc.comfwef";
			string correo = unUsuario.Correo;
			bool resultado = unUsuario.Validar_Correo(correo);
			Assert.IsFalse(resultado);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Nombre_Usuario_Vacio()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Nombre = "";
		}

		[TestMethod]
		public void Obtener_Usuario()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Nombre = "Maxi";
			Assert.AreEqual("Maxi", unUsuario.Nombre);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Apellido_Usuario_Vacio()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Apellido = "";
		}

		[TestMethod]
		public void Obtener_Usuario_Apellido()
		{
			Usuario unUsuario = new Usuario();
			unUsuario.Apellido = "Gimenez";
			Assert.AreEqual("Gimenez", unUsuario.Apellido);
		}
		[TestMethod]
		public void Usuario_Equals_Null()
		{
			Usuario unUsuario = new Usuario
			{
				Contrasena = "T5r1234567890",
				Correo = "usfhud@icsdc.com",
			};
			Object objeto = null;
			var objeto2 = new Object();
			Assert.IsFalse(unUsuario.Equals(objeto));
			Assert.IsFalse(unUsuario.Equals(objeto2));
			Assert.IsTrue(unUsuario.Equals(unUsuario));
		}

		[TestMethod]
		public void Usuario_Equals_Diferentes()
		{
			Usuario user1 = new Usuario
			{
				Contrasena = "1234567890Tyyy",
				Correo = "usfhud@icsdc.com",
			};
			Usuario user2 = new Usuario
			{
				Contrasena = "1234567890Tyyy",
				Correo = "12345@icsdc.com",
			};
			Assert.IsFalse(user1.Equals(user2));
		}

		[TestMethod]
		public void Usuario_Equals_Iguales()
		{
			Usuario usuario1 = new Usuario
			{
				Contrasena = "1234567890Yuu",
				Correo = "mateo@gmail.com",
			};
			Usuario usuario2 = new Usuario()
			{
				Contrasena = "234567891Yuu",
				Correo = "mateo@gmail.com",
			};
			Assert.IsTrue(usuario1.Equals(usuario2));

		}

		[TestMethod]
		public void Usuario_Tiene_Direccion()
		{
			Usuario usuario = new Usuario();
			usuario.Direccion = "direccion";
			Assert.AreEqual(usuario.Direccion, "direccion");
		}

		[TestMethod]
		public void Usuario_Tiene_idEspacioPrincipal()
		{
			Usuario usuario = new Usuario();
			usuario.IdEspacioPrincipal = 1;
			Assert.AreEqual(usuario.IdEspacioPrincipal, 1);
		}

		[TestMethod]
		public void Usuario_Tiene_Id()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "correo@gmail.com",
				Contrasena = "HOLAhola123",
				Nombre = "test",
				Apellido = "test",
				Direccion = "test"
			};
			usuario.Id = 1;
			Assert.AreEqual(usuario.Id, 1);
		}

		[TestMethod]
		public void Usuario_Tiene_Espacios()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "correo@gmail.com",
				Contrasena = "HOLAhola123",
				Nombre = "test",
				Apellido = "test",
				Direccion = "test"
			};

			Usuario admin = new Usuario()
			{
				Correo = "admin@gmail.com",
				Contrasena = "HOLAhola123",
				Nombre = "test",
				Apellido = "test",
				Direccion = "test"
			};

			Espacio espacio = new Espacio()
			{
				Nombre = "test",
				Admin = admin
			};

			List<Espacio> espacios = new List<Espacio>();
			espacios.Add(espacio);
			usuario.Espacios = espacios;
			Assert.AreEqual(usuario.Espacios, espacios);
			Assert.AreEqual(usuario.Espacios.Count, 1);
		}

		[TestMethod]
		public void Usuario_Tiene_EspaciosAdmin()
		{
			Usuario usuario = new Usuario()
			{
				Correo = "correo@gmail.com",
				Contrasena = "HOLAhola123",
				Nombre = "test",
				Apellido = "test",
				Direccion = "test"
			};

			Espacio espacio = new Espacio()
			{
				Nombre = "test",
				Admin = usuario
			};
			List<Espacio> espacios = new List<Espacio>();
			espacios.Add(espacio);
			usuario.EspaciosAdmin = espacios;
			Assert.AreEqual(usuario.EspaciosAdmin, espacios);
			Assert.AreEqual(usuario.EspaciosAdmin.Count, 1);
		}

		[TestMethod]
		[ExpectedException(typeof(DominioUsuarioExcepcion))]
		public void Excepcion_Al_Asignar_Correo_Null()
		{
			Usuario usuario = new Usuario()
			{
				Correo = null,
				Contrasena = "HOLAhola123",
				Nombre = "test",
				Apellido = "test",

			};
		}
	}
}

