using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTONFCEfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTONFCE
	/// </summary>
	[Serializable]
	public class LIS_PRODUTONFCECollection : List<LIS_PRODUTONFCEEntity>
	{
		public LIS_PRODUTONFCECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTONFCECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTONFCECollection)filter.Filter(this);
		}
	}
}