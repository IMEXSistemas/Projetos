using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe LIS_COTACAOCOMPRAfoi projetada para trabalhar com listas do tipo da classeLIS_COTACAOCOMPRA
	/// </summary>
	[Serializable]
	public class LIS_COTACAOCOMPRACollection : List<LIS_COTACAOCOMPRAEntity>
	{
		public LIS_COTACAOCOMPRACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public LIS_COTACAOCOMPRACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (LIS_COTACAOCOMPRACollection)filter.Filter(this);
		}
	}
}