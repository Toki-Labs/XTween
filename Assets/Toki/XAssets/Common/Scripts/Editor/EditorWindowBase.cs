/**********************************************************************************
/*		File Name 		: EditorWindowBase.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class EditorWindowBase : EditorWindow
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
    protected List<EditorWindowModuleBase> _moduleList = new List<EditorWindowModuleBase>();
    protected bool _drawable = true;
    protected bool _showHeader = true;
	
	
	/************************************************************************
	 *	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	 ************************************************************************/
	
	
	/************************************************************************
	 *	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	 ************************************************************************/
	public virtual void Initialize()
	{   
        int length = this._moduleList.Count;
        for ( int i = 0; i < length; ++i )
        {
            this._moduleList[i].Initialize( this );
        }
	}

    public virtual void Enable()
    {
		int length = this._moduleList.Count;
        for ( int i = 0; i < length; ++i )
        {
            this._moduleList[i].OnEnable();
        }	
    }

    public virtual void UpdateGUI()
    {
        if( this._drawable )
        {
            try
            {
                GUILayout.BeginVertical();
                GUILayout.Height(20f);
                int length = this._moduleList.Count;
                for (int i = 0; i < length; ++i)
                {
                    if( this._showHeader )
                    {
                        GUILayout.BeginVertical("Box");
                        {
                            GUILayout.Space(3f);
                            GUILayout.Label(this._moduleList[i].ModuleName, "BoldLabel");
                            GUILayout.Space(3f);
                        }
                        GUILayout.EndVertical();
                    }

                    GUILayout.Space(0f);

                    GUILayout.BeginVertical("Box");
                    GUILayout.Space(5f);
                    this._moduleList[i].OnEvent();
                    this._moduleList[i].OnGUI();                
                    GUILayout.Space(5f); 
                    GUILayout.EndVertical();
                    // GUILayout.Space(5f);
                }
                GUILayout.EndVertical();
            }
            catch( System.Exception e ) {}
        }
    }

    public virtual void Destroy()
    {
        int length = this._moduleList.Count;
        for (int i = 0; i < length; ++i)
        {
            this._moduleList[i].OnDestroy();
        }
        this._moduleList.Clear();
    }
	
	
	/************************************************************************
	 *	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	 ************************************************************************/
    public EditorWindowBase()
    {
        this.Initialize();
    }

    void OnEnable()
	{
        this.Enable();
	}

	void OnGUI()
	{
        this.UpdateGUI();
	}

	void OnDestroy()
	{

        this.Destroy();
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
    public void AddModule( params EditorWindowModuleBase[] modules )
    {
        this._moduleList = new List<EditorWindowModuleBase>( modules );
    }

    public bool ContainModule( EditorWindowModuleBase module )
    {
        return this._moduleList.Contains( module );
    }
	
	
}