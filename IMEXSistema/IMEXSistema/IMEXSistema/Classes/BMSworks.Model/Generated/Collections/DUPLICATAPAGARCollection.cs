using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe DUPLICATAPAGARfoi projetada para trabalhar com listas do tipo da classeDUPLICATAPAGAR
	/// </summary>
	[Serializable]
	public class DUPLICATAPAGARCollection : List<DUPLICATAPAGAREntity>
	{
		public DUPLICATAPAGARCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public DUPLICATAPAGARCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (DUPLICATAPAGARCollection)filter.Filter(this);
		}
	}
}