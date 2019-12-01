基于UniRx的一个轻便的事件框架，提取自uFrame框架。

>uFrame是一个专门为大型游戏项目设计的基于MVVM模式的代码框架，非常强大，但如果用于小游戏项目就会显得比较臃肿，没有必要，但我又想要用uFrame那一套便利的事件机制，因此我就把它提取出来形成了这个UniEvent。

### Installing with Unity Package Manager
*(Requires Unity version 2018.1.0  or above)*
To install this project as a [Git dependency](https://docs.unity3d.com/Manual/upm-git.html) using the Unity Package Manager,
add the following line to your project's `manifest.json`:
```
"com.github.m969.unievent": "https://github.com/m969/UniEvent.git"
```
You will need to have Git installed and available in your system's PATH.

>使用方式：
在使用之前需要先引入UniRx命名空间。

>订阅事件：
```c#
this.OnEvent<TEvent>().Subscribe(x => { });
```
>发布事件：
```c#
this.Pubish(new TEvent());
```

>示例:
```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public enum HeroEventType
{
    None,
    Spawn,
    Dead
}

public class HeroEvent
{
    public HeroEventType type;
    public string eventType;
    public string testStr;
}

public enum HeroCmdType
{
    None,
    Attack,
    BeAttacked
}

public class HeroCommand : ICommand<GameObject>
{
    public HeroCmdType type;
    public GameObject Result { get;set; }
}

public class UniEventTest : MonoBehaviour {
    //可订阅属性
    public ReactiveProperty<int> p = new ReactiveProperty<int>();


	void Start () {
        //订阅属性
        p.Subscribe(x => { print("p=" + x); });
        //订阅Hero事件
        this.OnEvent<HeroEvent>().Subscribe(OnHeroEvent);
        //筛选Hero出生事件
        this.OnEvent<HeroEvent>().Where(e => e.type == HeroEventType.Spawn).Subscribe(OnHeroSpawn);
        //筛选Hero死亡事件
        this.OnEvent<HeroEvent>().Where(e => e.type == HeroEventType.Dead).Subscribe(OnHeroDead);
        //订阅Hero命令
        this.OnEvent<HeroCommand>().Subscribe(OnHeroCommand);

        //改变属性
        p.Value = 1;
        //发布Hero事件
        this.Publish(new HeroEvent());
        //发布Hero出生事件
        this.Publish(new HeroEvent() { type = HeroEventType.Spawn });
        //发布Hero死亡事件
        this.Publish(new HeroEvent() { type = HeroEventType.Dead });
        //发布Hero命令并携带返回值
        var result = this.Execute<HeroCommand, GameObject>(new HeroCommand() { type = HeroCmdType.Attack });
        print(result);
    }

    private void OnHeroEvent(HeroEvent evt)
    {
        print("OnHeroEvent");
    }

    private void OnHeroSpawn(HeroEvent evt)
    {
        print("OnHeroSpawn");
    }

    private void OnHeroDead(HeroEvent evt)
    {
        print("OnHeroDead");
    }

    private void OnHeroCommand(HeroCommand evt)
    {
        print("OnHeroCommand " + evt.type);
        evt.Result = new GameObject(evt.type + "Result");
    }
}

```
