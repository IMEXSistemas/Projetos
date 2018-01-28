using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TIPOTRIBUTACAOfoi projetada para trabalhar com listas do tipo da classeTIPOTRIBUTACAO
	/// </summary>
	[Serializable]
	public class TIPOTRIBUTACAOCollection : List<TIPOTRIBUTACAOEntity>
	{
		public TIPOTRIBUTACAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TIPOTRIBUTACAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TIPOTRIBUTACAOCollection)filter.Filter(this);
		}
	}
}