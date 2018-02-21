using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTONFCEfoi projetada para trabalhar com listas do tipo da classePRODUTONFCE
	/// </summary>
	[Serializable]
	public class PRODUTONFCECollection : List<PRODUTONFCEEntity>
	{
		public PRODUTONFCECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTONFCECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTONFCECollection)filter.Filter(this);
		}
	}
}