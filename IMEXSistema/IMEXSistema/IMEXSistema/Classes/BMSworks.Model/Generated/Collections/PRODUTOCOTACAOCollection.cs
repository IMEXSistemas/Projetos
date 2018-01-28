using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOCOTACAOfoi projetada para trabalhar com listas do tipo da classePRODUTOCOTACAO
	/// </summary>
	[Serializable]
	public class PRODUTOCOTACAOCollection : List<PRODUTOCOTACAOEntity>
	{
		public PRODUTOCOTACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOCOTACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOCOTACAOCollection)filter.Filter(this);
		}
	}
}