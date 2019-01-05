using UnityEngine;
using System;
using System.Collections.Generic;

namespace Toki.Tween
{
    public interface IClassicHandlable : IDisposable
    {
        IExecutable OnPlay
        {
            get;
            set;
        }
        IExecutable OnStop
        {
            get;
            set;
        }
        IExecutable OnUpdate
        {
            get;
            set;
        }
        IExecutable OnComplete
        {
            get;
            set;
        }
        void CopyFrom( IClassicHandlable obj );
    }
}