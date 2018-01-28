using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
	/// <summary>
	/// A classe TABELAIBPTfoi projetada para trabalhar com listas do tipo da classeTABELAIBPT
	/// </summary>
	[Serializable]
	public class TABELAIBPTCollection : List<TABELAIBPTEntity>
	{
		public TABELAIBPTCollection() { }

		/// <summary>
		/// Método para aplicar filtro na coleção.
		/// Obs: Este método aplica um filtro a uma coleção e não na consulta do banco.
		/// <summary>
		public TABELAIBPTCollection Filter(BMSworks.Collection.ReflectiveFilter filter)
		{
			return (TABELAIBPTCollection)filter.Filter(this);
		}
	}
}