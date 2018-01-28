using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FORMAPAGAMENTOfoi projetada para trabalhar com listas do tipo da classeFORMAPAGAMENTO
	/// </summary>
	[Serializable]
	public class FORMAPAGAMENTOCollection : List<FORMAPAGAMENTOEntity>
	{
		public FORMAPAGAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FORMAPAGAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FORMAPAGAMENTOCollection)filter.Filter(this);
		}
	}
}