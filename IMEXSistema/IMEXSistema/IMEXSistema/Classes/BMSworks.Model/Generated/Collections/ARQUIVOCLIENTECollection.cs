using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ARQUIVOCLIENTEfoi projetada para trabalhar com listas do tipo da classeARQUIVOCLIENTE
	/// </summary>
	[Serializable]
	public class ARQUIVOCLIENTECollection : List<ARQUIVOCLIENTEEntity>
	{
		public ARQUIVOCLIENTECollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ARQUIVOCLIENTECollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ARQUIVOCLIENTECollection)filter.Filter(this);
		}
	}
}