using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDIDOMTfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDIDOMT
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDIDOMTCollection : List<LIS_PRODUTOSPEDIDOMTEntity>
	{
		public LIS_PRODUTOSPEDIDOMTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDIDOMTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDIDOMTCollection)filter.Filter(this);
		}
	}
}