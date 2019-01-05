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
Version(Alpha) 0.0.86 - [XTween_0.0.86.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.86.unitypackage)



Performance
---
Performance compare with other tweener.

__Garbage Alloc__
>__Start__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_Start.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Start.png)
>Garbage produce 1/74 than other tweener, Creation cost 1/3 than other tweener.

>__End__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_End.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_End.png)
>Garbage produe 1/50 than other tweener.

>Gabage Alloc Time|Other Tweener|XTween
>--------|-----------|------
>Start|185.5K|2.5KB
>End|1KB|0.02KB
>Total|186.5KB|2.52KB
> 
>Garbage produce 1/74 than other tweener.

__CPU Performance__
>Compare when move 100 gameObject
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_Update.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Update.png)
>XTween is 2 times faster than other tweener.

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

//When end tweener
tween.GotoAndStop(0.5f); //Move to Tweener's 0.5sec
tween.GotoAndPlay(0.3f); //Move to 0.3sec and play;
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
