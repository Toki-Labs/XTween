/**********************************************************************************
/*		File Name 		: SystemUtil.cs
/*		Author 			: 이동명
/*		Description 	: 코딩에 필요한 유틸 기능등을 모아 놓은 클래스
/*		Created Date 	: 2013-12-13
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public enum ExcpetionType
{
    MethodNotImplemented,
    UnsopportedInputType
}

public class SystemUtil
{
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
	private static List<UnityEngine.Object> _dontDestroyList = new List<UnityEngine.Object>();
		
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public static void OpenURL( string url )
	{
#if UNITY_WEBGL
		Application.ExternalEval("window.open('" + url + "','_blank')");
#else
		Application.OpenURL( url );
#endif
	}
		
	//마지막의 "/"은 제외하고 리턴
	public static string AbsPath
	{
		get
		{
#if UNITY_EDITOR
			string absPath = Application.dataPath;
			return absPath.Substring( 0, absPath.LastIndexOf( "/" ) );
#endif
			return Application.dataPath;
		}
	}

	public static Dictionary<string, string[]> EnviromentArgDic
    {
        get
        {
            Dictionary<string, string[]> dic = new Dictionary<string, string[]>();
            List<string> list = new List<string>();
            string[] args = Environment.GetCommandLineArgs();
            if( args != null )
            {
                int length = args.Length;
                for ( int i = 0; i < length; ++i )
                {
                    string value = args[i];
                    string key = GetKey(value);
                    if(key != null)
                    {
                        int index = i + 1;
                        list.Clear();
                        while( index < length )
                        {
                            string arg = args[index];
                            if( GetKey(arg) == null )
                            {
                                list.Add(arg);
                                index++;
                            }
                            else
                            {
                                i = index - 1;
                                break;
                            }
                        }
                        dic.Add(key, list.ToArray());
                    }
                }
            }
            return dic;
        }
    }

    private static string GetKey( string value )
    {
        string key = null;
        if(value.IndexOf("-") == 0)
            key = value.Substring(1,value.Length-1);
        return key;
    }

#if UNITY_EDITOR
    public static string projectName
    {
        get
        {
            string path = SystemUtil.AbsPath;
            int lastIndex = path.LastIndexOf("/") + 1;
            
            return path.Substring(lastIndex, path.Length - lastIndex);
        }
    }
#endif

	public static string currentScene
	{
		get
		{
			string scene = Application.loadedLevelName;
#if UNITY_EDITOR
			if( !Application.isPlaying )
			{
				scene = EditorApplication.currentScene;
				scene = scene.Substring( scene.LastIndexOf('/') + 1 ).Replace( ".unity", "" );
			}
#endif
			return scene;
		}
	}

    public static string timeStamp
    {
        get
        {
            System.DateTime time = System.DateTime.Now;
            string year = time.Year.ToString().Substring(2, 2);
            return string.Format("{0:D2}{1:D2}{2:D2}_{3:D2}{4:D2}", year, time.Month, time.Day, time.Hour, time.Minute);
        }
    }

    public static string timeStampWithSec
    {
        get
        {
            System.DateTime time = System.DateTime.Now;
            string year = time.Year.ToString().Substring(2, 2);
            return string.Format("{0:D2}{1:D2}{2:D2}_{3:D2}{4:D2}{5:D2}", year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }
    }

    public static string currentProjectName
	{
		get
		{
			string[] s = Application.dataPath.Split('/');
			return s[s.Length - 2];
		}
	}

    public static Exception GetException( ExcpetionType type, object obj, string extendDesc = null )
    {
        StringBuilder builder = new StringBuilder();
        builder.Append( "[" ).Append( obj.ToString() ).Append( "] " );
        switch ( type )
        {
            case ExcpetionType.MethodNotImplemented:
                builder.Append( "The method was not implement yet." );
                break;
            case ExcpetionType.UnsopportedInputType:
                builder.Append( "This type of device is unsupport input type." );
                break;
        }
        if( extendDesc != null )
        {
            builder.Append( " " ).Append( extendDesc );
        }

        return new Exception( builder.ToString() );
    }
		
#if UNITY_EDITOR
    public static string androidSDKPath
    {
        get { return EditorPrefs.GetString("AndroidSdkRoot"); }
        set { EditorPrefs.SetString("AndroidSdkRoot", value); }
    }

    public static string adbPath
    {
        get
        {
			string path = androidSDKPath + "/platform-tools/adb";
#if UNITY_EDITOR_WIN
            path +=  ".exe";
#endif
			return path;
        }
    }

    public static string jdkPath
    {
        get { return EditorPrefs.GetString("JdkPath"); }
        set { EditorPrefs.SetString("JdkPath", value); }
    }
#endif

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
#if UNITY_EDITOR
    public static void showTypeAssembly( Type type, string target = null )
	{
		System.Reflection.Assembly assembly = type.Assembly;//typeof(UnityEditor.EditorWindow).Assembly; //Get Type to EditorWindow
		Type[] types = assembly.GetTypes();
		if( target == null )
		{
			foreach ( Type ty in types )
			{
				Debug.Log( ty.ToString() );
			}
		}
		else
		{
			foreach ( Type ty in types )
			{
				string typeStr = ty.ToString();
				if( typeStr.IndexOf( target ) != -1 )
				{
					Debug.Log( typeStr );
				}
			}
		}
	}
	
	public static void showMemberNames( Type classType )
	{
		MemberInfo[] mInfos = classType.GetMembers();
		foreach ( MemberInfo mInfo in mInfos )
		{
			Debug.Log( "Name : " + mInfo.Name + ", Type :" + mInfo.MemberType );
		}
	}
		
	public static void showMemberNames( string typeName )
	{
		showMemberNames( Type.GetType( typeName ) );
	}
		
	public static void showPropertyNames( Type classType )
	{
		PropertyInfo[] pInfos = classType.GetProperties();
		foreach ( PropertyInfo pInfo in pInfos )
		{
			Debug.Log( "Name : " + pInfo.Name + ", Type :" + pInfo.PropertyType + ", CanRead : " + pInfo.CanRead + ", CanWrite : " + pInfo.CanWrite );
		}
	}
		
	public static void showPropertyNames( string typeName )
	{
		showPropertyNames( Type.GetType( typeName ) );
	}
		
	public static void showFieldNames( Type classType )
	{
		FieldInfo[] fInfos = classType.GetFields();
		foreach ( FieldInfo fInfo in fInfos )
		{
			Debug.Log( "Name : " + fInfo.Name + ", Type :" + fInfo.FieldType );
		}
	}
		
	public static void showFieldNames( string typeName )
	{
		showFieldNames( Type.GetType( typeName ) );
	}
		
	public static void showMethodNames( Type classType )
	{
		MethodInfo[] mInfos = classType.GetMethods();
		int pLength;
		ParameterInfo pInfo;
		string paramStr;
		foreach ( MethodInfo mInfo in mInfos )
		{
			ParameterInfo[] pInfos = mInfo.GetParameters();
			pLength = pInfos.Length;
			paramStr = "";
			for ( int i = 0; i < pLength; ++i )
			{
				pInfo = pInfos[i];
				if( i != 0 )
				{
					paramStr += ", ";
				}
				else
				{
					paramStr += " ";
				}
				paramStr += "[" + pInfo.ParameterType + "]" + pInfo.Name;
					
				if( i == pLength - 1 )
				{
					paramStr += " ";
				}
			}
			Debug.Log( mInfo.Name + " (" + paramStr + ") : " + mInfo.ReturnType );
		}
	}
		
	public static void showMethodNames( string typeName )
	{
		showMemberNames( Type.GetType( typeName ) );
	}
		
#endif
	public static void GenerateFolder( string path )
	{
		string[] folders = path.Split( new char[]{'/'} );
		string pathAdd = "";
		int length = folders.Length;
		for ( int i = 0; i < length; ++i )
		{
			pathAdd += folders[i] + "/";
			if( !Directory.Exists( pathAdd ) )
			{
				Directory.CreateDirectory( pathAdd );
			}
		}
	}
		
	public static string getGameObjectPath( GameObject target )
	{
		string path = target.name;
		Transform parent = target.transform.parent;
		while( parent != null )
		{
			path = parent.gameObject.name + "/" + path;
			parent = parent.transform.parent;
		}
		return path;
	}
		
	public static String getLanguageCodeToLanguage( string code )
	{
		SystemLanguage language;
		switch ( code )
		{
		case "af" :
			language = SystemLanguage.Afrikaans;
			break;
		case "ar" :
			language = SystemLanguage.Arabic;
			break;
		case "eu" :
			language = SystemLanguage.Basque;
			break;
		case "be" :
			language = SystemLanguage.Belarusian;
			break;
		case "bg" :
			language = SystemLanguage.Bulgarian;
			break;
		case "ca" :
			language = SystemLanguage.Catalan;
			break;
		case "zh" :
			language = SystemLanguage.Chinese;
			break;
		case "cs" :
			language = SystemLanguage.Czech;
			break;
		case "da" :
			language = SystemLanguage.Danish;
			break;
		case "nl" :
			language = SystemLanguage.Dutch;
			break;
		case "en" :
			language = SystemLanguage.English;
			break;
		case "et" :
			language = SystemLanguage.Estonian;
			break;
		case "fo" :
			language = SystemLanguage.Faroese;
			break;
		case "fi" :
			language = SystemLanguage.Finnish;
			break;
		case "fr" :
			language = SystemLanguage.French;
			break;
		case "de" :
			language = SystemLanguage.German;
			break;
		case "el" :
			language = SystemLanguage.Greek;
			break;
		case "he" :
			language = SystemLanguage.Hebrew;
			break;
		case "hu" :
			language = SystemLanguage.Hungarian;
			break;
		case "is" :
			language = SystemLanguage.Icelandic;
			break;
		case "id" :
			language = SystemLanguage.Indonesian;
			break;
		case "it" :
			language = SystemLanguage.Italian;
			break;
		case "ja" :
			language = SystemLanguage.Japanese;
			break;
		case "ko" :
			language = SystemLanguage.Korean;
			break;
		case "lv" :
			language = SystemLanguage.Latvian;
			break;
		case "lt" :
			language = SystemLanguage.Lithuanian;
			break;
		case "nb" :
			language = SystemLanguage.Norwegian;
			break;
		case "pl" :
			language = SystemLanguage.Polish;
			break;
		case "pt" :
			language = SystemLanguage.Portuguese;
			break;
		case "ro" :
			language = SystemLanguage.Romanian;
			break;
		case "ru" :
			language = SystemLanguage.Russian;
			break;
		case "sr" :
			language = SystemLanguage.SerboCroatian;
			break;
		case "sk" :
			language = SystemLanguage.Slovak;
			break;
		case "sl" :
			language = SystemLanguage.Slovenian;
			break;
		case "es" :
			language = SystemLanguage.Spanish;
			break;
		case "sv" :
			language = SystemLanguage.Swedish;
			break;
		case "th" :
			language = SystemLanguage.Thai;
			break;
		case "tr" :
			language = SystemLanguage.Turkish;
			break;
		case "uk" :
			language = SystemLanguage.Ukrainian;
			break;
		default :
			language = SystemLanguage.Unknown;
			break;
		}
			
		return language.ToString();
	}
}