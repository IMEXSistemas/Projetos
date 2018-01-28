using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_EQUIPAMENTOfoi projetada para trabalhar com listas do tipo da classeLIS_EQUIPAMENTO
	/// </summary>
	[Serializable]
	public class LIS_EQUIPAMENTOCollection : List<LIS_EQUIPAMENTOEntity>
	{
		public LIS_EQUIPAMENTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_EQUIPAMENTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_EQUIPAMENTOCollection)filter.Filter(this);
		}
	}
}