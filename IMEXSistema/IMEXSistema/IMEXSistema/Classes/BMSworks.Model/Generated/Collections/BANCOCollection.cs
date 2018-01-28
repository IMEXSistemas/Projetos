using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe BANCOfoi projetada para trabalhar com listas do tipo da classeBANCO
	/// </summary>
	[Serializable]
	public class BANCOCollection : List<BANCOEntity>
	{
		public BANCOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public BANCOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (BANCOCollection)filter.Filter(this);
		}
	}
}