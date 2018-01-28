using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe GRUPOCATEGORIAfoi projetada para trabalhar com listas do tipo da classeGRUPOCATEGORIA
	/// </summary>
	[Serializable]
	public class GRUPOCATEGORIACollection : List<GRUPOCATEGORIAEntity>
	{
		public GRUPOCATEGORIACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public GRUPOCATEGORIACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (GRUPOCATEGORIACollection)filter.Filter(this);
		}
	}
}