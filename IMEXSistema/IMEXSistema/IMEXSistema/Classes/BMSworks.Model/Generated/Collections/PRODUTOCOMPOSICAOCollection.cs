using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOCOMPOSICAOfoi projetada para trabalhar com listas do tipo da classePRODUTOCOMPOSICAO
	/// </summary>
	[Serializable]
	public class PRODUTOCOMPOSICAOCollection : List<PRODUTOCOMPOSICAOEntity>
	{
		public PRODUTOCOMPOSICAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOCOMPOSICAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOCOMPOSICAOCollection)filter.Filter(this);
		}
	}
}