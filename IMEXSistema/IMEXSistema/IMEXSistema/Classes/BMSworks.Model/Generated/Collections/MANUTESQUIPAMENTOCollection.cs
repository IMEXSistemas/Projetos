using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MANUTESQUIPAMENTOfoi projetada para trabalhar com listas do tipo da classeMANUTESQUIPAMENTO
	/// </summary>
	[Serializable]
	public class MANUTESQUIPAMENTOCollection : List<MANUTESQUIPAMENTOEntity>
	{
		public MANUTESQUIPAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MANUTESQUIPAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MANUTESQUIPAMENTOCollection)filter.Filter(this);
		}
	}
}