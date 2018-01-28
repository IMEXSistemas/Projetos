using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSMANUTfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSMANUT
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSMANUTCollection : List<LIS_PRODUTOSMANUTEntity>
	{
		public LIS_PRODUTOSMANUTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSMANUTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSMANUTCollection)filter.Filter(this);
		}
	}
}