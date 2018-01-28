using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SCRIPTVERSAOfoi projetada para trabalhar com listas do tipo da classeLIS_SCRIPTVERSAO
	/// </summary>
	[Serializable]
	public class LIS_SCRIPTVERSAOCollection : List<LIS_SCRIPTVERSAOEntity>
	{
		public LIS_SCRIPTVERSAOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SCRIPTVERSAOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SCRIPTVERSAOCollection)filter.Filter(this);
		}
	}
}