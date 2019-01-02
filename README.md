XTween - Tweener for Unity
===
Created by Lee Dong-Myung(Toki-Labs)

XTween란?
---
유니티에서 코드로 애니매이션 트윈을 구현하기 위한 라이브러리로, 강력한 퍼포먼스와 쉬운 사용방법으로 원하는 결과물을 쉽게 구현가능하게 합니다.

XTween은?
1. MonoBehaviour를 객체마다 생성시키지 않아 가볍다.
2. 몇줄의 코드로 Tween을 구현가능하다.
3. Tween하고자 하는 Object의 명확한 코드힌트를 제공하여 사용자 실수가 없다.
4. 이벤트 핸들링이 손쉽다. (별도의 메서드 선언없이 Anonymous Method지원)
5. Coroutine의 지원으로 다른 Instruction과의 조합이 용이하다.
6. Custom Easing의 지원으로 원하는 애니매이션을 얼마든지 만들어 낼 수있다.
7. 생성한 트윈은 Instance화 할 수 있어 재사용성이 높고 시간컨트롤이 용이하다.
8. 강력한 퍼포먼스로 메모리와 CPU사용량이 적다.
9. 에디터 모드에서 사용은 물론, 모든 플랫폼에서 사용가능하다.

예제는 [XTween Example](http://toki-labs.com/xtween)에서 확인하실 수 있습니다.



Implementation
---
```csharp
XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f)).Play();
```


Performance
---
Unity에서 보편적으로 쓰이는 Tween과 XTween과의 성능비교

__Garbage Alloc__
>__Start__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_Start.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Start.png)
>XTween이 74.2배 적은 Garbage 생성, 객체 생성시 비용 또한 3.4배 적음

>__End__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_End.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_End.png)
>XTween이 50배 적은 Garbage 생성

>Gabage Alloc Time|Other Tween|XTween
>--------|-----------|------
>Start|185.5K|2.5KB
>End|1KB|0.02KB
>Total|186.5KB|2.52KB
> 
>XTween이 74배 이상의 적은 Garbage 생성

__CPU Performance__
>100개의 GameObject를 동시에 움직일시 움직일때의 CPU사용 비교
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_Update.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_Update.png)
>XTween이 약2배 빠름

__Code Compare__
 >__Other Tween Code__
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
- [ ] Support path editor by GUI
- [x] Support Easing Edit in UI Graph


Lastest Release
---
Version(Alpha) 0.0.57 - [XTween_0.0.57.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.57.unitypackage)


Position
---
```csharp
XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f).AddZ(100f)).Play();

//or
XTween.To(moveObj, XHash.New.Position(600f,200f)).Play();
```

Scale
---
```csharp
XTween.To(moveObj, XHash.New.AddScaleX(1f).AddScaleY(1.5f).AddScaleZ(0.5f)).Play();

//or
XTween.To(moveObj, XHash.New.Scale(1f,1.5f)).Play();
```

Rotation
---
```csharp
XTween.To(moveObj, XHash.New.AddRotationZ(600f)).Play();

//or
XTween.To(moveObj, XHash.New.Rotation(60f,-180f,-45f)).Play();
```

Combination
---
```csharp
XTween.To(moveObj, XHash.New.AddX(600f).AddScaleX(200f).AddRotationZ(180f)).Play();
```

Bezier
---
```csharp
XHash hash = XHash.New.AddX(0f).AddY(0f).AddControlPointX(1000f).AddControlPoint(-500f);
XTween.To(moveObj, hash).Play();
```

Value
---
```csharp
XTween.ValueTo(XObjectHash.New.Add("value", 10f, 200f), UpdateValue).Play();

void UpdateValue(XObjectHash hash)
{
  Debug.Log(hash.Now("value"));
}

//or Property tween
XTween.ValueTo<Camera>(camera3D, XObjectHash.New.Add("fieldOfView", 6f)).Play();
```

Event Handling
---
```csharp
IAni ani = XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f));
ani.OnComplete = Executor<float>.New(OnTweenEnd, 10f);
ani.Play();

void OnTweenEnd(float value)
{
  Debug.Log(value);
}

//or
XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f)).AddOnComplete(()=>Debug.Log("OnComplete")).Play();
```

Coroutine
---
Support use with coroutine
```csharp
StartCoroutine(CoroutineTween());
IEnumerator CoroutineTween()
{
	XHash hash = XHash.New.AddX(200f).AddY(50f).AddZ(-1500f);
	yield return XTween.To(this.target3D, hash).WaitForPlay();
	Debug.Log("On Complete Tween");
}

```

Serial
---
```csharp
XHash endPosition = XHash.New.AddX(1000f).AddY(300f);
XHash endScale = XHash.New.AddScaleX(200f).AddScaleY(200f);
XTween.SerialTweens
(	
	false, 
	XTween.To(moveObj, endPosition), 
	XTween.To(moveObj, endScale)
).Play();
```

Parallel
---
```csharp
XHash endPosition = XHash.New.AddX(1000f).AddY(300f);
XHash endScale = XHash.New.AddScaleX(200f).AddScaleY(200f);
XTween.ParallelTweens
(	
	false, 
	XTween.To(moveObj, endPosition), 
	XTween.To(moveObj, endScale)
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
XTween.ColorTo(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f)).Play();

//or when object has other type
XTween.ColorTo<Image>(imageInstance, "color", XColorHash.New.AddRed(0.56f).AddGreen(0.83f)).Play();
```

Custom Easing
---
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/EaseCurve.png)
>Customizable Easing, Support code hint for use of ease name, TopMenu -> Windows -> XTween Editor
```csharp
XHash hash = XHash.New.AddX(200f).AddY(50f).AddZ(-1500f)
	     .AddControlPointX(-1000f,550f).AddControlPointY(550f,-300f);
XTween.To(target3D, hash, 1f, Ease.Get(EaseName.MyEasing).Play();
```

Decorator
---
```csharp
IAni tween = XTween.ColorTo(sprite, XColorHash.New.AddRed(0.56f).AddGreen(0.83f));

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
