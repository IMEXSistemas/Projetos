using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_NOTAFISCALEfoi projetada para trabalhar com listas do tipo da classeLIS_NOTAFISCALE
	/// </summary>
	[Serializable]
	public class LIS_NOTAFISCALECollection : List<LIS_NOTAFISCALEEntity>
	{
		public LIS_NOTAFISCALECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_NOTAFISCALECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_NOTAFISCALECollection)filter.Filter(this);
		}
	}
}