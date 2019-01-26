/**********************************************************************************
/*		File Name 		: XTweenVersionController.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2019-1-3
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Toki.Common;

namespace Toki.Tween
{
	public class XTweenVersionController : VersionController
	{
		/************************************************************************
		*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
		************************************************************************/
		public static XTweenVersionController To
		{
			get
			{
				VersionController controller;
				if( (controller = VersionController.Get(NAME)) == null )
				{
					controller = new XTweenVersionController(NAME);
					VersionController.Push(controller);
				}
				return controller as XTweenVersionController;
			}
		}
		
		/************************************************************************
		*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
		************************************************************************/
			
		/************************************************************************
		*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
		************************************************************************/
		public const string NAME = "XTween";
		
		/************************************************************************
		*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
		************************************************************************/
		public override string URL
		{
			get
			{
				return "https://toki-labs.github.io/XTween/Assets/Toki/XAssets/XTween/Scripts/Editor/xtween_version.json";
			}
		}

		public override string URLPackage
		{
			get
			{
				return "https://github.com/Toki-Labs/XTween/raw/master/Bin/";
			}
		}
		
		
		/************************************************************************
		*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
		************************************************************************/
		
		/************************************************************************
		*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
		************************************************************************/
		public XTweenVersionController(string assetName ) : base(assetName)
		{
		}
		
		/************************************************************************
		*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
		************************************************************************/
		protected override void ImportPrevProcess()
		{
			string absPath = SystemUtil.AbsPath;
			string nameStartPath = absPath + "/Assets/Toki/XAssets/XTween/Scripts/EaseCustom.cs";
			string nameDestPath = Path.Combine( VersionController.TempPath, "EaseCustomTemp" );
			File.Copy(nameStartPath, nameDestPath);
			string rootPath = absPath + "/Assets/Toki/XAssets/XTween/Scripts/";
			string[] dirs = new string[]{rootPath + "Editor", rootPath + "Runtime"};
			foreach ( var path in dirs )
			{
				Directory.Delete(path,true);
			}
		}

		protected override void ImportPostProcess()
		{
			string nameStartPath = SystemUtil.AbsPath + "/Assets/Toki/XAssets/XTween/Scripts/EaseCustom.cs";
			string tempNamePath = Path.Combine(VersionController.TempPath, "EaseCustomTemp");
			if( File.Exists(tempNamePath) )
			{
				File.Copy(tempNamePath, nameStartPath, true);
				AssetDatabase.Refresh();
			}
		}
		
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