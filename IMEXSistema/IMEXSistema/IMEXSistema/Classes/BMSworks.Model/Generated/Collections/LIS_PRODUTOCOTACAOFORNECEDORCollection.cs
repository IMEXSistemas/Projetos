using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOCOTACAOFORNECEDORfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOCOTACAOFORNECEDOR
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOCOTACAOFORNECEDORCollection : List<LIS_PRODUTOCOTACAOFORNECEDOREntity>
	{
		public LIS_PRODUTOCOTACAOFORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOCOTACAOFORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOCOTACAOFORNECEDORCollection)filter.Filter(this);
		}
	}
}