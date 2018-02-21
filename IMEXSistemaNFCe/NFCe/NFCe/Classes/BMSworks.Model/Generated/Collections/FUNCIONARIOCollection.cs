using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FUNCIONARIOfoi projetada para trabalhar com listas do tipo da classeFUNCIONARIO
	/// </summary>
	[Serializable]
	public class FUNCIONARIOCollection : List<FUNCIONARIOEntity>
	{
		public FUNCIONARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FUNCIONARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FUNCIONARIOCollection)filter.Filter(this);
		}
	}
}