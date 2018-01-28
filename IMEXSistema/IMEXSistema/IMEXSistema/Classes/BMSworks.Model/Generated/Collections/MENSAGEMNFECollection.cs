using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe MENSAGEMNFEfoi projetada para trabalhar com listas do tipo da classeMENSAGEMNFE
	/// </summary>
	[Serializable]
	public class MENSAGEMNFECollection : List<MENSAGEMNFEEntity>
	{
		public MENSAGEMNFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public MENSAGEMNFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (MENSAGEMNFECollection)filter.Filter(this);
		}
	}
}