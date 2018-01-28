using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SERVICOPEDOTICAfoi projetada para trabalhar com listas do tipo da classeLIS_SERVICOPEDOTICA
	/// </summary>
	[Serializable]
	public class LIS_SERVICOPEDOTICACollection : List<LIS_SERVICOPEDOTICAEntity>
	{
		public LIS_SERVICOPEDOTICACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SERVICOPEDOTICACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SERVICOPEDOTICACollection)filter.Filter(this);
		}
	}
}