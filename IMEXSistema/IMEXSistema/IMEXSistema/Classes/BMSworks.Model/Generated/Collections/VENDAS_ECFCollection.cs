using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe VENDAS_ECFfoi projetada para trabalhar com listas do tipo da classeVENDAS_ECF
	/// </summary>
	[Serializable]
	public class VENDAS_ECFCollection : List<VENDAS_ECFEntity>
	{
		public VENDAS_ECFCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public VENDAS_ECFCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (VENDAS_ECFCollection)filter.Filter(this);
		}
	}
}