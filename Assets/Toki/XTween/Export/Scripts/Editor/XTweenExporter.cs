/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
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

namespace Toki.Tween
{
    [InitializeOnLoad]
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

        [MenuItem("Window/XTween Build #%B",priority=16)]
        public static void ExampleBuild()
        {
            string buildPath = XTweenEditorManager.AbsPath + "/Export/WebGL";
            if( Directory.Exists(buildPath) )
                Directory.Delete(buildPath, true);

            BuildPipeline.BuildPlayer
            ( 
                FindEnabledEditorScenes(), 
                "Export/WebGL", 
                BuildTarget.WebGL, 
                BuildOptions.None
            );
        }

        protected static string[] FindEnabledEditorScenes()
        {
            List<string> EditorScenes = new List<string>();
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (!scene.enabled)
                {
                    continue;
                }

                EditorScenes.Add(scene.path);
            }
            return EditorScenes.ToArray();
        }

        public static string ReadText( string path )
        {
            return File.ReadAllText(path);
        }

        public static void WriteText( string path, string content )
        {
            StreamWriter writer = File.CreateText(path);
            writer.Write(content);
            writer.Close();
        }
        
        /************************************************************************
        *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
        ************************************************************************/
        private XTweenData _data;
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
        public XTweenData Data
        {
            get
            {
                return _data;
            }
        }

        public string JsonPath
        {
            get
            {
                return XTweenEditorManager.AbsPath + "/Assets/Toki/XTween/Export/Scripts/Editor/xtween_config.json";
            }
        }

        public static string ExportDefaultPath
        {
            get
            {
                string path = "";
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
                else
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/Documents";
                }
                return path;
            }
        }

        
        /************************************************************************
        *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
        ************************************************************************/
        public XTweenExporter()
        {
            this.Load();
            this._xtweenVersion = Data.version;
        }

        /************************************************************************
        *	 	 	 	 	Coroutine Declaration	 	  			 	 		*
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
        ************************************************************************/
        private string ReplaceTargetStringInContent( string patternStart, string patternEnd, string replaceString, string content )
        {
            int indexStart = content.IndexOf(patternStart);
            int indexEnd = content.IndexOf(patternEnd, indexStart) + patternEnd.Length;
            string target = content.Substring(indexStart, indexEnd - indexStart);
            return content.Replace(target, replaceString);
        }

        private void Load()
        {
            if( File.Exists(this.JsonPath) )
            {
                string jsonStr = ReadText(this.JsonPath);
                this._data = JsonUtility.FromJson<XTweenData>(jsonStr);
            }
            else
            {
                this._data = new XTweenData();
                this.Save();
            }
        }

        private void UpdateReleasePath()
        {
            string first = "Version(Alpha) ";
            string end = ".unitypackage)";
            string replace = "Version(Alpha) {VER} - [XTween_{VER}.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_{VER}.unitypackage)";
            replace = replace.Replace("{VER}", Data.version);
            string filePath = XTweenEditorManager.AbsPath + "/README.md";
            string content = ReadText(filePath);
            content = ReplaceTargetStringInContent(first, end, replace, content);
            WriteText(filePath, content);

            first = "<!--Version Start";
            end = "Version End-->";
            replace = 
            "<!--Version Start-->\n" +
            "<p>Version(Alpha) "+ Data.version +" - <a href=\"https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_"+ Data.version +".unitypackage\">XTween_"+ Data.version +".unitypackage</a></p>\n" +
            "<!--Version End-->";
            filePath = XTweenEditorManager.AbsPath + "/Export/index.html";
            content = ReadText(filePath);
            content = ReplaceTargetStringInContent(first, end, replace, content);
            WriteText(filePath, content);
        }
        
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
                string version = Data.version;
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
                exportRootPath = ExportDefaultPath;
                string addPath = "Assets/Toki/XTween/Export";
                exportPathList.Add(addPath);
            }
            else
            {
                this._xtweenVersion = this.ExportVersion;
                Data.version = this._xtweenVersion;
                Save();
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

        public void Save()
        {
            string jsonStr = JsonUtility.ToJson(this._data);
            WriteText(this.JsonPath, jsonStr);
        }
    }
}