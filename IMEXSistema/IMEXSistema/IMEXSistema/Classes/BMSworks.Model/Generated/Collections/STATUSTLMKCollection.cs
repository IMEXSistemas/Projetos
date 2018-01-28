using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe STATUSTLMKfoi projetada para trabalhar com listas do tipo da classeSTATUSTLMK
	/// </summary>
	[Serializable]
	public class STATUSTLMKCollection : List<STATUSTLMKEntity>
	{
		public STATUSTLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public STATUSTLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (STATUSTLMKCollection)filter.Filter(this);
		}
	}
}