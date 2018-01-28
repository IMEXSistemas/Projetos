using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ORDEMSERVICOfoi projetada para trabalhar com listas do tipo da classeLIS_ORDEMSERVICO
	/// </summary>
	[Serializable]
	public class LIS_ORDEMSERVICOCollection : List<LIS_ORDEMSERVICOEntity>
	{
		public LIS_ORDEMSERVICOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ORDEMSERVICOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ORDEMSERVICOCollection)filter.Filter(this);
		}
	}
}