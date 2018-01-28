using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SERVICONFEfoi projetada para trabalhar com listas do tipo da classeLIS_SERVICONFE
	/// </summary>
	[Serializable]
	public class LIS_SERVICONFECollection : List<LIS_SERVICONFEEntity>
	{
		public LIS_SERVICONFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SERVICONFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SERVICONFECollection)filter.Filter(this);
		}
	}
}