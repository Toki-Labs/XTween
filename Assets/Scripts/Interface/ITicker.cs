using System;

public interface ITicker
{
	float time
	{
		get;
		set;
	}
		
	void addTickerListener( TickerListener listener );
	void removeTickerListener( TickerListener listener );
	void start();
	void stop();
}
