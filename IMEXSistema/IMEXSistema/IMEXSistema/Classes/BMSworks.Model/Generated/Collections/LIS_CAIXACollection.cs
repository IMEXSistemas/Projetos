using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CAIXAfoi projetada para trabalhar com listas do tipo da classeLIS_CAIXA
	/// </summary>
	[Serializable]
	public class LIS_CAIXACollection : List<LIS_CAIXAEntity>
	{
		public LIS_CAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CAIXACollection)filter.Filter(this);
		}
	}
}