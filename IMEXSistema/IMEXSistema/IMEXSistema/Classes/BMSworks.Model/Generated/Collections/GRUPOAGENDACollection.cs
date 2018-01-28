using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe GRUPOAGENDAfoi projetada para trabalhar com listas do tipo da classeGRUPOAGENDA
	/// </summary>
	[Serializable]
	public class GRUPOAGENDACollection : List<GRUPOAGENDAEntity>
	{
		public GRUPOAGENDACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public GRUPOAGENDACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (GRUPOAGENDACollection)filter.Filter(this);
		}
	}
}