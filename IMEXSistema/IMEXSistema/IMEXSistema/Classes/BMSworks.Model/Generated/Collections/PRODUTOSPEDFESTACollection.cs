using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSPEDFESTAfoi projetada para trabalhar com listas do tipo da classePRODUTOSPEDFESTA
	/// </summary>
	[Serializable]
	public class PRODUTOSPEDFESTACollection : List<PRODUTOSPEDFESTAEntity>
	{
		public PRODUTOSPEDFESTACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSPEDFESTACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSPEDFESTACollection)filter.Filter(this);
		}
	}
}