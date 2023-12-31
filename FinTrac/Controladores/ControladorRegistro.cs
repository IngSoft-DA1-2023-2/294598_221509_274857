﻿using LogicaNegocio;
using Excepcion;
using Dominio;
using DTO;

namespace Controlador
{
	public class ControladorRegistro
	{
		private UsuarioLogica _usuarioLogic;
		private EspacioLogica _espacioLogic;

		public ControladorRegistro(UsuarioLogica usuarioLogic, EspacioLogica espacioLogic)
		{
			_usuarioLogic = usuarioLogic;
			_espacioLogic = espacioLogic;
		}

		public void CrearEspacioPrincipal(string correo)
		{
			Usuario admin = _usuarioLogic.EncontrarUsuario(correo);
			string nombre = "Principal " + admin.Nombre;
			_espacioLogic.CrearEspacio(nombre, admin);
		}

		public string RegistrarUsuario(UsuarioDTO usuarioDTO)
		{
			string msjError = "";
			int idEspacioPrincipal = _espacioLogic.EspacioMayorId() + 1;
			try
			{
				Usuario usuario = new Usuario()
				{
					Correo = usuarioDTO.Correo,
					Nombre = usuarioDTO.Nombre,
					Apellido = usuarioDTO.Apellido,
					Contrasena = usuarioDTO.Contrasena,
					Direccion = usuarioDTO.Direccion,
					IdEspacioPrincipal = idEspacioPrincipal
				};
				_usuarioLogic.CrearUsuario(usuario);
				CrearEspacioPrincipal(usuarioDTO.Correo);
			}
			catch (LogicaNegocioUsuarioExcepcion e)
			{
				msjError = e.Message;
			}
			catch (DominioUsuarioExcepcion e)
			{
				msjError = e.Message;
			}
			return msjError;
		}

		public bool RegistradoConExito(UsuarioDTO usuario)
		{
			Usuario usuarioEncontrado = _usuarioLogic.EncontrarUsuario(usuario.Correo);
			if (usuarioEncontrado == null)
			{
				return false;
			}
			usuario.IdEspacioPrincipal = usuarioEncontrado.IdEspacioPrincipal;
			return true;
		}
	}
}
