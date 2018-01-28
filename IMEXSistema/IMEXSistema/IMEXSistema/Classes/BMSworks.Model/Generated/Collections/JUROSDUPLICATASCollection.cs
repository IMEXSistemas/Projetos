using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe JUROSDUPLICATASfoi projetada para trabalhar com listas do tipo da classeJUROSDUPLICATAS
	/// </summary>
	[Serializable]
	public class JUROSDUPLICATASCollection : List<JUROSDUPLICATASEntity>
	{
		public JUROSDUPLICATASCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public JUROSDUPLICATASCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (JUROSDUPLICATASCollection)filter.Filter(this);
		}
	}
}