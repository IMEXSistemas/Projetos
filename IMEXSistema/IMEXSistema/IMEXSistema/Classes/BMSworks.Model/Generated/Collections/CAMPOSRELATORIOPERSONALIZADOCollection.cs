using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CAMPOSRELATORIOPERSONALIZADOfoi projetada para trabalhar com listas do tipo da classeCAMPOSRELATORIOPERSONALIZADO
	/// </summary>
	[Serializable]
	public class CAMPOSRELATORIOPERSONALIZADOCollection : List<CAMPOSRELATORIOPERSONALIZADOEntity>
	{
		public CAMPOSRELATORIOPERSONALIZADOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CAMPOSRELATORIOPERSONALIZADOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CAMPOSRELATORIOPERSONALIZADOCollection)filter.Filter(this);
		}
	}
}