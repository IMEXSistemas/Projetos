using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOCOMPOSICAOfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOCOMPOSICAO
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOCOMPOSICAOCollection : List<LIS_PRODUTOCOMPOSICAOEntity>
	{
		public LIS_PRODUTOCOMPOSICAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOCOMPOSICAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOCOMPOSICAOCollection)filter.Filter(this);
		}
	}
}