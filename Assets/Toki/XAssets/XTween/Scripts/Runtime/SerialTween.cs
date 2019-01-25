using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class SerialTween : GroupTween
	{
		private float _lastTime = 0f;
		private List<float> _durationList = new List<float>();
			
		public void Initialize( IXTween[] targets, ITimer ticker, float position )
		{
			base.Initialize(ticker, position);
			int l = targets.Length;
			_duration = 0;
				
			if (l > 0) {
				_a = targets[0] as IIXTween;
				_a.InitializeGroup();
				_duration += _a.Duration;
				_durationList.Add(_a.Duration);
				if (l > 1) {
					_b = targets[1] as IIXTween;
					_b.InitializeGroup();
					_duration += _b.Duration;
					_durationList.Add(_b.Duration);
					if (l > 2) {
						_c = targets[2] as IIXTween;
						_c.InitializeGroup();
						_duration += _c.Duration;
						_durationList.Add(_c.Duration);
						if (l > 3) {
							_d = targets[3] as IIXTween;
							_d.InitializeGroup();
							_duration += _d.Duration;
							_durationList.Add(_d.Duration);
							if (l > 4) {
								int length = l - 4;
								_targets = new IIXTween[length];
								for (int i = 4; i < l; ++i) 
								{
									IIXTween t = targets[i] as IIXTween;
									t.InitializeGroup();
									_duration += t.Duration;
									_durationList.Add(t.Duration);
									_targets[i - 4] = t;
								}
							}
						}
					}
				}
			}
		}

		protected override void InternalUpdate( float time )
		{
			float d = 0;
			float ld = 0;
			float lt = _lastTime;
			int l;
			int i;
			int index = 0;
			IIXTween t;
				
			if ((time - lt) >= 0) {
				if (_a != null) {
					d += _durationList[index];
					index++;
					if (lt <= d && ld <= time && !_a.Disposed) {
						_a.ResolveValues();
						_a.Tick(time - ld);
					}
					ld = d;
					if (_b != null) {
						d += _durationList[index];
						index++;
						if (lt <= d && ld <= time && !_b.Disposed) {
							_b.ResolveValues();
							_b.Tick(time - ld);
						}
						ld = d;
						if (_c != null) {
							d += _durationList[index];
							index++;
							if (lt <= d && ld <= time && !_c.Disposed) {
								_c.ResolveValues();
								_c.Tick(time - ld);
							}
							ld = d;
							if (_d != null) {
								d += _durationList[index];
								index++;
								if (lt <= d && ld <= time) {
									_d.ResolveValues();
									_d.Tick(time - ld);
								}
								ld = d;
								if (_targets != null) {
									l = _targets.Length;
									for (i = 0; i < l; ++i) {
										t = _targets[i];
										d += _durationList[index];
										index++;
										if (lt <= d && ld <= time) {
											t.ResolveValues();
											t.Tick(time - ld);
										}
										ld = d;
									}
								}
							}
						}
					}
				}
			}
			else {
				d = _duration;
				ld = d;
				if (_targets != null) {
					for (i = _targets.Length - 1; i >= 0; --i) {
						t = _targets[i];
						if (lt >= (d -= t.Duration) && ld >= time) {
							t.Tick(time - d);
						}
						ld = d;
					}
				}
				if (_d != null) {
					if (lt >= (d -= _d.Duration) && ld >= time) {
						_d.Tick(time - d);
					}
					ld = d;
				}
				if (_c != null) {
					if (lt >= (d -= _c.Duration) && ld >= time) {
						_c.Tick(time - d);
					}
					ld = d;
				}
				if (_b != null) {
					if (lt >= (d -= _b.Duration) && ld >= time) {
						_b.Tick(time - d);
					}
					ld = d;
				}
				if (_a != null) {
					if (lt >= (d -= _a.Duration) && ld >= time) {
						_a.Tick(time - d);
					}
					ld = d;
				}
			}
			_lastTime = time;
		}

		protected override void InternalRelease()
		{
			if( this._autoDispose )
			{
				this.PoolPush();
				if( this._classicHandlers != null ) 
					(this._classicHandlers as XEventHash).PoolPush();
			} 
		}

		public override void Dispose()
		{
			base.Dispose();
			this._lastTime = 0f;
			this._durationList.Clear();
		}
			
		protected override AbstractTween NewInstance()
		{
			List<IXTween> targets = new List<IXTween>();
			if (_a != null) {
				targets.Add(_a.Clone());
			}
			if (_b != null) {
				targets.Add(_b.Clone());
			}
			if (_c != null) {
				targets.Add(_c.Clone());
			}
			if (_d != null) {
				targets.Add(_d.Clone());
			}
			if (_targets != null) {
				IIXTween[] t = _targets;
				int l = t.Length;
				for (int i = 0; i < l; ++i) {
					targets.Add(t[i].Clone());
				}
			}
			SerialTween tween = Pool<SerialTween>.Pop();
			tween.Initialize(targets.ToArray(), Ticker, 0);
			return tween;
		}
	}
}