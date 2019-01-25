using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


public class uniFrameTest : MonoBehaviour {
    public ReactiveProperty<int?> p = new ReactiveProperty<int?>();


	// Use this for initialization
	void Start () {
        this.OnEvent<uniFrameTest>().ObserveOnMainThread().Subscribe(OnUni);
        this.Pubish(this);
        p.Where(x => { return x.HasValue; }).Subscribe(x => { print("p=" + x); });
        p.Value = 1;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnUni(uniFrameTest evt)
    {
        print("OnUni");
    }
}
