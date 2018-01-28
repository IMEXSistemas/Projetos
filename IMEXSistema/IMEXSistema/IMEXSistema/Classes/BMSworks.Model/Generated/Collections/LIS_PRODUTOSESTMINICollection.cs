using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_PRODUTOSESTMINIfoi projetada para trabalhar com listas do tipo da classeLIS_PRODUTOSESTMINI
	/// </summary>
	[Serializable]
	public class LIS_PRODUTOSESTMINICollection : List<LIS_PRODUTOSESTMINIEntity>
	{
		public LIS_PRODUTOSESTMINICollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_PRODUTOSESTMINICollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_PRODUTOSESTMINICollection)filter.Filter(this);
		}
	}
}