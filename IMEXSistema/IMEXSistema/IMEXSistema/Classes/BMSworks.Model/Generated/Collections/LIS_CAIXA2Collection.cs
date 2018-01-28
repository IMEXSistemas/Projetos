using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CAIXA2foi projetada para trabalhar com listas do tipo da classeLIS_CAIXA2
	/// </summary>
	[Serializable]
	public class LIS_CAIXA2Collection : List<LIS_CAIXA2Entity>
	{
		public LIS_CAIXA2Collection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CAIXA2Collection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CAIXA2Collection)filter.Filter(this);
		}
	}
}