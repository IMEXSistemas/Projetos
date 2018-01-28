using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CENTROCUSTOSfoi projetada para trabalhar com listas do tipo da classeCENTROCUSTOS
	/// </summary>
	[Serializable]
	public class CENTROCUSTOSCollection : List<CENTROCUSTOSEntity>
	{
		public CENTROCUSTOSCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CENTROCUSTOSCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CENTROCUSTOSCollection)filter.Filter(this);
		}
	}
}