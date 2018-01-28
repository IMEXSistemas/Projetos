using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe HISTORPROCESSOfoi projetada para trabalhar com listas do tipo da classeHISTORPROCESSO
	/// </summary>
	[Serializable]
	public class HISTORPROCESSOCollection : List<HISTORPROCESSOEntity>
	{
		public HISTORPROCESSOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public HISTORPROCESSOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (HISTORPROCESSOCollection)filter.Filter(this);
		}
	}
}