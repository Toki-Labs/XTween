/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class XExampleManager : MonoBehaviour
{
	/************************************************************************
	*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	************************************************************************/
	private static XExampleManager _instance;
	
	/************************************************************************
	*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	************************************************************************/
	public static XExampleManager Instance
	{
		get
		{
			return _instance;
		}
	}
	
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
	private List<Button> _buttonList;
	private string[] _scenes = new string[]
	{
		"Example_Position", "Example_Scale", "Example_Angle",
		"Example_Combination", "Example_Bezier", "Example_Value",
		"Example_Event", "Example_Serial", "Example_Parallel",
	};
	private string _containerName = "Main";
	private string _currentScene;

	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text text;
	public GameObject uiContainer;
	public GameObject buttonContainer;

	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	public string ContainerName
	{
		get
		{
			return this._containerName;
		}
	}
	
	/************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
	void Awake()
	{
		_instance = this;
		this._buttonList = new List<Button>( this.buttonContainer.GetComponentsInChildren<Button>() );
		foreach( var button in this._buttonList )
		{
			button.onClick.AddListener( () => this.ButtonSceneLoadClickHandler(button) );
		}
	}
	
	IEnumerator Start()
	{
		yield return null;
		if( this._containerName != "Main" )
			this.LoadScene(this._containerName);
	}
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	************************************************************************/
	private void LoadScene(string scene)
	{
		this._currentScene = scene;
		SceneManager.LoadScene(scene,LoadSceneMode.Additive);
		this.uiContainer.SetActive(false);
	}
    
	/************************************************************************
	*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	************************************************************************/
	public void UnloadScene()
	{
		if( this._currentScene != null )
		{
			SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(this._currentScene));
			this.uiContainer.SetActive(true);		
			this._currentScene = null;
		}
	}

	public void ButtonSceneLoadClickHandler(Button button)
	{
		this._currentScene = this._scenes[this._buttonList.IndexOf(button)];
		this.LoadScene(this._currentScene);		
	}

    public void Receiver(string message)
    {
        this._containerName = message;
    }
}