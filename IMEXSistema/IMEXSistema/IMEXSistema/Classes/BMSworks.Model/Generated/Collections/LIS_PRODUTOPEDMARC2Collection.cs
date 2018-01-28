using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOPEDMARC2foi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOPEDMARC2
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOPEDMARC2Collection : List<LIS_PRODUTOPEDMARC2Entity>
	{
		public LIS_PRODUTOPEDMARC2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOPEDMARC2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOPEDMARC2Collection)filter.Filter(this);
		}
	}
}