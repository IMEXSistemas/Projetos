using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FORMULARIOfoi projetada para trabalhar com listas do tipo da classeFORMULARIO
	/// </summary>
	[Serializable]
	public class FORMULARIOCollection : List<FORMULARIOEntity>
	{
		public FORMULARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FORMULARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FORMULARIOCollection)filter.Filter(this);
		}
	}
}