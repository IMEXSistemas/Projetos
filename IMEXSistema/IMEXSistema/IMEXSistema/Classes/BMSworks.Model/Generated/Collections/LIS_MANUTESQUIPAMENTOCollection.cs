using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MANUTESQUIPAMENTOfoi projetada para trabalhar com listas do tipo da classeLIS_MANUTESQUIPAMENTO
	/// </summary>
	[Serializable]
	public class LIS_MANUTESQUIPAMENTOCollection : List<LIS_MANUTESQUIPAMENTOEntity>
	{
		public LIS_MANUTESQUIPAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MANUTESQUIPAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MANUTESQUIPAMENTOCollection)filter.Filter(this);
		}
	}
}