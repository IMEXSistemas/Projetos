using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CABDAVfoi projetada para trabalhar com listas do tipo da classeCABDAV
	/// </summary>
	[Serializable]
	public class CABDAVCollection : List<CABDAVEntity>
	{
		public CABDAVCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CABDAVCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CABDAVCollection)filter.Filter(this);
		}
	}
}