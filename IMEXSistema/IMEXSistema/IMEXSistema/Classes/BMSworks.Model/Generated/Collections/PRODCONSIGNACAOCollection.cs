using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODCONSIGNACAOfoi projetada para trabalhar com listas do tipo da classePRODCONSIGNACAO
	/// </summary>
	[Serializable]
	public class PRODCONSIGNACAOCollection : List<PRODCONSIGNACAOEntity>
	{
		public PRODCONSIGNACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODCONSIGNACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODCONSIGNACAOCollection)filter.Filter(this);
		}
	}
}