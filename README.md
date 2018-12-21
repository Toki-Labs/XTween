XTween - Tweener for Unity
===
Created by Lee Dong-Myung(Toki-Labs)

XTween란?
---
유니티에서 코드로 애니매이션 트윈을 구현하기 위한 라이브러리로, 강력한 퍼포먼스와 쉬운 사용방법으로 원하는 결과물을 쉽게 구현가능하게 합니다.

XTween은 이런 기능이 좋습니다.
1. MonoBehaviour를 객체마다 생성시키지 않는다.
2. 몇줄의 코드로 Tween을 구현가능하다.
3. 이벤트 핸들링이 손쉽다.
4. 강력한 퍼포먼스로 메모리와 CPU사용량이 적다.
5. 에디터 모드에서 사용은 물론, 모든 플랫폼에서 사용가능합니다.

예제는 [XTween Example](http://toki-labs.com/xtween)에서 확인하실 수 있습니다.

Lastest Release
---
Version(Alpha) 0.0.5 - [XTween_0.0.5.unitypackage](https://github.com/Toki-Labs/XTween/raw/master/Bin/XTween_0.0.5.unitypackage)

Implementation
---
```csharp
XTween.To(moveObj, XHash.New.AddX(600f).AddY(200f)).Play();
```


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
