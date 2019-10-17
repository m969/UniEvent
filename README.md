基于UniRx的超轻便的一个事件框架，提取自uFrame框架。

>uFrame是一个专门为大型游戏项目设计的基于MVVM模式的代码框架，非常强大，但如果用于小游戏项目就会显得比较臃肿，没有必要，但我又想要用uFrame那一套便利的事件机制，因此我就把它提取出来形成了这个UniEvent。

>使用方式：
在使用之前需要先引入UniRx命名空间。

>订阅事件：
```
this.OnEvent<TEvent>().Subscribe(x => { });
```
>发布事件：
```
this.Pubish(new TEvent());
```
