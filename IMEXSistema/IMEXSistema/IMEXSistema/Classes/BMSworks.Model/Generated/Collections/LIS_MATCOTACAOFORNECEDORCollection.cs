using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MATCOTACAOFORNECEDORfoi projetada para trabalhar com listas do tipo da classeLIS_MATCOTACAOFORNECEDOR
	/// </summary>
	[Serializable]
	public class LIS_MATCOTACAOFORNECEDORCollection : List<LIS_MATCOTACAOFORNECEDOREntity>
	{
		public LIS_MATCOTACAOFORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MATCOTACAOFORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MATCOTACAOFORNECEDORCollection)filter.Filter(this);
		}
	}
}