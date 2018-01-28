using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TRIBUNALfoi projetada para trabalhar com listas do tipo da classeTRIBUNAL
	/// </summary>
	[Serializable]
	public class TRIBUNALCollection : List<TRIBUNALEntity>
	{
		public TRIBUNALCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TRIBUNALCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TRIBUNALCollection)filter.Filter(this);
		}
	}
}