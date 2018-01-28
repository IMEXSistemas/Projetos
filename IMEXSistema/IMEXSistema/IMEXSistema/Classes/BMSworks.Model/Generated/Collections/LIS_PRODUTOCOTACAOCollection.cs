using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOCOTACAOfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOCOTACAO
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOCOTACAOCollection : List<LIS_PRODUTOCOTACAOEntity>
	{
		public LIS_PRODUTOCOTACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOCOTACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOCOTACAOCollection)filter.Filter(this);
		}
	}
}