using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ESTOQUEfoi projetada para trabalhar com listas do tipo da classeESTOQUE
	/// </summary>
	[Serializable]
	public class ESTOQUECollection : List<ESTOQUEEntity>
	{
		public ESTOQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ESTOQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ESTOQUECollection)filter.Filter(this);
		}
	}
}