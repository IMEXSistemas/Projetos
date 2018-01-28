using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICONFEfoi projetada para trabalhar com listas do tipo da classeSERVICONFE
	/// </summary>
	[Serializable]
	public class SERVICONFECollection : List<SERVICONFEEntity>
	{
		public SERVICONFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICONFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICONFECollection)filter.Filter(this);
		}
	}
}