using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MESAfoi projetada para trabalhar com listas do tipo da classeMESA
	/// </summary>
	[Serializable]
	public class MESACollection : List<MESAEntity>
	{
		public MESACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MESACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MESACollection)filter.Filter(this);
		}
	}
}