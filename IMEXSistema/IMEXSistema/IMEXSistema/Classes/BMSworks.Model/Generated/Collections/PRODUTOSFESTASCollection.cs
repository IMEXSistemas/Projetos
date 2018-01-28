using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSFESTASfoi projetada para trabalhar com listas do tipo da classePRODUTOSFESTAS
	/// </summary>
	[Serializable]
	public class PRODUTOSFESTASCollection : List<PRODUTOSFESTASEntity>
	{
		public PRODUTOSFESTASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSFESTASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSFESTASCollection)filter.Filter(this);
		}
	}
}