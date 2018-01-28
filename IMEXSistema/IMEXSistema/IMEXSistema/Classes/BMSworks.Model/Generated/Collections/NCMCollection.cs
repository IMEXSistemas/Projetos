using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe NCMfoi projetada para trabalhar com listas do tipo da classeNCM
	/// </summary>
	[Serializable]
	public class NCMCollection : List<NCMEntity>
	{
		public NCMCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public NCMCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (NCMCollection)filter.Filter(this);
		}
	}
}