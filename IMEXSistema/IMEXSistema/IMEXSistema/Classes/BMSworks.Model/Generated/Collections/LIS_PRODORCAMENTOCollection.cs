using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODORCAMENTOfoi projetada para trabalhar com listas do tipo da classeLIS_PRODORCAMENTO
	/// </summary>
	[Serializable]
	public class LIS_PRODORCAMENTOCollection : List<LIS_PRODORCAMENTOEntity>
	{
		public LIS_PRODORCAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODORCAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODORCAMENTOCollection)filter.Filter(this);
		}
	}
}