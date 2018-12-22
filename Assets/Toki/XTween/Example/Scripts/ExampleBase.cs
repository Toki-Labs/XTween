/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class ExampleBase : MonoBehaviour
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
	protected IEnumerator _coroutineStart;
	protected IAni _tween;
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public TweenUIContainer uiContainer;
	public GameObject buttonBack;
    public GameObject target2D;
	public GameObject target3D;
	public GameObject container2D;
	public GameObject container3D;
		
	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
	IEnumerator Start()
	{
		yield return null;
		if( XExampleManager.Instance != null )
		{
			if( XExampleManager.Instance.ContainerName == "Main" )
				this.buttonBack.SetActive(true);
		}
		this.uiContainer.containerChangeHandler = this.ChangedContainer;
		yield return this.StartCoroutine(this.StartExample());
	}

	protected virtual IEnumerator StartExample()
	{
		yield return null;
	}
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	protected virtual IEnumerator CoroutineStart()
	{
		yield return null;
	}
	
	/************************************************************************
	*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	************************************************************************/
	private void ChangedContainer(string container)
	{
		bool is3D = container.Equals("3D");
		this.container2D.SetActive(!is3D);
		this.container3D.SetActive(is3D);
	}
    
	/************************************************************************
	*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	************************************************************************/
    public void ButtonMoveClickHandler()
    {
		if( this._coroutineStart != null )
		{
			this.StopCoroutine(this._coroutineStart);
			this._coroutineStart = null;
		}
		this._coroutineStart = this.CoroutineStart();
		this.StartCoroutine(this._coroutineStart);
    }

	public void ButtonBackClickHandler()
	{
		XExampleManager.Instance.UnloadScene();
	}
}