using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDIDOMTfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDIDOMT
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDIDOMTCollection : List<PRODUTOSPEDIDOMTEntity>
	{
		public PRODUTOSPEDIDOMTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDIDOMTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDIDOMTCollection)filter.Filter(this);
		}
	}
}