using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MENSAGEMfoi projetada para trabalhar com listas do tipo da classeMENSAGEM
	/// </summary>
	[Serializable]
	public class MENSAGEMCollection : List<MENSAGEMEntity>
	{
		public MENSAGEMCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MENSAGEMCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MENSAGEMCollection)filter.Filter(this);
		}
	}
}