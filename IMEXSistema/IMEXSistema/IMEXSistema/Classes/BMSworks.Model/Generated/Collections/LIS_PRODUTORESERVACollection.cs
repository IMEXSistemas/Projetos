using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTORESERVAfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTORESERVA
	/// </summary>
	[Serializable]
	public class LIS_PRODUTORESERVACollection : List<LIS_PRODUTORESERVAEntity>
	{
		public LIS_PRODUTORESERVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTORESERVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTORESERVACollection)filter.Filter(this);
		}
	}
}