using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_MATERIALPEDIDOfoi projetada para trabalhar com listas do tipo da classeLIS_MATERIALPEDIDO
	/// </summary>
	[Serializable]
	public class LIS_MATERIALPEDIDOCollection : List<LIS_MATERIALPEDIDOEntity>
	{
		public LIS_MATERIALPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_MATERIALPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_MATERIALPEDIDOCollection)filter.Filter(this);
		}
	}
}