/**********************************************************************************
/*		File Name 		: XTweenEditorWindow.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.IO;

namespace Toki.Tween
{
    public class XTweenEditorWindow : EditorWindow
    {
        /************************************************************************
        *	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
        ************************************************************************/
        [MenuItem("Window/XTween Export #%8",priority=15)]
        public static void XTweenWindow()
        {
            EditorWindow.GetWindow<XTweenEditorWindow>(false, "XTween Export", true);
        }

        
        /************************************************************************
        *	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
        ************************************************************************/
        public string _xtweenVersion;
        
        /************************************************************************
        *	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
        ************************************************************************/
        
        
        
        /************************************************************************
        *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
        ************************************************************************/
        
        
        /************************************************************************
        *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
        ************************************************************************/
        void OnEnable()
        {
            this._xtweenVersion = XTweenExporter.Instance.Data.version;
        }

        void OnGUI()
        {
            XTweenData data = XTweenExporter.Instance.Data;
            GUILayout.BeginVertical();

            GUILayout.BeginVertical("Box");
            {
                GUILayout.Space(3f);
                GUILayout.Label("Export", "BoldLabel");
                GUILayout.Space(3f);
            }
            GUILayout.EndVertical();

            GUILayout.Space(10f);

            bool changed = false;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Version", GUILayout.Width(110f));
            this._xtweenVersion = EditorGUILayout.TextField(this._xtweenVersion);
            if (this._xtweenVersion != data.version)
            {
                changed = true;
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Next Version", GUILayout.Width(110f));
            GUILayout.Label(XTweenExporter.Instance.ExportVersion);
            GUI.backgroundColor = (changed) ? Color.green : Color.gray;
            if (GUILayout.Button("Save", GUILayout.Width(100f)))
            {
                data.version = this._xtweenVersion;
                XTweenExporter.Instance.Save();
                changed = false;
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.Space(10f);

            if (GUILayout.Button("Export Core", GUILayout.Height(25f)))
            {
                string exportVersion = XTweenExporter.Instance.ExportVersion;
                XTweenExporter.Instance.Export(false);
                this._xtweenVersion = exportVersion;
            }


            GUI.backgroundColor = Color.green;
            if(GUILayout.Button("Export All", GUILayout.Height(30f)))
            {
                XTweenExporter.Instance.Export(true);
            }
            GUI.backgroundColor = Color.white;

            GUILayout.Space(20f);

            if(GUILayout.Button("Test", GUILayout.Height(30f)))
            {
                // XTweenExporter.UpdateReleasePath();
            }

            GUILayout.Space(10f);

            GUILayout.EndVertical();
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
        
        
    }
}