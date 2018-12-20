using System;

namespace Toki.Interfaces
{
	public interface IUIKit : INative
	{
		void SendEmail( string reciever, string title, string body );
		void Alert( string message, string title = null, Action<string> resultListener = null, params string[] buttons );
	}
}