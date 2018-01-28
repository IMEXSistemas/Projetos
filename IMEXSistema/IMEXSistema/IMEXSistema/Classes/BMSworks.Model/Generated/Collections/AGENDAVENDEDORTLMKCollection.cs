using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe AGENDAVENDEDORTLMKfoi projetada para trabalhar com listas do tipo da classeAGENDAVENDEDORTLMK
	/// </summary>
	[Serializable]
	public class AGENDAVENDEDORTLMKCollection : List<AGENDAVENDEDORTLMKEntity>
	{
		public AGENDAVENDEDORTLMKCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public AGENDAVENDEDORTLMKCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (AGENDAVENDEDORTLMKCollection)filter.Filter(this);
		}
	}
}