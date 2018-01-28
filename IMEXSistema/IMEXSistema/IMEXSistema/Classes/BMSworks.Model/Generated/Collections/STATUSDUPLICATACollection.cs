using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe STATUSDUPLICATAfoi projetada para trabalhar com listas do tipo da classeSTATUSDUPLICATA
	/// </summary>
	[Serializable]
	public class STATUSDUPLICATACollection : List<STATUSDUPLICATAEntity>
	{
		public STATUSDUPLICATACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public STATUSDUPLICATACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (STATUSDUPLICATACollection)filter.Filter(this);
		}
	}
}