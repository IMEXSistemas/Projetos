using System;

namespace BMSworks.Collection
{
	/// <summary>
	/// Class to hold a single FilterProperty for the ReflectiveFilter. 
	//  The FilterProperty is a Names and Value
	/// that are to be matched in the filter. /// A custom class was used instead of a hashtable or sorted list
	/// as we need the ability to add duplicate "keys" or in this case, 
	/// properties to the FilterPropertyCollection. 
	/// </summary>
	internal sealed class FilterProperty
	{

		private string _Name=string.Empty;

		private string _Value=string.Empty;

		public FilterProperty(string propertyName, string propertyValue)
		{
			_Name = propertyName;
			_Value = propertyValue;
		}
		public string PropertyName
		{
			get
			{
				if(_Name==null)
				{
					_Name=string.Empty;
				}
				return _Name;
			}
		}

		public string PropertyValue
		{
			get
			{
				if(_Value==null)
				{
					_Value=string.Empty;
				}
				return _Value;
			}
		}
	}
}
