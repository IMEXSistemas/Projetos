using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICOfoi projetada para trabalhar com listas do tipo da classeSERVICO
	/// </summary>
	[Serializable]
	public class SERVICOCollection : List<SERVICOEntity>
	{
		public SERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICOCollection)filter.Filter(this);
		}
	}
}