using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDIDOMTQOSfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDIDOMTQOS
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDIDOMTQOSCollection : List<PRODUTOSPEDIDOMTQOSEntity>
	{
		public PRODUTOSPEDIDOMTQOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDIDOMTQOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDIDOMTQOSCollection)filter.Filter(this);
		}
	}
}