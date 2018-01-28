using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_TABELAIBPTfoi projetada para trabalhar com listas do tipo da classeLIS_TABELAIBPT
	/// </summary>
	[Serializable]
	public class LIS_TABELAIBPTCollection : List<LIS_TABELAIBPTEntity>
	{
		public LIS_TABELAIBPTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_TABELAIBPTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_TABELAIBPTCollection)filter.Filter(this);
		}
	}
}