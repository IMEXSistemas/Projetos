using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDMARCfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDMARC
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDMARCCollection : List<LIS_PRODUTOSPEDMARCEntity>
	{
		public LIS_PRODUTOSPEDMARCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDMARCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDMARCCollection)filter.Filter(this);
		}
	}
}