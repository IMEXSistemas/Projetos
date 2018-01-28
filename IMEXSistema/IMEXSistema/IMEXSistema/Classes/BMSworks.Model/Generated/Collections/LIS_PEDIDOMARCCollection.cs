using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PEDIDOMARCfoi projetada para trabalhar com listas do tipo da classeLIS_PEDIDOMARC
	/// </summary>
	[Serializable]
	public class LIS_PEDIDOMARCCollection : List<LIS_PEDIDOMARCEntity>
	{
		public LIS_PEDIDOMARCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PEDIDOMARCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PEDIDOMARCCollection)filter.Filter(this);
		}
	}
}