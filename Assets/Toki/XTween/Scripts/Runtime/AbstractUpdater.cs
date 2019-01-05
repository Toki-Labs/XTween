using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
    public abstract class AbstractUpdater : IUpdating
    {
        protected float _invert;
        protected float _factor = 0f;
        protected bool _resolvedValues = false;
        protected Action _stopOnDestroyHandler;
        public virtual Action StopOnDestroyHandler
        {
            set { _stopOnDestroyHandler = value; }
        }
        public abstract IClassicHandlable Start {set;}
        public abstract IClassicHandlable Finish {set;}
        public virtual void Updating( float factor )
        {
            _invert = 1.0f - factor;
            _factor = factor;
            UpdateObject();
        }
        protected float Calcurate( float[] cpVec, float source, float finish )
        {
            float l;
            int ip;
            float it;
            float p1;
            float p2;
            float result;

            if (_factor != 1f)
            {
                if ((l = cpVec.Length) == 1)
                {
                    result = source + _factor * (2 * _invert * (cpVec[0] - source) + _factor * (finish - source));
                }
                else
                {
                    ip = (int)(_factor * l) >> 0;
                    it = (_factor - (ip * (1 / l))) * l;
                    if (ip == 0)
                    {
                        p1 = source;
                        p2 = (cpVec[0] + cpVec[1]) * 0.5f;
                    }
                    else if (ip == (l - 1))
                    {
                        p1 = (cpVec[ip - 1] + cpVec[ip]) * 0.5f;
                        p2 = finish;
                    }
                    else
                    {
                        if( ip >= l )
                        {
                            ip = (int)l - 1;
                            return source + _factor * (2 * _invert * (cpVec[ip] - source) + _factor * (finish - source));
                        }
                        else
                        {
                            p1 = (cpVec[ip - 1] + cpVec[ip]) * 0.5f;
                            p2 = (cpVec[ip] + cpVec[ip + 1]) * 0.5f;
                        }
                    }
                    result = p1 + it * (2 * (1 - it) * (cpVec[ip] - p1) + it * (p2 - p1));
                }
            }
            else
            {
                result = source * _invert + finish * _factor;
            }
            return result;
        }
            
        public abstract void ResolveValues();
        protected abstract void UpdateObject();
        public abstract void Release();
        public virtual void Dispose()
        {
            this._invert = 0f;
            this._factor = 0f;
            this._resolvedValues = false;
            this._stopOnDestroyHandler = null;
        }
    }
}