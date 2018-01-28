using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MOVIMENTACAOCONTAfoi projetada para trabalhar com listas do tipo da classeMOVIMENTACAOCONTA
	/// </summary>
	[Serializable]
	public class MOVIMENTACAOCONTACollection : List<MOVIMENTACAOCONTAEntity>
	{
		public MOVIMENTACAOCONTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MOVIMENTACAOCONTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MOVIMENTACAOCONTACollection)filter.Filter(this);
		}
	}
}