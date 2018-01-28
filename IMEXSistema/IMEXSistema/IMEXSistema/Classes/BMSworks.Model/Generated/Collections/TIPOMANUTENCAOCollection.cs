using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOMANUTENCAOfoi projetada para trabalhar com listas do tipo da classeTIPOMANUTENCAO
	/// </summary>
	[Serializable]
	public class TIPOMANUTENCAOCollection : List<TIPOMANUTENCAOEntity>
	{
		public TIPOMANUTENCAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOMANUTENCAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOMANUTENCAOCollection)filter.Filter(this);
		}
	}
}