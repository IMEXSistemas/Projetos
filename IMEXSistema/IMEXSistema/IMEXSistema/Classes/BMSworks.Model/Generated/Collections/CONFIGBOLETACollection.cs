using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CONFIGBOLETAfoi projetada para trabalhar com listas do tipo da classeCONFIGBOLETA
	/// </summary>
	[Serializable]
	public class CONFIGBOLETACollection : List<CONFIGBOLETAEntity>
	{
		public CONFIGBOLETACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CONFIGBOLETACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CONFIGBOLETACollection)filter.Filter(this);
		}
	}
}