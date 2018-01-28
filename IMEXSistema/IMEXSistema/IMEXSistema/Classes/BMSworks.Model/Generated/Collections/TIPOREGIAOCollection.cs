using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOREGIAOfoi projetada para trabalhar com listas do tipo da classeTIPOREGIAO
	/// </summary>
	[Serializable]
	public class TIPOREGIAOCollection : List<TIPOREGIAOEntity>
	{
		public TIPOREGIAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOREGIAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOREGIAOCollection)filter.Filter(this);
		}
	}
}