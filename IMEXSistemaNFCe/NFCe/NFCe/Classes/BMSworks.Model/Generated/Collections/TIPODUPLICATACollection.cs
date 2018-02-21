using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPODUPLICATAfoi projetada para trabalhar com listas do tipo da classeTIPODUPLICATA
	/// </summary>
	[Serializable]
	public class TIPODUPLICATACollection : List<TIPODUPLICATAEntity>
	{
		public TIPODUPLICATACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPODUPLICATACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPODUPLICATACollection)filter.Filter(this);
		}
	}
}