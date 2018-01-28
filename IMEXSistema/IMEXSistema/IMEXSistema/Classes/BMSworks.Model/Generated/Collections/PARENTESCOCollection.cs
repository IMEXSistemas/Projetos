using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PARENTESCOfoi projetada para trabalhar com listas do tipo da classePARENTESCO
	/// </summary>
	[Serializable]
	public class PARENTESCOCollection : List<PARENTESCOEntity>
	{
		public PARENTESCOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PARENTESCOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PARENTESCOCollection)filter.Filter(this);
		}
	}
}