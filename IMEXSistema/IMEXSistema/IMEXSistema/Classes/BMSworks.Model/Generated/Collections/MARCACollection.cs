using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MARCAfoi projetada para trabalhar com listas do tipo da classeMARCA
	/// </summary>
	[Serializable]
	public class MARCACollection : List<MARCAEntity>
	{
		public MARCACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MARCACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MARCACollection)filter.Filter(this);
		}
	}
}