using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe AGENDAfoi projetada para trabalhar com listas do tipo da classeAGENDA
	/// </summary>
	[Serializable]
	public class ESTOQUELOTECollection : List<ESTOQUELOTEEntity>
	{
		public ESTOQUELOTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ESTOQUELOTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ESTOQUELOTECollection)filter.Filter(this);
		}
	}
}