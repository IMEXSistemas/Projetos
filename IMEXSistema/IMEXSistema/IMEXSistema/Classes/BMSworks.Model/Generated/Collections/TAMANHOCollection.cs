using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TAMANHOfoi projetada para trabalhar com listas do tipo da classeTAMANHO
	/// </summary>
	[Serializable]
	public class TAMANHOCollection : List<TAMANHOEntity>
	{
		public TAMANHOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TAMANHOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TAMANHOCollection)filter.Filter(this);
		}
	}
}