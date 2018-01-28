using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDIDOfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDIDO
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDIDOCollection : List<PRODUTOSPEDIDOEntity>
	{
		public PRODUTOSPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDIDOCollection)filter.Filter(this);
		}
	}
}