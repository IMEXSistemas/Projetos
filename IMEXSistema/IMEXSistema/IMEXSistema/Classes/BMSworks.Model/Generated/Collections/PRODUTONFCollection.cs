using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTONFfoi projetada para trabalhar com listas do tipo da classePRODUTONF
	/// </summary>
	[Serializable]
	public class PRODUTONFCollection : List<PRODUTONFEntity>
	{
		public PRODUTONFCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTONFCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTONFCollection)filter.Filter(this);
		}
	}
}