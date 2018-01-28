using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_ORDEMSERVICOSFECHfoi projetada para trabalhar com listas do tipo da classeLIS_ORDEMSERVICOSFECH
	/// </summary>
	[Serializable]
	public class LIS_ORDEMSERVICOSFECHCollection : List<LIS_ORDEMSERVICOSFECHEntity>
	{
		public LIS_ORDEMSERVICOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_ORDEMSERVICOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_ORDEMSERVICOSFECHCollection)filter.Filter(this);
		}
	}
}