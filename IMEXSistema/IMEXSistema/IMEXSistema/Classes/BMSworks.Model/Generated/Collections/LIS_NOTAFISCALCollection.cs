using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_NOTAFISCALfoi projetada para trabalhar com listas do tipo da classeLIS_NOTAFISCAL
	/// </summary>
	[Serializable]
	public class LIS_NOTAFISCALCollection : List<LIS_NOTAFISCALEntity>
	{
		public LIS_NOTAFISCALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_NOTAFISCALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_NOTAFISCALCollection)filter.Filter(this);
		}
	}
}