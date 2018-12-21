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

    public static void Export()
    {
        Instance.UpdateReleasePath(Instance.Export(false));
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
        int indexEnd = content.IndexOf(patternEnd, indexStart) + 1;
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
    public string GetExportVersion()
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
	
	public string Export(bool packingAll)
    {
        string addStr = "";
        string exportPath = XTweenEditorManager.AbsPath + "/Bin";
        List<string> exportPathList = new List<string>(this._folderList.ToArray());
        if( packingAll )
        {
            addStr = "All_";
            exportPath = XTweenEditorManager.ExportDefaultPath;
            string addPath = "Assets/Toki/XTween/Export";
            exportPathList.Add(addPath);
        }
        else
        {
            this._xtweenVersion = this.GetExportVersion();
            XTweenEditorManager manager = XTweenEditorManager.Instance;
            manager.Data.version = this._xtweenVersion;
            manager.Save();
        }
        string exportFileName = "XTween_" + addStr + this._xtweenVersion + ".unitypackage";
        exportPath = exportPath + "/" + exportFileName;
        try
        {
            AssetDatabase.ExportPackage(exportPathList.ToArray(), exportPath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        Debug.Log(File.Exists(exportPath));
        return exportFileName;
    }

    private void UpdateReleasePath(string exportFileName)
    {
        const string FIND_TEXT = "(https://github.com/Toki-Labs/XTween/raw/master/Bin/";
        string filePath = XTweenEditorManager.AbsPath + "/README.md";
        string content = XTweenEditorManager.ReadText(filePath);
        string exportPath = FIND_TEXT + exportFileName + ")";
        content = ReplaceTargetStringInContent(FIND_TEXT, ")", exportPath, content);
        XTweenEditorManager.WriteText(filePath, content);
    }
}