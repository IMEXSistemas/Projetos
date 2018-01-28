using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe OCORRENCIATLMKfoi projetada para trabalhar com listas do tipo da classeOCORRENCIATLMK
	/// </summary>
	[Serializable]
	public class OCORRENCIATLMKCollection : List<OCORRENCIATLMKEntity>
	{
		public OCORRENCIATLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public OCORRENCIATLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (OCORRENCIATLMKCollection)filter.Filter(this);
		}
	}
}