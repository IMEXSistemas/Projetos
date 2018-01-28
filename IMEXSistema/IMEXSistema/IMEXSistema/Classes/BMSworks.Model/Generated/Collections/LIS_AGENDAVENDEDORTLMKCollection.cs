using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_AGENDAVENDEDORTLMKfoi projetada para trabalhar com listas do tipo da classeLIS_AGENDAVENDEDORTLMK
	/// </summary>
	[Serializable]
	public class LIS_AGENDAVENDEDORTLMKCollection : List<LIS_AGENDAVENDEDORTLMKEntity>
	{
		public LIS_AGENDAVENDEDORTLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_AGENDAVENDEDORTLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_AGENDAVENDEDORTLMKCollection)filter.Filter(this);
		}
	}
}