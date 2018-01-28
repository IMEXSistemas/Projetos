using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOCOTACAOFORNECEDORfoi projetada para trabalhar com listas do tipo da classePRODUTOCOTACAOFORNECEDOR
	/// </summary>
	[Serializable]
	public class PRODUTOCOTACAOFORNECEDORCollection : List<PRODUTOCOTACAOFORNECEDOREntity>
	{
		public PRODUTOCOTACAOFORNECEDORCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOCOTACAOFORNECEDORCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOCOTACAOFORNECEDORCollection)filter.Filter(this);
		}
	}
}