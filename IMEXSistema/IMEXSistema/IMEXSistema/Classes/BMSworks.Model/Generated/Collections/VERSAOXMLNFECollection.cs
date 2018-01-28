using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VERSAOXMLNFEfoi projetada para trabalhar com listas do tipo da classeVERSAOXMLNFE
	/// </summary>
	[Serializable]
	public class VERSAOXMLNFECollection : List<VERSAOXMLNFEEntity>
	{
		public VERSAOXMLNFECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VERSAOXMLNFECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VERSAOXMLNFECollection)filter.Filter(this);
		}
	}
}