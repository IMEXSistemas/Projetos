using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DUPLICATARECEBERfoi projetada para trabalhar com listas do tipo da classeDUPLICATARECEBER
	/// </summary>
	[Serializable]
	public class DUPLICATARECEBERCollection : List<DUPLICATARECEBEREntity>
	{
		public DUPLICATARECEBERCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DUPLICATARECEBERCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DUPLICATARECEBERCollection)filter.Filter(this);
		}
	}
}