using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MOVCONTACORRENTEfoi projetada para trabalhar com listas do tipo da classeMOVCONTACORRENTE
	/// </summary>
	[Serializable]
	public class MOVCONTACORRENTECollection : List<MOVCONTACORRENTEEntity>
	{
		public MOVCONTACORRENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MOVCONTACORRENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MOVCONTACORRENTECollection)filter.Filter(this);
		}
	}
}