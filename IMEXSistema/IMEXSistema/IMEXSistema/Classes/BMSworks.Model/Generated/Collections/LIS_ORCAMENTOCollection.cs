using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ORCAMENTOfoi projetada para trabalhar com listas do tipo da classeLIS_ORCAMENTO
	/// </summary>
	[Serializable]
	public class LIS_ORCAMENTOCollection : List<LIS_ORCAMENTOEntity>
	{
		public LIS_ORCAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ORCAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ORCAMENTOCollection)filter.Filter(this);
		}
	}
}