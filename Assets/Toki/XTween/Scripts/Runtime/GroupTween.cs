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

		public override IXTween SetLock()
		{
			if (_a != null) {
				_a.SetLock();
			}
			if (_b != null) {
				_b.SetLock();
			}
			if (_c != null) {
				_c.SetLock();
			}
			if (_d != null) {
				_d.SetLock();
			}
			if (_targets != null) {
				IIXTween[] t = _targets;
				int l = t.Length;
				for (int i = 0; i < l; ++i) {
					t[i].SetLock();
				}
			}
			return base.SetLock();
		}

		public override void Dispose()
		{
			base.Dispose();
			if( this._a != null ) if( !this._a.Disposed ) this._a.Release();
			if( this._b != null ) if( !this._b.Disposed ) this._b.Release();
			if( this._c != null ) if( !this._c.Disposed ) this._c.Release();
			if( this._d != null ) if( !this._d.Disposed ) this._d.Release();
			if( this._targets != null )
			{
				int length = this._targets.Length;
				for ( int i = 0; i < length; ++i )
				{
					if( !this._targets[i].Disposed ) 
						this._targets[i].Release();
				}
			}
			this._a = null;
			this._b = null;
			this._c = null;
			this._d = null;
			this._targets = null;
		}
	}
}