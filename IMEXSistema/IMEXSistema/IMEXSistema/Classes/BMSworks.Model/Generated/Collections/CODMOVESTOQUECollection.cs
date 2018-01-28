using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CODMOVESTOQUEfoi projetada para trabalhar com listas do tipo da classeCODMOVESTOQUE
	/// </summary>
	[Serializable]
	public class CODMOVESTOQUECollection : List<CODMOVESTOQUEEntity>
	{
		public CODMOVESTOQUECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CODMOVESTOQUECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CODMOVESTOQUECollection)filter.Filter(this);
		}
	}
}