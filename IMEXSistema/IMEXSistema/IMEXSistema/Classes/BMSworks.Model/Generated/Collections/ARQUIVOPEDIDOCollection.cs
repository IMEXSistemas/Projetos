using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe ARQUIVOPEDIDOfoi projetada para trabalhar com listas do tipo da classeARQUIVOPEDIDO
	/// </summary>
	[Serializable]
	public class ARQUIVOPEDIDOCollection : List<ARQUIVOPEDIDOEntity>
	{
		public ARQUIVOPEDIDOCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public ARQUIVOPEDIDOCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (ARQUIVOPEDIDOCollection)filter.Filter(this);
		}
	}
}