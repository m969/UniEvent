using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TestEvent
{
    public string testStr;
}

public class TestCommand
{
    public string testStr;
    public string Result;
}

public class UniEventTest : MonoBehaviour {
    public ReactiveProperty<int?> p = new ReactiveProperty<int?>();


	void Start () {
        p.Where(x => { return x.HasValue; }).Subscribe(x => { print("p=" + x); });
        this.OnEvent<TestEvent>().Subscribe(OnTestEvent);
        this.OnEvent<TestEvent>().Where((evt)=> { return evt.testStr == "test"; }).Subscribe(OnTestEvent2);
        this.OnEvent<TestCommand>().Subscribe(OnTestCommand);

        p.Value = 1;
        this.Publish(new TestEvent());
        this.Publish(new TestEvent() { testStr = "test" });
        var result = this.Execute(new TestCommand());
        print(result);
        result = this.Execute<TestCommand, string>(new TestCommand());
        print(result);
    }

    private void OnTestEvent(TestEvent evt)
    {
        print("OnTestEvent " + evt.testStr);
    }

    private void OnTestEvent2(TestEvent evt)
    {
        print("OnTestEvent " + evt.testStr);
    }

    private void OnTestCommand(TestCommand evt)
    {
        print("OnTestCommand " + evt.testStr);
        evt.Result = "OnTestCommand Result";
    }
}
