using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDIDOMTQOSfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDIDOMTQOS
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDIDOMTQOSCollection : List<LIS_PRODUTOSPEDIDOMTQOSEntity>
	{
		public LIS_PRODUTOSPEDIDOMTQOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDIDOMTQOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDIDOMTQOSCollection)filter.Filter(this);
		}
	}
}