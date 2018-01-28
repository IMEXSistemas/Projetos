using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDIDOfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDIDO
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDIDOCollection : List<LIS_PRODUTOSPEDIDOEntity>
	{
		public LIS_PRODUTOSPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDIDOCollection)filter.Filter(this);
		}
	}
}