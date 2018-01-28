using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe COTACAOCOMPRAfoi projetada para trabalhar com listas do tipo da classeCOTACAOCOMPRA
	/// </summary>
	[Serializable]
	public class COTACAOCOMPRACollection : List<COTACAOCOMPRAEntity>
	{
		public COTACAOCOMPRACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public COTACAOCOMPRACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (COTACAOCOMPRACollection)filter.Filter(this);
		}
	}
}