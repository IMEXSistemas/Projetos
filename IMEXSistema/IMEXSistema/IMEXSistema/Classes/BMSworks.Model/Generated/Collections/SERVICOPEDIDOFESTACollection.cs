using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SERVICOPEDIDOFESTAfoi projetada para trabalhar com listas do tipo da classeSERVICOPEDIDOFESTA
	/// </summary>
	[Serializable]
	public class SERVICOPEDIDOFESTACollection : List<SERVICOPEDIDOFESTAEntity>
	{
		public SERVICOPEDIDOFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SERVICOPEDIDOFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SERVICOPEDIDOFESTACollection)filter.Filter(this);
		}
	}
}