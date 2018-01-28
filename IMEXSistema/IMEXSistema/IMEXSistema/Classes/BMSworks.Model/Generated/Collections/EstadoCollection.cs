using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ESTADOfoi projetada para trabalhar com listas do tipo da classeESTADO
	/// </summary>
	[Serializable]
	public class ESTADOCollection : List<ESTADOEntity>
	{
		public ESTADOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ESTADOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ESTADOCollection)filter.Filter(this);
		}
	}
}