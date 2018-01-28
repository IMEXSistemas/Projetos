using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe RESERVAfoi projetada para trabalhar com listas do tipo da classeRESERVA
	/// </summary>
	[Serializable]
	public class RESERVACollection : List<RESERVAEntity>
	{
		public RESERVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public RESERVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (RESERVACollection)filter.Filter(this);
		}
	}
}