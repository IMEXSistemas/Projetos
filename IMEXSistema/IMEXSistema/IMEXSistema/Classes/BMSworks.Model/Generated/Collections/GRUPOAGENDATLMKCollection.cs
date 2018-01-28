using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe GRUPOAGENDATLMKfoi projetada para trabalhar com listas do tipo da classeGRUPOAGENDATLMK
	/// </summary>
	[Serializable]
	public class GRUPOAGENDATLMKCollection : List<GRUPOAGENDATLMKEntity>
	{
		public GRUPOAGENDATLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public GRUPOAGENDATLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (GRUPOAGENDATLMKCollection)filter.Filter(this);
		}
	}
}