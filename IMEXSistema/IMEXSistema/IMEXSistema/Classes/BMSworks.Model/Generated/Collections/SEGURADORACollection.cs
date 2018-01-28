using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SEGURADORAfoi projetada para trabalhar com listas do tipo da classeSEGURADORA
	/// </summary>
	[Serializable]
	public class SEGURADORACollection : List<SEGURADORAEntity>
	{
		public SEGURADORACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SEGURADORACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SEGURADORACollection)filter.Filter(this);
		}
	}
}