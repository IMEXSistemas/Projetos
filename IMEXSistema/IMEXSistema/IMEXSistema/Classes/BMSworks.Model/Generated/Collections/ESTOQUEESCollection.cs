using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ESTOQUEESfoi projetada para trabalhar com listas do tipo da classeESTOQUEES
	/// </summary>
	[Serializable]
	public class ESTOQUEESCollection : List<ESTOQUEESEntity>
	{
		public ESTOQUEESCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ESTOQUEESCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ESTOQUEESCollection)filter.Filter(this);
		}
	}
}