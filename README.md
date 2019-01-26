XTween - Tweener for Unity
===
Created by Lee Dong-Myung(Toki-Labs)

XTween?
---
XTween is Tweener library for unity using by code, It has powerful performance and easy to implement.

XTween is
1. Can make tweener by few code (easy to learn and good readablity)
2. Provide code hint. (You just start with "To" than You can use tweener)
3. Easy to control event. (Support Anonymous Method)
4. Support coroutine. (Easy to combine with other instruction)
5. Support custom easing.
6. Can make instance. This good at resuablity and time control.
7. Less use of memory and cpu. (XTween is don't create MonoBehaviour each by tween)
8. Support in editor mode(Not Play mode) and Available on all types of platforms

You can check example at [XTween Example](http://toki-labs.com/xtween)

__Ps. Because it is still in the development stage(ALPHA), the interface can change frequently. At this stage, you can use it for testing purposes. After in BETA, You can use it stably.__

Implementation
---
```csharp
// Shortcut type
gameObject.ToPosition2D(600f, 200f, 1f).Play();

// Universal type
XTween.To(gameObject, XHash.Position(600f, 200f), 1f).Play();

// Use with coroutine
StartCoroutine(CoroutineTween());

IEnumerator CoroutineTween()
{
	yield return gameObject.ToPosition2D(600f, 200f, 1f).WaitForPlay();

	//or
	yield return XTween.To(gameObject, XHash.Position(600f, 200f), 1f).WaitForPlay();
}
```



Lastest Release
---
Version(Alpha) 0.0.114 - [XTween_0.0.114.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.114.unitypackage)



Performance
---
Tweener comparison when moving 1000 gameObjects. set x, y, z and use elastic easing.

__Garbage Alloc__
>__Start__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/Tween_Start.jpg)
>XTween rarely produces Garbage

>__End__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/Tween_End.jpg)
>XTween dose not produces garbage.

__CPU Performance__
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/Tween_Update.jpg)
>XTween is similar with D Tween, but 2 times faster than i Tweener.

>Gabage Alloc Time/Cost|XTween|D Tween|L Tween|I Tween
>----------------------|------|-------|-------|-------
>Start|122B|0.6MB|406.4KB|4.7MB
>End|0B|0B|0B|169.9KB
>Update|0.8ms|0.8ms|1.2ms|1.7ms
>Total|122B/0.8ms|0.6MB/0.8ms|406.4KB/1.2ms|4.9MB/1.7ms
> 
>XTween does not produce Garbage because it uses Pooling. and XTween is good performance at cpu.

__Code Compare__
 >__Other Tweener Code__
 >
 >![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_Code.JPG)
 
 >__XTween Code__
 >
 >![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Code.JPG)


Road Map
---
- [x] Support property tween
- [x] Support UI RectTransform
- [x] Performance check & polishing effciency
- [x] Support Easing Edit in UI Graph
- [x] Stabilize Interface
- [x] Decorator Combine with IXTween Interface
- [ ] Documentaion
- [ ] Specific examples
- [ ] Support visual editor
- [ ] Support path editor by GUI



Position
---
```csharp
gameObject.ToPosition2D(600f, 200f, 1f).Play();

//Universal type
XTween.To(gameObject, XHash.New.AddX(600f).AddY(200f).AddZ(100f), 1f).Play();
```

Scale
---
```csharp
gameObject.ToScale2D(1f, 1.5f, 1f).Play();

//Universal type
XTween.To(gameObject, XHash.New.AddScaleX(1f).AddScaleY(1.5f).AddScaleZ(0.5f), 1f).Play();
```

Rotation
---
```csharp
gameObject.ToRotation3D(60f, -180f, -45f, 1f).Play();

//Universal type
XTween.To(gameObject, XHash.New.AddRotationZ(600f), 1f).Play();
```

Combination
---
```csharp
//Position, Scale, Rotation tween in same time, same easing
gameObject.To(XHash.Position(0f,10f).AddScaleX(200f).AddRotationZ(60f), 1f).Play();

//Universal type
XTween.To(gameObject, XHash.New.AddX(600f).AddScaleX(200f).AddRotationZ(180f), 1f).Play();
```

Bezier
---
```csharp
XHash hash = XHash.Position(0f,0f).AddControlPointX(1000f).AddControlPointZ(-500f);
gameObject.To(hash, 1f).Play();

//Universal type
XTween.To(gameObject, hash, 1f).Play();
```

Value
---
```csharp
//Setter
XTween.ToValue(x=>camera3D.fieldOfView=x, 10f, 1f).Play();

//or Multi value type
XObjectHash hash = XObjectHash.New.Add("value0", 50f, 10f).Add("value1", 0f, 10f);
XTween.ToValueMulti(hash, UpdateValue, 1f).Play();

void UpdateValue(XObjectHash hash)
{
	camera3D.fieldOfView = hash.Now("value0");
	Debug.Log( hash.Now("value1") );
}
```

Property
---
```csharp
camera3D.ToProperty("fieldOfView", 6f, 1f).Play();
```

Event Handling
---
```csharp
gameObject.ToPosition2D(600f, 200f, 1f)
          .AddOnComplete(()=>Debug.Log("OnComplete")).Play();

void OnTweenEnd(GameObject tweenTarget)
{
	Debug.Log(value);
}
```

Coroutine
---
Support use with coroutine
```csharp
//Start tween
IEnumerator tweenCoroutine = CoroutineTween();
StartCoroutine(tweenCoroutine);

IEnumerator CoroutineTween()
{
	yield return gameObject.ToPosition3D(200f, 50f, -1500f, 1f).WaitForPlay();
	Debug.Log("On Complete First Tween");

	//Start other tween start at 0.3sec
	yield return gameObject.ToPosition2D(100f, 500f, 1f).WaitForPlay(0.3f);
	Debug.Log("On Complete Second Tween");
}

//Stop tween;
StopCoroutine(tweenCoroutine);
```

Serial
---
```csharp
XTween.SerialTweens
(	
	false, 
	gameObject.ToPosition2D(1000f, 300f, 1f), 
	gameObject.ToScale2D(200f, 200f, 1f)
).Play();
```

Parallel
---
```csharp
XTween.ParallelTweens
(	
	false, 
	gameObject.ToPosition2D(1000f, 300f, 1f), 
	gameObject.ToScale(200f, 200f, 1f)
).Play();
```

UI
---
```csharp
XHash hashButton = XHash.New.AddX(400f).AddY(-250f).AddWidth(800f).AddHeight(400f);
XTween.To(button, hashButton, 1f).Play();

//or when stretch type
XHash hashDropdown = XHash.New.AddLeft(2000f).AddRight(300f).AddTop(500f).AddBottom(400f);
XTween.To(dropdown, hashDropdown, 1f).Play();
```

Color
---
```csharp
XTween.ToColor(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f), 1f).Play();

//or when object has other type
XColorHash hash = XColorHash.New.AddRed(0.56f).AddGreen(0.83f);
XTween.ToColor<Image>(imageInstance, "color", hash, 1f).Play();
```

Easing
---
```csharp
gameObject.ToPosition2D(600f, 200f, 1f, Ease.QuintOut).Play();
```

Custom Easing
---
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/EaseCurve.png)
>Customizable Easing, Support code hint for use of ease name, TopMenu -> Windows -> XTween Editor
```csharp
XHash hash = XHash.Position(200f, 50f, -1500f)
	     .AddControlPointX(-1000f,550f).AddControlPointY(550f,-300f);
XTween.To(target3D, hash, 1f, Ease.Custom(EaseCustom.MyEasing)).Play();
```

Decorator
---
```csharp
IXTween tween = XTween.ToColor(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f), 1f);

//Delay Tweener
tween.SetDelay(tween, 1f/*Pre Delay*/, 1f/*Post Delay*/);

//Scale Tweener time
tween.SetScale(tween, 2f/*Scale tweener time*/);

//Repeat Tweener
tween.SetRepeat(tween, 3/*3 time repeat*/);

//Reverse Tweener
tween.SetReverse(tween);
tween.Play();
```

Reuse
---
XTween is basically autodispose. when the tweener is completed or stopped.
So, you should set to "Lock" for reuse tweener
```csharp
//This Tweener will not dispose when stop or complete.
IXTween tween = gameObject.ToPosition2D(600f, 200f, 1f).SetLock().Play();

//When the tweener after completed or stopped. You can reuse this.
tween.Play(0f); //Replay

//When you are not using twin anymore. You should "Release" this tween.
tween.Release();
tween = null;
```

Time Control
---
```csharp
//Start
IXTween tween = gameObject.ToPosition2D(600f, 200f, 1f).Lock().Play();
//Stop at this position
tween.Stop();
//Resume
tween.Play();
//Move to 0.5sec and Stop
tween.GotoAndStop(0.5f); 
//Move to 0.3sec and play;
tween.Play(0.3f); 
//Position set to 0 and Stop
tween.Reset();
```

Author Info
---
Lee Dong-Myung(Tok-Labs) is a software developer in Korea.

Blog: http://blog.toki-labs.com (Korean)
Mail: dongmyung.5152@gmail.com

License
---
This library is under the MIT License.

Some code is borrowed from BetweenAS3.
