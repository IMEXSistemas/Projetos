using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_EQUIPAMENTOOSFECHfoi projetada para trabalhar com listas do tipo da classeLIS_EQUIPAMENTOOSFECH
	/// </summary>
	[Serializable]
	public class LIS_EQUIPAMENTOOSFECHCollection : List<LIS_EQUIPAMENTOOSFECHEntity>
	{
		public LIS_EQUIPAMENTOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_EQUIPAMENTOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_EQUIPAMENTOOSFECHCollection)filter.Filter(this);
		}
	}
}