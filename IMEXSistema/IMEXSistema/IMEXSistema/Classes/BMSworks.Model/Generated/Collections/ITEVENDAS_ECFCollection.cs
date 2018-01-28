using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ITEVENDAS_ECFfoi projetada para trabalhar com listas do tipo da classeITEVENDAS_ECF
	/// </summary>
	[Serializable]
	public class ITEVENDAS_ECFCollection : List<ITEVENDAS_ECFEntity>
	{
		public ITEVENDAS_ECFCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ITEVENDAS_ECFCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ITEVENDAS_ECFCollection)filter.Filter(this);
		}
	}
}