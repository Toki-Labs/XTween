using System;

public interface IIAni : IAni
{
	ITimer ticker
	{
		get;
	}
    Action decoratorStopOnDestroy
    {
        set;
    }

    void IntializeGroup();
	void StartPlay();
	void StartStop();
    void ResolveValues();
	void UpdateTween( float time );
}