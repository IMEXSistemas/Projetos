using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ARQUIVOBINARIOfoi projetada para trabalhar com listas do tipo da classeARQUIVOBINARIO
	/// </summary>
	[Serializable]
	public class ARQUIVOBINARIOCollection : List<ARQUIVOBINARIOEntity>
	{
		public ARQUIVOBINARIOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ARQUIVOBINARIOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ARQUIVOBINARIOCollection)filter.Filter(this);
		}
	}
}