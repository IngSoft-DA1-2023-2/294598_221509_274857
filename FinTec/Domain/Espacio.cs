﻿namespace Domain
{
	public class Espacio
	{
		private Usuario _admin;
		private List<Usuario> _usuariosInvitados;
		public List<Usuario> UsuariosInvitados
		{
			get
			{
				return _usuariosInvitados;
			}
			set
			{
				if (value.Contains(_admin))
					throw new DomainEspacioException("El administrador no puede estar en la lista de invitados");
				_usuariosInvitados = value;
			}
		}
		public Usuario Admin
		{
			get
			{
				return _admin;
			}
			set
			{
				if (value == null)
					throw new DomainEspacioException("El espacio debe tener un administrador");
				_admin = value;
			}
		}
		public Espacio()
		{
		}

		public void cambiarAdmin(Usuario nuevoAdmin)
		{
			if (!UsuariosInvitados.Contains(nuevoAdmin))
				throw new DomainEspacioException("El nuevo administrador no puede estar en la lista de invitados");
			UsuariosInvitados.Add(Admin);
			Admin = nuevoAdmin;
			UsuariosInvitados.Remove(nuevoAdmin);
		}
	}
}
