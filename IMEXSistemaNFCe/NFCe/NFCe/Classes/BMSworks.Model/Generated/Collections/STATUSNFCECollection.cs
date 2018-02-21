using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe STATUSNFCEfoi projetada para trabalhar com listas do tipo da classeSTATUSNFCE
	/// </summary>
	[Serializable]
	public class STATUSNFCECollection : List<STATUSNFCEEntity>
	{
		public STATUSNFCECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public STATUSNFCECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (STATUSNFCECollection)filter.Filter(this);
		}
	}
}