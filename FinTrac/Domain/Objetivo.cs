﻿using Excepcion;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Objetivo
    {
        private string _titulo;
        private double _montoMaximo;
		private string? _token;
        private List<Categoria> _categorias = new List<Categoria>();
        public int Id { get; set; }
		public int EspacioId { get; set; }
        public Espacio Espacio { get; set; }
        public string Titulo 
        {
            get
            { 
                return _titulo;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new DominioEspacioExcepcion("El titulo es requerido");
                _titulo = value;
            }
        }
        public double MontoMaximo
        {
			get
            {
				return _montoMaximo;
			}
			set
            {
				if (value <= 0)
					throw new DominioEspacioExcepcion("El monto máximo debe ser mayor a 0.");
				_montoMaximo = value;
			}
		}
        public List<Categoria> Categorias { 
            get
            {
				return _categorias;
			}
            set
            {
                if (value == null || value.Count == 0 )
					throw new DominioEspacioExcepcion("Debe seleccionar al menos una categoría.");
				_categorias = value;
            }
        }
		public string? Token
		{
			get
			{
				return _token;
			}
			set
			{
				_token = value;
			}
		}

		public bool ContieneCategoria(Categoria categoria)
        {
			return _categorias.Contains(categoria);
		}
    }
}
