/**********************************************************************************
/*		File Name 		: ModuleTest.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-28
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Security.Cryptography;
using System.Text;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

public class ModuleTest : EditorWindowModuleBase
{
    /************************************************************************
    *	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
    ************************************************************************/
     

    /************************************************************************
	*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	************************************************************************/
    


    /************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/


    /************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/


    /************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
    public override string ModuleName
    {
        get
        {
            return "Test";
        }
    }


    /************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/


    /************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/


    /************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
    public override void Initialize(EditorWindowBase window)
    {
        base.Initialize(window);
    }

    public override void OnEnable()
    {
    }

    private int _count = 0;
    public override void OnGUI()
    {
        base.OnGUI();

        if (GUILayout.Button("Test0", GUILayout.Height(40f)))
        {
            /*SlackFile file = new SlackFile();
            file.title = "Title Test";
            file.fileName = "fileName Test";
            file.initial_comment = "Initial comment";
            file.filePath = SystemUtil.absPath + "/Assets/Images/test.jpg";
            SlackManager.To.Send("app_notifier", file);*/
            
        }

        if (GUILayout.Button("Test1", GUILayout.Height(40f)))
        {
        }
    }
    
    public override void OnDestroy()
    {
        
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