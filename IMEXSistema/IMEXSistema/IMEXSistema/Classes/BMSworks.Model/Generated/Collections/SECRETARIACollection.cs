using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe SECRETARIAfoi projetada para trabalhar com listas do tipo da classeSECRETARIA
	/// </summary>
	[Serializable]
	public class SECRETARIACollection : List<SECRETARIAEntity>
	{
		public SECRETARIACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public SECRETARIACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (SECRETARIACollection)filter.Filter(this);
		}
	}
}