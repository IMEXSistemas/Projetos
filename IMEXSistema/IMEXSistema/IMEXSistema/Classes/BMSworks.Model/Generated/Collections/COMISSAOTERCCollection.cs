using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe COMISSAOTERCfoi projetada para trabalhar com listas do tipo da classeCOMISSAOTERC
	/// </summary>
	[Serializable]
	public class COMISSAOTERCCollection : List<COMISSAOTERCEntity>
	{
		public COMISSAOTERCCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public COMISSAOTERCCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (COMISSAOTERCCollection)filter.Filter(this);
		}
	}
}