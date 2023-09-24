﻿using BussinesLogic;

namespace BussinesLogicTest
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
            unUsuario.Contrasena = "1234567890";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Contrasena_Maximo_Treinta()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Contrasena = "1234567890123456789012345678901";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsFalse(resultado);
        }
        [TestMethod]
        public void Contrasena_Contiene_Mayuscula()
        {
            Usuario unUsuario = new Usuario();
            unUsuario.Contrasena = "contrasena";
            string contrasena = unUsuario.Contrasena;
            bool resultado = unUsuario.Validar_Contrasena(contrasena);
            Assert.IsFalse(resultado);
        }
    }
}
