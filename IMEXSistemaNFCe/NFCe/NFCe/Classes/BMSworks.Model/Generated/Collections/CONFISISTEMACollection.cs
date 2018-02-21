using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONFISISTEMAfoi projetada para trabalhar com listas do tipo da classeCONFISISTEMA
	/// </summary>
	[Serializable]
	public class CONFISISTEMACollection : List<CONFISISTEMAEntity>
	{
		public CONFISISTEMACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONFISISTEMACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONFISISTEMACollection)filter.Filter(this);
		}
	}
}