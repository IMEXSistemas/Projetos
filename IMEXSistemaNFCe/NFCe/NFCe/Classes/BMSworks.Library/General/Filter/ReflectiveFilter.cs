using System;
using System.Reflection;
using System.Collections;

namespace BMSworks.Collection
{

	#region Filter Clause Enum

	/// <summary>
	/// Provides a clause for the reflective filter
	/// Is used to decide if filter should be equal
	/// to given property or not equal to.
	/// The filter clause is optional and defaults to 
	/// 'Equals'
	/// </summary>
	public enum FilterClause
	{
		/// <summary>
		/// Equal to filter clause
		/// </summary>
		Equals,
		/// <summary>
		/// Not equal to filter clause
		/// </summary>
		IsNotEqualTo
	}

	#endregion

	#region Filter Operand Enum

	/// <summary>
	/// The filter operand to use. 
	/// The filter operand decides if we should use
	/// AND or OR. For example, if we want 
	/// to filter a collection of users, and we add
	/// a property filter, looking for "Bob" is the "Name" field
	/// and an "Id" of "3" then the OR operand would
	/// filter on all records where the Name =|!= Bob OR
	/// the Id =|!= 3. In the same way the AND operand would
	/// only filter records where Name =|!= Bob AND Id =!|!= 3.
	/// Summary: A Filter Operand is used to AND or OR together
	/// more than one filter
	/// </summary>
	public enum FilterOperand
	{
		/// <summary>
		/// AND operand
		/// </summary>
		And,
		/// <summary>
		/// OR operand
		/// </summary>
		Or
	}
	#endregion

	#region Reflective Filter

	/// <summary>
	/// Reflective Filter V1.0
	/// The reflective filter is a generic filter that is able to 
	/// filter ANY strongly typed object collection 
	/// according to any number of property values that are 
	/// either equal to or not equal to associated values.
	/// The values can will be met on either an OR or AND 
	/// conditional. 
	/// </summary>
	public sealed class ReflectiveFilter
	{
		/// <summary>
		/// A collection of Filter Properties by which the 
		/// filter will filter on
		/// </summary>
		private FilterPropertyCollection _FilterList;
		/// <summary>
		/// The object type contained in the strongly typed collection
		/// E.g - In a UserCollection would be User objects
		/// </summary>
		private Type _ObjectType;
		/// <summary>
		/// The concrete object collection type that will be returned. 
		/// For example, CompanyName.DomainObjects.ClassCollection
		/// </summary>
		private Type _CollectionType;
		/// <summary>
		/// The abstract collection that is returned after being filtered
		/// </summary>
        private IList _FilteredCollection;
		/// <summary>
		/// The filter clause to apply. 
		/// Default: Equals
		/// Options: Equals, IsNotEqualTo
		/// When filtering records, we can choose if we want to filter on properties
		/// that are 'Equal' or 'Not Equal to' our FilterProperty criteria
		/// </summary>
		private FilterClause _Clause = FilterClause.Equals;
		/// <summary>
		/// The filter operand to use. 
		/// The filter operand decides if we should use
		/// AND or OR. For example, if we want 
		/// to filter a collection of users, and we add
		/// a property filter, looking for "Bob" is the "Name" field
		/// and an "Id" of "3" then the OR operand would
		/// filter on all records where the Name =|!= Bob OR
		/// the Id =|!= 3. In the same way the AND operand would
		/// only filter records where Name =|!= Bob AND Id =!|!= 3.
		/// Default: Or
		/// Options: Or, And
		/// </summary>
		private FilterOperand _Operand = FilterOperand.Or;

		public ReflectiveFilter(Type typeOfObject, Type typeOfCollection)
		{
			_FilterList = new FilterPropertyCollection();
			_ObjectType = typeOfObject;
			_CollectionType = typeOfCollection;
			try
			{ // create the concrete collection type.
                _FilteredCollection = (IList)Activator.CreateInstance(_CollectionType);
			}
			catch(System.MemberAccessException)
			{
				throw new MemberAccessException("You must specify a collection that is not abstract");
			}
		}

		public ReflectiveFilter(Type typeOfObject, Type typeOfCollection, FilterClause clause)
		{
			_FilterList = new FilterPropertyCollection();
			_ObjectType = typeOfObject;
			_CollectionType = typeOfCollection;
			_Clause = clause;
			try
			{
				// create the concrete collection type
                _FilteredCollection = (IList)Activator.CreateInstance(_CollectionType);
			}
			catch(System.MemberAccessException)
			{
				throw new MemberAccessException("You must specify a collection that is not abstract");
			}
		}

		public ReflectiveFilter(Type typeOfObject, Type typeOfCollection, FilterOperand operand)
		{
			_FilterList = new FilterPropertyCollection();
			_ObjectType = typeOfObject;
			_CollectionType = typeOfCollection;
			_Operand = operand;
			try
			{
				// create the concrete collection type
                _FilteredCollection = (IList)Activator.CreateInstance(_CollectionType);
			}
			catch(System.MemberAccessException)
			{
				throw new MemberAccessException("You must specify a collection that is not abstract");
			}
		}

		public ReflectiveFilter(Type typeOfObject, Type typeOfCollection, FilterClause clause, FilterOperand operand)
		{
			_FilterList = new FilterPropertyCollection();
			_Operand = operand;
			_ObjectType = typeOfObject;
			_CollectionType = typeOfCollection;
			_Clause = clause;

			try
			{ // create the concrete collection 
                _FilteredCollection = (IList)Activator.CreateInstance(_CollectionType);
			}
			catch(System.MemberAccessException)
			{
				throw new MemberAccessException("You must specify a collection that is not abstract");
			}
		}

		/// <summary>
		/// Adds a property filter to the filter. 
		/// A property filter contains the name of the 
		/// property to filter on, plus the value of 
		/// the property to match
		/// 
		/// the name of the property to filter on
		/// the value of the property to match
		/// </summary>
		public void AddFilter(string property, string matchValue)
		{
			FilterProperty filterProperty = new FilterProperty(property, matchValue);
			_FilterList.Add(filterProperty);
		}
		/// <summary>
		/// Filters the collection according to the added filters. 
		/// If no filters have been applied, then the original collection
		/// is returned
		/// 
		/// the collection to be filtered
		/// a filtered collection
		/// </summary>
        public IList Filter(IList collection)
		{
			// no filters have been added. Return the same collection
			if(_FilterList.Count==0)
			{
				return collection;
			}
			FilterOperation filterOp = new FilterOperation(_ObjectType, _CollectionType, _Clause, _FilterList, _Operand);
			filterOp.Filter(collection, _FilteredCollection);
			return _FilteredCollection;
		}

		#region Filter Operation Private Class

		/// <summary>
		/// Provides the filtering implementation for the reflective filter. 
		/// </summary>
		private sealed class FilterOperation
		{
			/// <summary>
			/// The object type that will be in the collection. For example, CompanyName.DomainObjects.Class
			/// </summary>
			private Type _ObjectType;
			/// <summary>
			/// The concrete object collection type that will be returned. 
			/// For example, CompanyName.DomainObjects.ClassCollection
			/// </summary>
			private Type _CollectionType;
			/// <summary>
			/// The concrete object collection type that will be returned. For example, CompanyName.DomainObjects.ClassCollection
			/// private Type _CollectionType;
			/// 
			/// The clause by which to filter. For example, _Property value that is _FilterClause (== or !=) to _FilterValue
			/// </summary>
			private FilterClause _FilterClause;
			/// <summary>
			/// The filter operand to use. 
			/// The filter operand decides if we should use
			/// AND or OR. For example, if we want 
			/// to filter a collection of users, and we add
			/// a property filter, looking for "Bob" is the "Name" field
			/// and an "Id" of "3" then the OR operand would
			/// filter on all records where the Name =|!= Bob OR
			/// the Id =|!= 3. In the same way the AND operand would
			/// only filter records where Name =|!= Bob AND Id =!|!= 3.
			/// </summary>
			private FilterOperand _Operand;
			/// <summary>
			/// A collection of Filter Properties by which the /// filter will filter on
			/// </summary>
			private FilterPropertyCollection _PropertiesAndValues;
			/// <summary>
			/// Constrcutor to initiate the reflective filter's 'filtering' operation.
			/// 
			/// object type of holding property value
			/// the type of collection to add the object back into if filtered
			/// the optional clause of == or != to
			/// the FilterPropertiesCollection that contains /// all of the property names and values that we need to match
			/// 
			/// the filter operand. Either an OR or AND
			/// </summary>
			public FilterOperation(Type typeOfObject, Type typeOfCollection, FilterClause clause, FilterPropertyCollection propertiesAndValues, FilterOperand operand)
			{
				_ObjectType = typeOfObject;
				_CollectionType = typeOfCollection;
				_FilterClause = clause;
				_PropertiesAndValues = propertiesAndValues;
				_Operand = operand;
			}
			/// <summary>
			/// Performs the filtering based on the 
			/// constrcutor's parameters
			/// 
			/// The collection to perform the filtering upon
			/// the collection that is passed back after being filtered
			/// 
			/// A strongly typed collection of objects that have been filtered according to the filter criteria
			/// </summary>
            public IList Filter(IList originalCollection, IList filteringCollection)
			{
				switch(_Operand) // AND or OR
				{
					case FilterOperand.And:
					{
						int propertyCount = _PropertiesAndValues.Count;
						int currentCount = 0; // E.g. current 0 : PropertiesAndValues 2
						foreach(object o in originalCollection)
						{
							switch(_FilterClause) // Equal or IsNotEqulaTo
							{
								case FilterClause.IsNotEqualTo:
								{
									foreach(FilterProperty fp in _PropertiesAndValues)
									{
										// if we match current property value with object's property value
										if(_ObjectType.GetProperty(fp.PropertyName.ToString()).GetValue(o, null).ToString() !=fp.PropertyValue.ToString())
										{
											currentCount++;
										}
									}
									if(currentCount==propertyCount) // then it all adds up to true
									{
										filteringCollection.Add(o);
									}
									break;
								}
								default:
								{
									foreach(FilterProperty fp in _PropertiesAndValues)
									{
										// if we match current property value with object's property value
										if(_ObjectType.GetProperty(fp.PropertyName.ToString()).GetValue(o, null).ToString() ==fp.PropertyValue.ToString())
										{
											currentCount++;
										}
									}
									if(currentCount==propertyCount) // then it all adds up to true
									{
										filteringCollection.Add(o);
									}								
									break;
								}
							}
							currentCount = 0;

						}

						break;

					}

					default: // OR operand
					{
						try
						{
							// make a string (quicker than other ways I tried)
							// with a strange delimiter.
							string valuesString=string.Empty;
							foreach(FilterProperty fp in _PropertiesAndValues)
							{
								valuesString+=fp.PropertyValue+"¶";
							}

							// loop through the collection
							foreach(object o in originalCollection)
							{
								switch(_FilterClause) // Equal or IsNotEqualTo
								{
									case FilterClause.IsNotEqualTo: // Not Equal To
									{
										// for every property
										foreach(FilterProperty fp in _PropertiesAndValues)
										{
											
											// if the object's property value is NOT found in the valuesString, add it
											if(valuesString.IndexOf( _ObjectType.GetProperty(fp.PropertyName).GetValue(o, null).ToString()+"¶")==-1)
											{
												if(filteringCollection.Contains(o)==false)
												{
													filteringCollection.Add(o);
												}
											}
											foreach(FilterProperty ifp in _PropertiesAndValues)
											{
												// removal if later property dictates it shouldnt be there
												if(valuesString.IndexOf( _ObjectType.GetProperty(ifp.PropertyName).GetValue(o, null).ToString()+"¶")!=-1)
												{
													if(filteringCollection.Contains(o)==true)
													{
														filteringCollection.Remove(o);
													}
												}
											}
										}
										break; 
									}

									default: // Equals
									{
										// for every property
										foreach(FilterProperty fp in _PropertiesAndValues)
										{
											// if the objects property value IS found in the valuesString, add it
											if(_ObjectType.GetProperty(fp.PropertyName).GetValue(o, null).ToString()==fp.PropertyValue)
											{
												if(filteringCollection.Contains(o)==false)
												{
													filteringCollection.Add(o);
												}
											}
										} 
										break;


									}
								}
							} 
							break;
						}
						catch(System.NullReferenceException)
						{
							throw new NullReferenceException("The types you have used may not be correct, or the given property does not exist"); 
						}
					}
				}
				return filteringCollection;
			}
		}
		#endregion
	}

	#endregion

}
