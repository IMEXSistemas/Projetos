using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_HISTORPROCESSOfoi projetada para trabalhar com listas do tipo da classeLIS_HISTORPROCESSO
	/// </summary>
	[Serializable]
	public class LIS_HISTORPROCESSOCollection : List<LIS_HISTORPROCESSOEntity>
	{
		public LIS_HISTORPROCESSOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_HISTORPROCESSOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_HISTORPROCESSOCollection)filter.Filter(this);
		}
	}
}