using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallelTween : GroupTween
{
    protected List<IIAni> _destroyList;

	public ParallelTween( IAni[] targets, ITimer ticker, float position ) : base(ticker, position)
	{
		int l = targets.Length;
			
		_duration = 0;
			
		if (l > 0) {
			_a = targets[0] as IIAni;
			_duration = _a.Duration > _duration ? _a.Duration : _duration;
			if (l > 1) {
				_b = targets[1] as IIAni;
				_duration = _b.Duration > _duration ? _b.Duration : _duration;
				if (l > 2) {
					_c = targets[2] as IIAni;
					_duration = _c.Duration > _duration ? _c.Duration : _duration;
					if (l > 3) {
						_d = targets[3] as IIAni;
						_duration = _d.Duration > _duration ? _d.Duration : _duration;
						if (l > 4) {
							int length = l - 4;
							_targets = new IIAni[length];
							for (int i = 4; i < l; ++i) {
								IIAni t = targets[i] as IIAni;
								_targets[i - 4] = t;
								_duration = t.Duration > _duration ? t.Duration : _duration;
							}
						}
					}
				}
			}
		}
	}
		
	protected override void InternalUpdate( float time )
	{
		if (_a != null) {
			_a.UpdateTween(time);
			if (_b != null) {
				_b.UpdateTween(time);
				if (_c != null) {
					_c.UpdateTween(time);
					if (_d != null) {
						_d.UpdateTween(time);
						if (_targets != null) {
							IIAni[] targets = _targets;
							int l = targets.Length;
							for (int i = 0; i < l; ++i) {
								targets[i].UpdateTween(time);
							}
						}
					}
				}
			}
		}
	}
		
	protected override AbstractTween NewInstance()
	{
		List<IAni> targets = new List<IAni>();
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
			IIAni[] t = _targets;
			int l = t.Length;
			for (int i = 0; i < l; ++i) {
				targets.Add(t[i].Clone());
			}
		}
		return new ParallelTween(targets.ToArray(), _ticker, 0);
	}
}