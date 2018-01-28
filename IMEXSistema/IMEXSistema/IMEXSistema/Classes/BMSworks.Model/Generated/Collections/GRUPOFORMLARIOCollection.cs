using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe GRUPOFORMLARIOfoi projetada para trabalhar com listas do tipo da classeGRUPOFORMLARIO
	/// </summary>
	[Serializable]
	public class GRUPOFORMLARIOCollection : List<GRUPOFORMLARIOEntity>
	{
		public GRUPOFORMLARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public GRUPOFORMLARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (GRUPOFORMLARIOCollection)filter.Filter(this);
		}
	}
}