using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe RELATORIOPERSONALIZADOfoi projetada para trabalhar com listas do tipo da classeRELATORIOPERSONALIZADO
	/// </summary>
	[Serializable]
	public class RELATORIOPERSONALIZADOCollection : List<RELATORIOPERSONALIZADOEntity>
	{
		public RELATORIOPERSONALIZADOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public RELATORIOPERSONALIZADOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (RELATORIOPERSONALIZADOCollection)filter.Filter(this);
		}
	}
}