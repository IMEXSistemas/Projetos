using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SALAOFESTAfoi projetada para trabalhar com listas do tipo da classeSALAOFESTA
	/// </summary>
	[Serializable]
	public class SALAOFESTACollection : List<SALAOFESTAEntity>
	{
		public SALAOFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SALAOFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SALAOFESTACollection)filter.Filter(this);
		}
	}
}