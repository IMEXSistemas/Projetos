using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ORIGEMMERCADORIAfoi projetada para trabalhar com listas do tipo da classeORIGEMMERCADORIA
	/// </summary>
	[Serializable]
	public class ORIGEMMERCADORIACollection : List<ORIGEMMERCADORIAEntity>
	{
		public ORIGEMMERCADORIACollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ORIGEMMERCADORIACollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ORIGEMMERCADORIACollection)filter.Filter(this);
		}
	}
}