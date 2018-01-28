using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FERIADOfoi projetada para trabalhar com listas do tipo da classeFERIADO
	/// </summary>
	[Serializable]
	public class FERIADOCollection : List<FERIADOEntity>
	{
		public FERIADOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FERIADOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FERIADOCollection)filter.Filter(this);
		}
	}
}