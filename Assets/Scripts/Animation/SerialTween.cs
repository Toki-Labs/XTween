using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SerialTween : GroupTween
{
	private float _lastTime = 0;
		
	public SerialTween( IAni[] targets, ITimer ticker, float position ) : base(ticker, position)
	{
		int l = targets.Length;
			
		_duration = 0;
			
		if (l > 0) {
			_a = targets[0] as IIAni;
            _a.IntializeGroup();
			_duration += _a.duration;
			if (l > 1) {
				_b = targets[1] as IIAni;
                _b.IntializeGroup();
                _duration += _b.duration;
				if (l > 2) {
					_c = targets[2] as IIAni;
                    _c.IntializeGroup();
                    _duration += _c.duration;
					if (l > 3) {
						_d = targets[3] as IIAni;
                        _d.IntializeGroup();
                        _duration += _d.duration;
						if (l > 4) {
							int length = l - 4;
							_targets = new IIAni[length];
							for (int i = 4; i < l; ++i) {
								IIAni t = targets[i] as IIAni;
                                t.IntializeGroup();
                                _targets[i - 4] = t;
								_duration += t.duration;
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
		IIAni t;
			
		if ((time - lt) >= 0) {
			if (_a != null) {
				if (lt <= (d += _a.duration) && ld <= time) {
					_a.UpdateTween(time - ld);
				}
				ld = d;
				if (_b != null) {
					if (lt <= (d += _b.duration) && ld <= time) {
						_b.UpdateTween(time - ld);
					}
					ld = d;
					if (_c != null) {
						if (lt <= (d += _c.duration) && ld <= time) {
							_c.UpdateTween(time - ld);
						}
						ld = d;
						if (_d != null) {
							if (lt <= (d += _d.duration) && ld <= time) {
								_d.UpdateTween(time - ld);
							}
							ld = d;
							if (_targets != null) {
								l = _targets.Length;
								for (i = 0; i < l; ++i) {
									t = _targets[i];
									if (lt <= (d += t.duration) && ld <= time) {
										t.UpdateTween(time - ld);
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
					if (lt >= (d -= t.duration) && ld >= time) {
						t.UpdateTween(time - d);
					}
					ld = d;
				}
			}
			if (_d != null) {
				if (lt >= (d -= _d.duration) && ld >= time) {
					_d.UpdateTween(time - d);
				}
				ld = d;
			}
			if (_c != null) {
				if (lt >= (d -= _c.duration) && ld >= time) {
					_c.UpdateTween(time - d);
				}
				ld = d;
			}
			if (_b != null) {
				if (lt >= (d -= _b.duration) && ld >= time) {
					_b.UpdateTween(time - d);
				}
				ld = d;
			}
			if (_a != null) {
				if (lt >= (d -= _a.duration) && ld >= time) {
					_a.UpdateTween(time - d);
				}
				ld = d;
			}
		}
		_lastTime = time;
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
		return new SerialTween(targets.ToArray(), ticker, 0);
	}
}