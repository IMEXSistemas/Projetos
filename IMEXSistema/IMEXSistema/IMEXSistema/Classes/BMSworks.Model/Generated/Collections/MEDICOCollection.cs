using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MEDICOfoi projetada para trabalhar com listas do tipo da classeMEDICO
	/// </summary>
	[Serializable]
	public class MEDICOCollection : List<MEDICOEntity>
	{
		public MEDICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MEDICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MEDICOCollection)filter.Filter(this);
		}
	}
}