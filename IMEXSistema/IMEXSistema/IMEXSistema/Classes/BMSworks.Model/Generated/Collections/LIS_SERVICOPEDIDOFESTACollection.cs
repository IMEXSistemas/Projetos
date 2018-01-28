using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_SERVICOPEDIDOFESTAfoi projetada para trabalhar com listas do tipo da classeLIS_SERVICOPEDIDOFESTA
	/// </summary>
	[Serializable]
	public class LIS_SERVICOPEDIDOFESTACollection : List<LIS_SERVICOPEDIDOFESTAEntity>
	{
		public LIS_SERVICOPEDIDOFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_SERVICOPEDIDOFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_SERVICOPEDIDOFESTACollection)filter.Filter(this);
		}
	}
}