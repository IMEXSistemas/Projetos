using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_FECHOSERVICOfoi projetada para trabalhar com listas do tipo da classeLIS_FECHOSERVICO
	/// </summary>
	[Serializable]
	public class LIS_FECHOSERVICOCollection : List<LIS_FECHOSERVICOEntity>
	{
		public LIS_FECHOSERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_FECHOSERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_FECHOSERVICOCollection)filter.Filter(this);
		}
	}
}