using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe GRUPOSTATUSfoi projetada para trabalhar com listas do tipo da classeGRUPOSTATUS
	/// </summary>
	[Serializable]
	public class GRUPOSTATUSCollection : List<GRUPOSTATUSEntity>
	{
		public GRUPOSTATUSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public GRUPOSTATUSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (GRUPOSTATUSCollection)filter.Filter(this);
		}
	}
}