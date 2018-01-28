using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSPEDFESTAfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSPEDFESTA
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSPEDFESTACollection : List<LIS_PRODUTOSPEDFESTAEntity>
	{
		public LIS_PRODUTOSPEDFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSPEDFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSPEDFESTACollection)filter.Filter(this);
		}
	}
}