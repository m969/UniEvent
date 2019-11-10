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
