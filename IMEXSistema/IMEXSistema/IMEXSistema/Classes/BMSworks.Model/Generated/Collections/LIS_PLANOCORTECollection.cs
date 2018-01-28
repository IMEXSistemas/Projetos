using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PLANOCORTEfoi projetada para trabalhar com listas do tipo da classeLIS_PLANOCORTE
	/// </summary>
	[Serializable]
	public class LIS_PLANOCORTECollection : List<LIS_PLANOCORTEEntity>
	{
		public LIS_PLANOCORTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PLANOCORTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PLANOCORTECollection)filter.Filter(this);
		}
	}
}