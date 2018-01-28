using System;
using System.Collections;

namespace BMSworks.Collection
{
	/// <summary>
	/// Filter Property Collection V1.0
	/// Class to hold FilterProperties for the ReflectiveFilter. 
	/// The FilterProperties are a collection of Names and Values
	/// that are to be matched in the filter. 
	/// A custom class was used instead of a hashtable or sorted list
	/// as we need the ability to add duplicate "keys" or in this case,
	/// properties. 
	/// </summary>
	internal sealed class FilterPropertyCollection : CollectionBase 
	{
		public int Add(FilterProperty fp)
		{
			return List.Add(fp);
		}

		public bool Contains(Object item)
		{
			return List.Contains(item);
		}
	}
}