using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe PRODUTOSfoi projetada para trabalhar com listas do tipo da classePRODUTOS
	/// </summary>
	[Serializable]
	public class PRODUTOSFASTCollection : List<PRODUTOSFASTEntity>
	{
        public PRODUTOSFASTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public PRODUTOSFASTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (PRODUTOSFASTCollection)filter.Filter(this);
		}
	}
}