using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SERVICONPSfoi projetada para trabalhar com listas do tipo da classeLIS_SERVICONPS
	/// </summary>
	[Serializable]
	public class LIS_SERVICONPSCollection : List<LIS_SERVICONPSEntity>
	{
		public LIS_SERVICONPSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SERVICONPSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SERVICONPSCollection)filter.Filter(this);
		}
	}
}