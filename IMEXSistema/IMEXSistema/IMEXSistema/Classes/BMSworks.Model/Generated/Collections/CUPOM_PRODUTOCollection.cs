using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe CUPOM_PRODUTOfoi projetada para trabalhar com listas do tipo da classeCUPOM_PRODUTO
	/// </summary>
	[Serializable]
	public class CUPOM_PRODUTOCollection : List<CUPOM_PRODUTOEntity>
	{
		public CUPOM_PRODUTOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public CUPOM_PRODUTOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (CUPOM_PRODUTOCollection)filter.Filter(this);
		}
	}
}