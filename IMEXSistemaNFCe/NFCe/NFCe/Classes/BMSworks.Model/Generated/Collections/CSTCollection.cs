using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CSTfoi projetada para trabalhar com listas do tipo da classeCST
	/// </summary>
	[Serializable]
	public class CSTCollection : List<CSTEntity>
	{
		public CSTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CSTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CSTCollection)filter.Filter(this);
		}
	}
}