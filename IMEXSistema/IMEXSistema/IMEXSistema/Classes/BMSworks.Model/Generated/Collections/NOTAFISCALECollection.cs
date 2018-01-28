using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe NOTAFISCALEfoi projetada para trabalhar com listas do tipo da classeNOTAFISCALE
	/// </summary>
	[Serializable]
	public class NOTAFISCALECollection : List<NOTAFISCALEEntity>
	{
		public NOTAFISCALECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public NOTAFISCALECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (NOTAFISCALECollection)filter.Filter(this);
		}
	}
}