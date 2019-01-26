/**********************************************************************************
/*		File Name 		: DataUtil.cs
/*		Author 			: 이동명
/*		Description 	: Data관련 클래스를 쉽게 컨트롤하기 위한 유틸 클래스
/*		Created Date 	: 2013-12-15
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

[Serializable]
public class KeyValueData
{
	public string key;
	public object value;
}

public class DataUtil
{
	/************************************************************************
	 *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
		
		
	/************************************************************************
	 *	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	 ************************************************************************/
	public static T[] GetArrayFromCollection<T>( System.Collections.ICollection list )
	{
        if( list == null )
        {
            return null;
        }
		T[] arr = new T[list.Count];
		list.CopyTo( arr, 0 );
		return arr;
	}

    public static T[] GetArrayFromGenericCollection<T>( System.Collections.Generic.ICollection<T> list)
    {
        if (list == null)
        {
            return null;
        }
        T[] arr = new T[list.Count];
        list.CopyTo(arr, 0);
        return arr;
    }

    public static List<string> ConvertListToString( List<object> inputList )
	{
#if !UNITY_WP8 && !UNITY_WSA
		return inputList.ConvertAll( new Converter<object,string>( ConverterString ) );
#else
		List<string> outputList = new List<string>();
		int length = inputList.Count;
		for ( int i = 0; i < length; ++i )
		{
			outputList.Add( inputList[i].ToString() );
		}
		return outputList;
#endif
	}

	public static List<float> GetStringToFloatList( string value )
	{
		if( string.IsNullOrEmpty(value) )
		{
			return null;
		}
		else
		{
			Func<string, float> func = delegate( string target )
			{
				float output;
				float.TryParse(target, out output);
				return output;
			};
			List<string> strList = new List<string>(value.Split(','));
			return strList.ConvertAll<float>( new Converter<string, float>( func ));
		}
	}

	public static List<T> GetStringToEnumList<T>( string value ) where T : struct, IConvertible
	{
		if( string.IsNullOrEmpty(value) )
		{
			return null;
		}
		else
		{
			Func<string, T> func = delegate( string target )
			{
				return (T)Enum.Parse( typeof(T), target );
			};
			List<string> strList = new List<string>(value.Split(','));
			return strList.ConvertAll<T>( new Converter<string, T>( func ));
		}
	}
		
	public static T FromByteArray<T>(Byte[] array)
	{	
		if(array == null || array.Length == 0)
		{
			UnityEngine.Debug.Log("Serialization of zero sized array!");
			return default(T); 
		}

#if !UNITY_WSA
        using(var stream = new System.IO.MemoryStream(array))
		{
			try
			{
				var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				T bundle = (T)formatter.Deserialize(stream);
				return bundle;
			}
			catch(Exception e)
			{
				UnityEngine.Debug.Log("Error when reading stream: "+ e.Message);
			}
		}
#endif
		return default(T);
	}
		
	public static byte[] ToByteArray<T>( T bundle)
	{
#if !UNITY_WSA
        var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		using(var stream = new System.IO.MemoryStream())
		{
			formatter.Serialize(stream, bundle );
			return stream.ToArray();
		}
#else
        return null;
#endif
    }

	public static byte[] ToByteArray( string filePath )
	{
		return File.ReadAllBytes( filePath );
	}
		
	public static string ConverterString( object input )
	{
		return input.ToString();
	}
		
	public static List<T> Copy<T>( ICollection list )
	{
		return new List<T>( GetArrayFromCollection<T>( list ) );
	}
		
	public static int GetLeftDays( DateTime startDate, int period )
	{
		DateTime endDate = startDate.AddDays( period );
		DateTime today = DateTime.Now;
		return (endDate - today).Days + 1;
	}

	public static string[] GetEnumNameList<T>() where T : struct, IConvertible
	{
		int length = GetEnumLength<T>();
		string[] enums = new string[length];
		for ( int i = 0; i < length; ++i )
		{
			enums[i] = Enum.GetName( typeof(T), i );
		}
		return enums;
	}

	public static T GetStringToEnum<T>(string name) where T : struct
	{
		return (T)Enum.Parse(typeof(T), name);
	}
	
	public static List<T> GetEnumList<T>() where T : struct, IConvertible
	{
		int length = GetEnumLength<T>();
		List<T> enumList = new List<T>();
		for ( int i = 0; i < length; ++i )
		{
			enumList.Add((T)Enum.Parse( typeof(T), Enum.GetName( typeof(T), i ) ));
		}
		return enumList;
	}

    public static int GetEnumLength<T>()
    {
        return Enum.GetNames(typeof(T)).Length;
    }

	public static Dictionary<T,K> GetDictionaryFromList<T,K>( List<K> list, string keyName )
	{
		Dictionary<T,K> dic = new Dictionary<T,K>();
		Type type = typeof(K);
		int length = list.Count;
		for ( int i = 0; i < length; ++i )
		{
			K data = list[i];
			T key = (T)type.GetField( keyName ).GetValue(data);
			dic.Add( key, data );
		}

		return (Dictionary<T,K>)dic;
	}

	public static Dictionary<T,K> GetDictionaryFromArray<T,K>( K[] array, string keyName )
	{
		Dictionary<T,K> dic = new Dictionary<T,K>();
		Type type = typeof(K);
		int length = array.Length;
		for ( int i = 0; i < length; ++i )
		{
			K data = array[i];
			T key = (T)type.GetField( keyName ).GetValue(data);
			dic.Add( key, data );
		}

		return (Dictionary<T,K>)dic;
	}

	public static T GetFieldValue<K,T>( K target, string fieldName )
	{
		Type type = typeof(K);
		return (T)type.GetField( fieldName ).GetValue( target );
	}
}