using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe NOTASERVICOfoi projetada para trabalhar com listas do tipo da classeNOTASERVICO
	/// </summary>
	[Serializable]
	public class NOTASERVICOCollection : List<NOTASERVICOEntity>
	{
		public NOTASERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public NOTASERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (NOTASERVICOCollection)filter.Filter(this);
		}
	}
}