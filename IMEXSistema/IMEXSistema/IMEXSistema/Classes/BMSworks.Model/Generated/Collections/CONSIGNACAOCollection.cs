using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONSIGNACAOfoi projetada para trabalhar com listas do tipo da classeCONSIGNACAO
	/// </summary>
	[Serializable]
	public class CONSIGNACAOCollection : List<CONSIGNACAOEntity>
	{
		public CONSIGNACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONSIGNACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONSIGNACAOCollection)filter.Filter(this);
		}
	}
}