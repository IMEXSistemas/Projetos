using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDMARCfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDMARC
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDMARCCollection : List<PRODUTOSPEDMARCEntity>
	{
		public PRODUTOSPEDMARCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDMARCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDMARCCollection)filter.Filter(this);
		}
	}
}