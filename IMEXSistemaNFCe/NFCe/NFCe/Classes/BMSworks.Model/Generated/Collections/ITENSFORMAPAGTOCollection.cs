using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITENSFORMAPAGTOfoi projetada para trabalhar com listas do tipo da classeITENSFORMAPAGTO
	/// </summary>
	[Serializable]
	public class ITENSFORMAPAGTOCollection : List<ITENSFORMAPAGTOEntity>
	{
		public ITENSFORMAPAGTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITENSFORMAPAGTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITENSFORMAPAGTOCollection)filter.Filter(this);
		}
	}
}