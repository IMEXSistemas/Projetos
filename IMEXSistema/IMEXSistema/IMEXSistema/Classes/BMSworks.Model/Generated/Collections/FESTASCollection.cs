using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe FESTASfoi projetada para trabalhar com listas do tipo da classeFESTAS
	/// </summary>
	[Serializable]
	public class FESTASCollection : List<FESTASEntity>
	{
		public FESTASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public FESTASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (FESTASCollection)filter.Filter(this);
		}
	}
}