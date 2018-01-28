using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOOSFECHfoi projetada para trabalhar com listas do tipo da classePRODUTOOSFECH
	/// </summary>
	[Serializable]
	public class PRODUTOOSFECHCollection : List<PRODUTOOSFECHEntity>
	{
		public PRODUTOOSFECHCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOOSFECHCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOOSFECHCollection)filter.Filter(this);
		}
	}
}