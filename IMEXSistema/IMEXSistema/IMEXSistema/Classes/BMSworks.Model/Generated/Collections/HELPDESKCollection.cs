using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe HELPDESKfoi projetada para trabalhar com listas do tipo da classeHELPDESK
	/// </summary>
	[Serializable]
	public class HELPDESKCollection : List<HELPDESKEntity>
	{
		public HELPDESKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public HELPDESKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (HELPDESKCollection)filter.Filter(this);
		}
	}
}