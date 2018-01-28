using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CUPOMfoi projetada para trabalhar com listas do tipo da classeCUPOM
	/// </summary>
	[Serializable]
	public class CUPOMCollection : List<CUPOMEntity>
	{
		public CUPOMCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CUPOMCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CUPOMCollection)filter.Filter(this);
		}
	}
}