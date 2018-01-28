using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_RESERVAfoi projetada para trabalhar com listas do tipo da classeLIS_RESERVA
	/// </summary>
	[Serializable]
	public class LIS_RESERVACollection : List<LIS_RESERVAEntity>
	{
		public LIS_RESERVACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_RESERVACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_RESERVACollection)filter.Filter(this);
		}
	}
}