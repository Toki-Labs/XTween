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



Implementation
---
```csharp
// Simple use
XTween.To(gameObject, XHash.Position(600f,200f)).Play();


// Shortcut type
gameObject.To(XHash.Position(600f,200f)).Play();


// Use with coroutine
IEnumerator tweenCoroutine = CoroutineTween();
StartCoroutine(tweenCoroutine);

IEnumerator CoroutineTween()
{
	yield return XTween.To(gameObject, XHash.Position(600f,200f)).WaitForPlay();

	//or
	yield return gameObject.To(XHash.Position(600f,200f)).WaitForPlay();
}

StopCoroutine(tweenCoroutine);
```



Lastest Release
---
Version(Alpha) 0.0.91 - [XTween_0.0.91.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.91.unitypackage)



Performance
---
Tweener comparison when moving 1000 gameObjects.

__Garbage Alloc__
>__Start__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Start_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/DTween_Start_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/iTween_Start_1000.png)
>XTween rarely produces Garbage

>__End__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_End_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/DTween_End_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/iTween_End_1000.png)
>XTween is don't produces garbage.

__CPU Performance__
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Update_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/DTween_Update_1000.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/iTween_Update_1000.png)
>XTween is similar to DTween, but 2 times faster than i Tweener.

>Gabage Alloc Time/Cost|XTween|D Tween|I Tween
>----------------------|------|-------|-------
>Start|122B|0.6MB|4.7MB
>End|0B|0B|169.9KB
>Update|0.8ms|0.8ms|1.7ms
>Total|122B/0.8ms|0.6MB/0.8ms|4.9MB/1.7ms
> 
>XTween does not produce Garbage because it uses Pooling. and had good performance at cpu

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
- [ ] Support visual editor
- [ ] Support path editor by GUI



Position
---
```csharp
XTween.To(gameObject, XHash.New.AddX(600f).AddY(200f).AddZ(100f)).Play();

//or
gameObject.To(XHash.Position(600f,200f)).Play();
```

Scale
---
```csharp
XTween.To(gameObject, XHash.New.AddScaleX(1f).AddScaleY(1.5f).AddScaleZ(0.5f)).Play();

//or
gameObject.To(XHash.Scale(1f,1.5f)).Play();
```

Rotation
---
```csharp
XTween.To(gameObject, XHash.New.AddRotationZ(600f)).Play();

//or
gameObject.To(XHash.Rotation(60f,-180f,-45f)).Play();
```

Combination
---
```csharp
//Position, Scale, Rotation tween in same time, same easing
XTween.To(gameObject, XHash.New.AddX(600f).AddScaleX(200f).AddRotationZ(180f)).Play();

//or
gameObject.To(XHash.Position(0f,10f).AddScaleX(200f).AddRotationZ(60f)).Play();
```

Bezier
---
```csharp
XHash hash = XHash.Position(0f,0f).AddControlPointX(1000f).AddControlPoint(-500f);
XTween.To(gameObject, hash).Play();

//or
gameObject.To(hash).Play();
```

Value
---
```csharp
//Setter
XTween.ToValue(x=>camera3D.fieldOfView=x, 10f).Play();

//or multi type
XTween.ToValue(XObjectHash.New.Add("value", 50f, 10f), UpdateValue).Play();

void UpdateValue(XObjectHash hash)
{
	camera3D.fieldOfView = hash.Now("value");
}
```

Property
---
```csharp
camera3D.ToProperty("fieldOfView", 6f).Play();

//or
XTween.To<Camera>(camera3D, XObjectHash.New.Add("fieldOfView", 6f)).Play();
```

Event Handling
---
```csharp
XTween.To(gameObject, XHash.Position(600f,200f)).AddOnComplete(()=>Debug.Log("OnComplete")).Play();

//or
IXTween ani = XTween.To(gameObject, XHash.New.AddX(600f).AddY(200f));
ani.OnComplete = Executor<float>.New(OnTweenEnd, 10f);
ani.Play();

void OnTweenEnd(float value)
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
	yield return gameObject.To(XHash.Position(200f,50f,-1500f)).WaitForPlay();
	Debug.Log("On Complete First Tween");

	//Start other tween start at 0.3sec
	yield return gameObject.To(XHash.Position(100f,500f)).WaitForGotoAndPlay(0.3f);
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
	gameObject.To(XHash.Position(1000f,300f)), 
	gameObject.To(XHash.Scale(200f,200f))
).Play();
```

Parallel
---
```csharp
XTween.ParallelTweens
(	
	false, 
	gameObject.To(XHash.Position(1000f,300f)), 
	gameObject.To(XHash.Scale(200f,200f))
).Play();
```

UI
---
```csharp
XHash hashButton = XHash.New.AddX(400f).AddY(-250f).AddWidth(800f).AddHeight(400f);
XTween.To(button, hashButton).Play();

//or when stretch type
XHash hashDropdown = XHash.New.AddLeft(2000f).AddRight(300f).AddTop(500f).AddBottom(400f);
XTween.To(dropdown, hashDropdown).Play();
```

Color
---
```csharp
XTween.ToColor(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f)).Play();

//or when object has other type
XTween.ToColor<Image>(imageInstance, "color", XColorHash.New.AddRed(0.56f).AddGreen(0.83f)).Play();
```

Easing
---
```csharp
gameObject.To(XHash.Position(600f,200f), 1f, Ease.QuintOut).Play();
```

Custom Easing
---
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/EaseCurve.png)
>Customizable Easing, Support code hint for use of ease name, TopMenu -> Windows -> XTween Editor
```csharp
XHash hash = XHash.New.Position(200f,50f,-1500f)
	     .AddControlPointX(-1000f,550f).AddControlPointY(550f,-300f);
XTween.To(target3D, hash, Ease.Custom(EaseCustom.MyEasing)).Play();
```

Decorator
---
```csharp
IXTween tween = XTween.ToColor(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f));

//Delay Tweener
tween = XTween.Delay(tween, 1f/*Pre Delay*/, 1f/*Post Delay*/);

//Scale Tweener time
tween = XTween.Scale(tween, 2f/*Scale tweener time*/);

//Slice Tweener
tween = XTween.Slice(tween, 0.2f/*Slice start at*/, 0.8f/*Slice end*/, false/*is percent value?*/);

//Repeat Tweener
tween = XTween.Repeat(tween, 3/*3 time repeat*/);

//Reverse Tweener
tween = XTween.Reverse(tween);
tween.Play();
```

Reuse
---
XTween is basically autodispose. when the tweener is completed or stopped.
So, you should set to "Lock" for reuse tweener
```csharp
//This Tweener will not dispose when stop or complete.
IXTween tween = gameObject.To(XHash.Position(600f,200f)).Lock().Play();

//When the tweener after completed or stopped. You can reuse this.
tween.Reset(); //Set to play position 0;
tween.Play(); //Replay

//When you are not using twin anymore. You should "Release" this tween.
tween.Release();
tween = null;
```

Time Control
---
```csharp
//Start
IXTween tween = gameObject.To(XHash.Position(600f,200f)).Lock().Play();
//Stop at this position
tween.Stop();
//Resume
tween.Play();
//Move to 0.5sec and Stop
tween.GotoAndStop(0.5f); 
//Move to 0.3sec and play;
tween.GotoAndPlay(0.3f); 
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
