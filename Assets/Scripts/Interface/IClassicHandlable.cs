using UnityEngine;
using System;
using System.Collections.Generic;

public interface IClassicHandlable
{
    IExecutable onPlay
    {
        get;
        set;
    }
    IExecutable onStop
    {
        get;
        set;
    }
    IExecutable onUpdate
    {
        get;
        set;
    }
    IExecutable onComplete
    {
        get;
        set;
    }
    void CopyFrom( IClassicHandlable obj );
}