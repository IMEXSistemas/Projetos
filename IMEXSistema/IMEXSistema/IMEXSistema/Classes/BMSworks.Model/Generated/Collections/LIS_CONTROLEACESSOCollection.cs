using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_CONTROLEACESSOfoi projetada para trabalhar com listas do tipo da classeLIS_CONTROLEACESSO
	/// </summary>
	[Serializable]
	public class LIS_CONTROLEACESSOCollection : List<LIS_CONTROLEACESSOEntity>
	{
		public LIS_CONTROLEACESSOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_CONTROLEACESSOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_CONTROLEACESSOCollection)filter.Filter(this);
		}
	}
}