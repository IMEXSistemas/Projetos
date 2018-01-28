using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FECHOSERVICO2foi projetada para trabalhar com listas do tipo da classeLIS_FECHOSERVICO2
	/// </summary>
	[Serializable]
	public class LIS_FECHOSERVICO2Collection : List<LIS_FECHOSERVICO2Entity>
	{
		public LIS_FECHOSERVICO2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FECHOSERVICO2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FECHOSERVICO2Collection)filter.Filter(this);
		}
	}
}