/**********************************************************************************
/*		File Name 		: ModuleXTween.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class XTweenExporter
{
	/************************************************************************
	 *	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	 ************************************************************************/
	private static XTweenExporter _instance;
    public static XTweenExporter Instance
    {
        get
        {
            if( _instance == null )
            {
                _instance = new XTweenExporter();
            }
            return _instance;
        }
    }

    public static void UpdateRelease()
    {
        Instance.Export(false,true);
        Instance.UpdateReleasePath();
    }
	
	/************************************************************************
	 *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
    private List<string> _folderList = new List<string>
    { 
        "Assets/Toki/XTween/Example", 
        "Assets/Toki/XTween/Images",
        "Assets/Toki/XTween/Scripts"
    };
    private string _xtweenVersion = "0.0.1";
	
	/************************************************************************
	 *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	 ************************************************************************/
    private string ReplaceTargetStringInContent( string patternStart, string patternEnd, string replaceString, string content )
    {
        int indexStart = content.IndexOf(patternStart);
        int indexEnd = content.IndexOf(patternEnd, indexStart) + patternEnd.Length;
        string target = content.Substring(indexStart, indexEnd - indexStart);
        return content.Replace(target, replaceString);
    }
	
	/************************************************************************
	 *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
    public XTweenExporter()
    {
        this._xtweenVersion = XTweenEditorManager.Instance.Data.version;
    }

	/************************************************************************
	 *	 	 	 	 	Coroutine Declaration	 	  			 	 		*
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
    public string ExportVersion
    {
        get
        {
            string version = XTweenEditorManager.Instance.Data.version;
            if( string.IsNullOrEmpty( version ) )
            {
                return "0.0.1";
            }
            else
            {
                string[] versions = version.Split('.');
                int head = int.Parse(versions[0]);
                int mid = int.Parse(versions[1]);
                int tail = int.Parse(versions[2]);
                tail++;
                if (tail > 999)
                {
                    mid++;
                    tail = 0;
                }
                if (mid > 9)
                {
                    head++;
                }
                return head.ToString() + "." + mid.ToString() + "." + tail.ToString();
            }
        }
    }
	
	public void Export(bool packingAll, bool release = false )
    {
        string addStr = "";
        string exportRootPath = XTweenEditorManager.AbsPath + "/Bin";
        List<string> exportPathList = new List<string>(this._folderList.ToArray());
        if( packingAll )
        {
            addStr = "All_";
            exportRootPath = XTweenEditorManager.ExportDefaultPath;
            string addPath = "Assets/Toki/XTween/Export";
            exportPathList.Add(addPath);
        }
        else
        {
            this._xtweenVersion = this.ExportVersion;
            XTweenEditorManager manager = XTweenEditorManager.Instance;
            manager.Data.version = this._xtweenVersion;
            manager.Save();
        }
        string exportFileName = "XTween_" + addStr + this._xtweenVersion + ".unitypackage";
        string exportPath = exportRootPath + "/" + exportFileName;
        if( !release )
        {
            AssetDatabase.ExportPackage(exportPathList.ToArray(), exportPath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        }
        else
        {
            string packageFile = exportRootPath + "/XTween.unitypackage";
            File.Copy(packageFile, exportPath);
            File.Delete(packageFile);
        }
    }

    private void UpdateReleasePath()
    {
        string first = "Version(Alpha) ";
        string end = ".unitypackage)";
        string replace = "Version(Alpha) {VER} - [XTween_{VER}.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_{VER}.unitypackage)";
        replace = replace.Replace("{VER}", XTweenEditorManager.Instance.Data.version);
        string filePath = XTweenEditorManager.AbsPath + "/README.md";
        string content = XTweenEditorManager.ReadText(filePath);
        content = ReplaceTargetStringInContent(first, end, replace, content);
        XTweenEditorManager.WriteText(filePath, content);
    }
}