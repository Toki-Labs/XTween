/**********************************************************************************
/*		File Name 		: GroupTween.cs
/*		Author 			: Lee Dong-Myung
/*		Description 	: 
/*		Created Date 	: 2014-4-22
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class GroupTween : AbstractTween, IIXTweenGroup
	{
		/************************************************************************
			*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
			************************************************************************/
			
			
		/************************************************************************
			*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
			************************************************************************/
		protected IIXTween _a;
		protected IIXTween _b;
		protected IIXTween _c;
		protected IIXTween _d;
		protected IIXTween[] _targets;
			
			
		/************************************************************************
			*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
			************************************************************************/
			
			
		/************************************************************************
			*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
			************************************************************************/


		/************************************************************************
			*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
			************************************************************************/
		public override void Initialize( ITimer ticker, float position )
		{
			base.Initialize(ticker, position);
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
		public IIXTween[] tweens
		{
			get
			{
				return _targets;
			}
		}
			
		public bool Contains( IXTween tween)
		{
			if (tween == null) {
				return false;
			}
			if (_a == tween) {
				return true;
			}
			if (_b == tween) {
				return true;
			}
			if (_c == tween) {
				return true;
			}
			if (_d == tween) {
				return true;
			}
			if (_targets != null) {
				return System.Array.IndexOf<IIXTween>( _targets, tween as IIXTween) != -1;
			}
			return false;
		}
			
		public IXTween GetTweenAt( int index )
		{
			if (index < 0) {
				return null;
			}
			if (index == 0) {
				return _a;
			}
			if (index == 1) {
				return _b;
			}
			if (index == 2) {
				return _c;
			}
			if (index == 3) {
				return _d;
			}
			if (_targets != null) {
				if (index - 4 < _targets.Length) {
					return _targets[index - 4];
				}
			}
			return null;
		}
			
		public int GetTweenIndex( IXTween tween )
		{
			if (tween == null) {
				return -1;
			}
			if (_a == tween) {
				return 0;
			}
			if (_b == tween) {
				return 1;
			}
			if (_c == tween) {
				return 2;
			}
			if (_d == tween) {
				return 3;
			}
			if (_targets != null) {
				int i = System.Array.IndexOf<IIXTween>(_targets, tween as IIXTween);
				if (i != -1) {
					return i + 4;
				}
			}
			return -1;
		}

		public override void ResolveValues()
		{
			int l;
			int i;
			IIXTween t;
			if (_a != null)
			{
				_a.ResolveValues();
				if (_b != null)
				{
					_b.ResolveValues();
					if (_c != null)
					{
						_c.ResolveValues();
						if (_d != null)
						{
							_d.ResolveValues();
							if (_targets != null)
							{
								l = _targets.Length;
								for (i = 0; i < l; ++i)
								{
									t = _targets[i];
									t.ResolveValues();
								}
							}
						}
					}
				}
			}
		}

		public override IXTween Play()
		{
			int l;
			int i;
			IIXTween t;

			if (_a != null)
			{
				_a.IntializeGroup();
				if (_b != null)
				{
					_b.IntializeGroup();
					if (_c != null)
					{
						_c.IntializeGroup();
						if (_d != null)
						{
							_d.IntializeGroup();
							if (_targets != null)
							{
								l = _targets.Length;
								for (i = 0; i < l; ++i)
								{
									t = _targets[i];
									t.IntializeGroup();
								}
							}
						}
					}
				}
			}

			base.Play();
			return this;
		}

		public virtual void GroupStopOnDestroy( IIXTween target )
		{

		}
			
	}
}