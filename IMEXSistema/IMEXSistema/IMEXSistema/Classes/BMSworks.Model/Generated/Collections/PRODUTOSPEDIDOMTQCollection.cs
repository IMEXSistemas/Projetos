using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDIDOMTQfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDIDOMTQ
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDIDOMTQCollection : List<PRODUTOSPEDIDOMTQEntity>
	{
		public PRODUTOSPEDIDOMTQCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDIDOMTQCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDIDOMTQCollection)filter.Filter(this);
		}
	}
}