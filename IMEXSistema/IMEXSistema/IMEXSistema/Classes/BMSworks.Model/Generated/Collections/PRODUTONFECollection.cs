using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTONFEfoi projetada para trabalhar com listas do tipo da classePRODUTONFE
	/// </summary>
	[Serializable]
	public class PRODUTONFECollection : List<PRODUTONFEEntity>
	{
		public PRODUTONFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTONFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTONFECollection)filter.Filter(this);
		}
	}
}