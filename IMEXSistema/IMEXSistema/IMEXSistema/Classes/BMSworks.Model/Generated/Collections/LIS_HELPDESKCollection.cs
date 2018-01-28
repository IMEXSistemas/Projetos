using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_HELPDESKfoi projetada para trabalhar com listas do tipo da classeLIS_HELPDESK
	/// </summary>
	[Serializable]
	public class LIS_HELPDESKCollection : List<LIS_HELPDESKEntity>
	{
		public LIS_HELPDESKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_HELPDESKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_HELPDESKCollection)filter.Filter(this);
		}
	}
}