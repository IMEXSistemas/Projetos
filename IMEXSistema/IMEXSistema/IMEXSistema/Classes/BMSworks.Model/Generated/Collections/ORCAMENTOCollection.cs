using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ORCAMENTOfoi projetada para trabalhar com listas do tipo da classeORCAMENTO
	/// </summary>
	[Serializable]
	public class ORCAMENTOCollection : List<ORCAMENTOEntity>
	{
		public ORCAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ORCAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ORCAMENTOCollection)filter.Filter(this);
		}
	}
}