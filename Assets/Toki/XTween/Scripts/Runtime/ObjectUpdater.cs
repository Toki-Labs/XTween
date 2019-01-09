using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class ObjectUpdater : AbstractUpdater
	{
		protected Dictionary<string, XObjectSet> _valueDic = new Dictionary<string, XObjectSet>();
		protected string[] _keys;
		protected int _keyLength;
		protected XObjectHash _source;
		protected Action<XObjectHash> _updateHandler;
			
		public override IClassicHandlable Start { set{}	}
		public override IClassicHandlable Finish
		{
			set
			{
				this._source = (XObjectHash)value;
			}
		}
			
		public Action<XObjectHash> UpdateHandler
		{
			set
			{
				this._updateHandler = value;
			}
		}

		protected void ComposeDic()
		{
			this._valueDic = _source.ObjectSet;
			this._keys = this._valueDic.Keys.GetArrayFromCollection<string>();
			this._keyLength = this._keys.Length;
		}

		public override void ResolveValues()
		{
			if( _resolvedValues ) return;
			this.ComposeDic();

			foreach ( var item in this._valueDic )
			{
				XObjectValues objValue = item.Value.value;
				item.Value.updater = objValue.controlPoint == null ? 
				(Action<float,float>)delegate( float invert, float factor )
				{
					objValue.current = objValue.start * invert + objValue.end * factor;
					item.Value.value = objValue;
				} :
				delegate( float invert, float factor )
				{
					objValue.current = Calcurate(objValue.controlPoint, objValue.start, objValue.end, invert, factor);
					item.Value.value = objValue;
				};
				item.Value.value = objValue;
			}
			this._resolvedValues = true;
		}
			
		protected override void UpdateObject()
		{
			for (int i = 0; i < this._keyLength; ++i)
			{
				this._valueDic[this._keys[i]].updater(_invert, _factor);
			}
			if( this._updateHandler != null ) this._updateHandler(_source);
		}

		public override void Release()
		{
			if( this._source != null ) this._source.PoolPush();
			foreach (var item in this._valueDic)
				item.Value.PoolPush();

			this.PoolPush();
		}

		public override void Dispose()
		{
			base.Dispose();
			this._valueDic.Clear();
			this._keys = null;
			this._keyLength = 0;
			this._source = null;
			this._updateHandler = null;
		}
	}

	public class ObjectUpdater<T> : ObjectUpdater
	{
		protected T _target;
		public T Target
		{
			get { return _target; }
			set
			{
				_target = value;
			}
		}

		public override void ResolveValues()
		{
			if( _resolvedValues ) return;
			if( IsNullTarget() ) throw new System.NullReferenceException("Tweener target is Null at start point");
			base.ComposeDic();

			Type type = typeof(T);
			foreach ( var item in this._valueDic )
			{
				XObjectValues objValue = item.Value.value;

				PropertyInfo property = typeof(T).GetProperty(item.Key);
				if( property == null )
				{
					throw new System.ArgumentException("There is no property named \""+ item.Key + "\".");
				}
				else
				{
					if(typeof(T).GetProperty(item.Key).GetType().Equals(typeof(float)))
					{
						Action<T,float> setter = (Action<T, float>)Delegate.CreateDelegate(
							typeof(Action<T, float>),
							typeof(T).GetProperty(item.Key).GetSetMethod()
						);
						PropertyInfo pInfo = type.GetProperty(item.Key);
						if( objValue.ContainStart )
						{
							setter(_target, objValue.start);
						}
						else
						{
							objValue.start = (float)pInfo.GetValue(_target, null);
						}
						item.Value.updater = objValue.controlPoint == null ? 
						(Action<float,float>)delegate( float invert, float factor )
						{
							if( IsNullTarget() ) return;
							
							objValue.current = objValue.start * invert + objValue.end * factor;
							item.Value.value = objValue;
							setter(_target, objValue.current);
						} :
						delegate( float invert, float factor )
						{
							if( IsNullTarget() ) return;

							objValue.current = Calcurate(objValue.controlPoint, objValue.start, objValue.end, invert, factor);
							item.Value.value = objValue;
							setter(_target, objValue.current);
						};
					}
					else if(typeof(T).GetProperty(item.Key).GetType().Equals(typeof(int)))
					{
						Action<T,int> setter = (Action<T, int>)Delegate.CreateDelegate(
							typeof(Action<T, int>),
							typeof(T).GetProperty(item.Key).GetSetMethod()
						);
						PropertyInfo pInfo = type.GetProperty(item.Key);
						if( objValue.ContainStart )
						{
							setter(_target, Mathf.RoundToInt(objValue.start));
						}
						else
						{
							objValue.start = (float)pInfo.GetValue(_target, null);
						}
						item.Value.updater = objValue.controlPoint == null ? 
						(Action<float,float>)delegate( float invert, float factor )
						{
							if( IsNullTarget() ) return;
							
							objValue.current = objValue.start * invert + objValue.end * factor;
							item.Value.value = objValue;
							setter(_target, Mathf.RoundToInt(objValue.current));
						} :
						delegate( float invert, float factor )
						{
							if( IsNullTarget() ) return;

							objValue.current = Calcurate(objValue.controlPoint, objValue.start, objValue.end, invert, factor);
							item.Value.value = objValue;
							setter(_target, Mathf.RoundToInt(objValue.current));
						};
					}
					else
					{
						throw new System.ArgumentException("Property type is must be int or float.");
					}
				}
			}
			_resolvedValues = true;
		}

		private bool IsNullTarget()
		{
			if(EqualityComparer<T>.Default.Equals(_target, default(T))) 
			{
				this._tweener.StopOnDestroy();					
				return true;
			}
			return false;
		}
			
		protected override void UpdateObject()
		{
			if( IsNullTarget() ) return;
			base.UpdateObject();
		}

		public override void Release()
		{
			if( this._source != null ) this._source.PoolPush();
			foreach (var item in this._valueDic)
				item.Value.PoolPush();

			this.PoolPush();
		}

		public override void Dispose()
		{
			base.Dispose();
			this._target = default(T);
		}
	}
}