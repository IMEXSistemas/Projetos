using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ABERTURACAIXAfoi projetada para trabalhar com listas do tipo da classeLIS_ABERTURACAIXA
	/// </summary>
	[Serializable]
	public class LIS_ABERTURACAIXACollection : List<LIS_ABERTURACAIXAEntity>
	{
		public LIS_ABERTURACAIXACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ABERTURACAIXACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ABERTURACAIXACollection)filter.Filter(this);
		}
	}
}