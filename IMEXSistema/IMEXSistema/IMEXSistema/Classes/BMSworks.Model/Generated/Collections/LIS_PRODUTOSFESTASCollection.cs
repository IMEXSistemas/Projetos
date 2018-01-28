using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSFESTASfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSFESTAS
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSFESTASCollection : List<LIS_PRODUTOSFESTASEntity>
	{
		public LIS_PRODUTOSFESTASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSFESTASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSFESTASCollection)filter.Filter(this);
		}
	}
}