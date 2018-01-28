using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONFIGPEDBALCAOfoi projetada para trabalhar com listas do tipo da classeCONFIGPEDBALCAO
	/// </summary>
	[Serializable]
	public class CONFIGPEDBALCAOCollection : List<CONFIGPEDBALCAOEntity>
	{
		public CONFIGPEDBALCAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONFIGPEDBALCAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONFIGPEDBALCAOCollection)filter.Filter(this);
		}
	}
}