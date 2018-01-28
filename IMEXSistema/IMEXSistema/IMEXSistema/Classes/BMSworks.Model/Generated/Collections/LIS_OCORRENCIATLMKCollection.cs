using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_OCORRENCIATLMKfoi projetada para trabalhar com listas do tipo da classeLIS_OCORRENCIATLMK
	/// </summary>
	[Serializable]
	public class LIS_OCORRENCIATLMKCollection : List<LIS_OCORRENCIATLMKEntity>
	{
		public LIS_OCORRENCIATLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_OCORRENCIATLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_OCORRENCIATLMKCollection)filter.Filter(this);
		}
	}
}