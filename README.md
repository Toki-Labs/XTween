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
5. 강력한 퍼포먼스로 메모리와 CPU사용량이 적다.
6. 에디터 모드에서 사용은 물론, 모든 플랫폼에서 사용가능하다.

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
>XTween이 74.2배 적은 Garbage 생성

>__End__
>
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/U_Tween_End.png)
>![](https://github.com/Toki-Labs/XTween/raw/master/StoreDocument/XTween_End.png)
>XTween이 50배 적은 Garbage 생성, 생성시 비용 또한 3.4배 적음

>Gabage Alloc Time|Other Tween|XTween
>--------|-----------|------
>Start|185.5KG|2.5KB
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
- [ ] Support UI RectTransform
- [x] Performance check & polishing effciency
- [ ] Support path editor by GUI
- [ ] Support Easing Edit in UI Graph


Lastest Release
---
Version(Alpha) 0.0.25 - [XTween_0.0.25.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.25.unitypackage)


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
XTween.To(moveObj, XHash.New.AddX(600f).AddScale(200f).AddRotationZ(180f)).Play();
```

Bezier
---
```csharp
XPoint controlPoint = XPoint.New.AddX(1000).AddY(-500f);
XHash end = XHash.New.AddX(0f).AddY(0f);
XTween.BezierTo(this.moveObj, controlPoint, end).Play();
```

Value
---
```csharp
void Start()
{
  XTween.Tween(XObjectHash.New.Add("value", 10f, 200f), UpdateValue).Play();
}

void UpdateValue(XObjectHash hash)
{
  Debug.Log(hash.Now("value"));
}

//or
XObjectHash<Camera> hash = XObjectHash<Camera>.New.Add(camera3D, "fieldOfView", 6f);
XTween.To<Camera>(hash,data.time,data.Easing).Play();
```

Event Handling
---
```csharp
void Start()
{
  IAni ani = XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f));
  ani.onComplete = Executor<float>.New(OnTweenEnd, 10f);
  ani.Play();
}

void OnTweenEnd(float value)
{
  Debug.Log(value);
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

Author Info
---
Lee Dong-Myung(Tok-Labs) is a software developer in Korea.

Blog: http://blog.toki-labs.com (Korean)
Mail: dongmyung.5152@gmail.com

License
---
This library is under the MIT License.

Some code is borrowed from BetweenAS3.
