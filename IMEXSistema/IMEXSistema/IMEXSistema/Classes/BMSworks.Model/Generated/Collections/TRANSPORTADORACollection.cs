using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TRANSPORTADORAfoi projetada para trabalhar com listas do tipo da classeTRANSPORTADORA
	/// </summary>
	[Serializable]
	public class TRANSPORTADORACollection : List<TRANSPORTADORAEntity>
	{
		public TRANSPORTADORACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TRANSPORTADORACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TRANSPORTADORACollection)filter.Filter(this);
		}
	}
}