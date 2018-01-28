using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe AVISOAGENDATLMKfoi projetada para trabalhar com listas do tipo da classeAVISOAGENDATLMK
	/// </summary>
	[Serializable]
	public class AVISOAGENDATLMKCollection : List<AVISOAGENDATLMKEntity>
	{
		public AVISOAGENDATLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public AVISOAGENDATLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (AVISOAGENDATLMKCollection)filter.Filter(this);
		}
	}
}