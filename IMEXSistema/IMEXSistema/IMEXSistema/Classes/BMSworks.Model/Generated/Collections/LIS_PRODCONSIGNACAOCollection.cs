using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODCONSIGNACAOfoi projetada para trabalhar com listas do tipo da classeLIS_PRODCONSIGNACAO
	/// </summary>
	[Serializable]
	public class LIS_PRODCONSIGNACAOCollection : List<LIS_PRODCONSIGNACAOEntity>
	{
		public LIS_PRODCONSIGNACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODCONSIGNACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODCONSIGNACAOCollection)filter.Filter(this);
		}
	}
}