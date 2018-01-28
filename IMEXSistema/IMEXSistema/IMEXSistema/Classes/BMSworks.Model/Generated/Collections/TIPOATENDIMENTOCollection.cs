using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOATENDIMENTOfoi projetada para trabalhar com listas do tipo da classeTIPOATENDIMENTO
	/// </summary>
	[Serializable]
	public class TIPOATENDIMENTOCollection : List<TIPOATENDIMENTOEntity>
	{
		public TIPOATENDIMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOATENDIMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOATENDIMENTOCollection)filter.Filter(this);
		}
	}
}