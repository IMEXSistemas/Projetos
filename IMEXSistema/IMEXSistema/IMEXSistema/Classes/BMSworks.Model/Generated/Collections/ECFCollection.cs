using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ECFfoi projetada para trabalhar com listas do tipo da classeECF
	/// </summary>
	[Serializable]
	public class ECFCollection : List<ECFEntity>
	{
		public ECFCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ECFCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ECFCollection)filter.Filter(this);
		}
	}
}