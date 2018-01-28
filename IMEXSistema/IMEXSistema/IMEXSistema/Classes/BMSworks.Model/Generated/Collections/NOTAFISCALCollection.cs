using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe NOTAFISCALfoi projetada para trabalhar com listas do tipo da classeNOTAFISCAL
	/// </summary>
	[Serializable]
	public class NOTAFISCALCollection : List<NOTAFISCALEntity>
	{
		public NOTAFISCALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public NOTAFISCALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (NOTAFISCALCollection)filter.Filter(this);
		}
	}
}