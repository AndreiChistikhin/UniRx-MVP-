using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class UniRxExamples : MonoBehaviour
{
    //https://gist.github.com/staltz/868e7e9bc2a7b8c1f754 гайд
    //https://medium.com/@gbrosgames/unirx-series-part-1-messagebroker-8d9c4b4581e4 message broker
    
    private void DoubleCLickDetection()
    {
        var clickStream = Observable.EveryUpdate()
            .Where(_ => Input.GetMouseButtonDown(0));

        clickStream.Buffer(clickStream.Throttle(TimeSpan.FromMilliseconds(250)))
            .Where(xs => xs.Count >= 2)
            .Subscribe(xs => Debug.Log("DoubleClick Detected! Count:" + xs.Count));
    }

    private void SimultaneousRequests()
    {
        var parallel = Observable.WhenAll(
            ObservableWWW.Get("http://google.com/"),
            ObservableWWW.Get("http://bing.com/"),
            ObservableWWW.Get("http://unity3d.com/"));

        parallel.Subscribe(xs =>
        {
            Debug.Log(xs[0].Substring(0, 100)); // google
            Debug.Log(xs[1].Substring(0, 100)); // bing
            Debug.Log(xs[2].Substring(0, 100)); // unity
        });
    }

    private void ShowProgress()
    {
        var progressNotifier = new ScheduledNotifier<float>();
        progressNotifier.Subscribe(x => Debug.Log(x));

        ObservableWWW.Get("http://google.com/", progress: progressNotifier).Subscribe();
    }

    //AddTo allows you to dispose of several subscriptions at once
    private void IntervalFrame()
    {
        Observable.IntervalFrame(30).Subscribe(x => Debug.Log(x)).AddTo(this);
        Observable.IntervalFrame(30).TakeUntilDisable(this)
            .Subscribe(x => Debug.Log(x), () => Debug.Log("completed!"));
    }

    private void WatchPositionChange()
    {
        transform.ObserveEveryValueChanged(x => x.position).Subscribe(x => Debug.Log(x));
    }

    private void DelayInvoke()
    {
        Observable.TimerFrame(100).Subscribe(_ => Debug.Log("after 100 frame"));
    }

    private void UnityUIEvents()
    {
        Button button = null;
        Toggle toggle = null;
        InputField input = null;
        Slider slider = null;
        Text text = null;

        button.onClick.AsObservable().Subscribe(_ => Debug.Log("clicked"));
        // Toggle, Input etc as Observable (OnValueChangedAsObservable is a helper providing isOn value on subscribe)
        // SubscribeToInteractable is an Extension Method, same as .interactable = x)
        toggle.OnValueChangedAsObservable().SubscribeToInteractable(button);

        // Input is displayed after a 1 second delay
        input.OnValueChangedAsObservable()
            .Where(x => x != null)
            .Delay(TimeSpan.FromSeconds(1))
            .SubscribeToText(text);

        // Converting for human readability
        slider.OnValueChangedAsObservable()
            .SubscribeToText(text, x => Math.Round(x, 2).ToString());
    }

    private void DestroyEnemy()
    {
        Enemy enemy = new Enemy(500);
        Button button = null;
        Text text = null;

        button.OnClickAsObservable().Subscribe(_ => enemy.CurrentHp.Value -= 99);
        enemy.CurrentHp.SubscribeToText(text);
        enemy.IsDead.Where(isDead => isDead)
            .Subscribe(_ => { button.interactable = false; });
    }

    private void Updates()
    {
        Observable.EveryUpdate().Subscribe(_ => Debug.Log("everyUpdate")).AddTo(this);
        Observable.EveryFixedUpdate().Subscribe(_=>DoSmth()).AddTo(this);
        Observable.EveryLateUpdate().Subscribe(_ => Debug.Log("everyLateUpdate")).AddTo(this);
    }
    
    private void DoSmth()
    {
        Debug.Log("fixedUpdate");
    }

    private void Triggers()
    {
        Collider collider=null;
        collider.OnTriggerEnterAsObservable().Subscribe(_ => { Debug.Log("trigger"); });
    }

    private void ReactiveProperties()
    {
        ReactiveProperty<float> hp = new ReactiveProperty<float>();
        hp.Where(x => x < 50).Subscribe(x=>Debug.Log("LessThan50HP")).AddTo(this);
        hp.Value = 49;
    }

    private void ReactiveCommands()
    {
        ReactiveCommand<float> floatCommand = new ReactiveCommand<float>();
        floatCommand.Subscribe(t => Debug.Log(t)).AddTo(this);
        floatCommand.Execute(10);
    }
}

public class ReactivePresenter : MonoBehaviour
{
    public Button MyButton;
    public Toggle MyToggle;
    public Text text;

    Enemy enemy = new Enemy(1000);

    void Start()
    {
        // Rx supplies user events from Views and Models in a reactive manner 
        MyButton.OnClickAsObservable().Subscribe(_ => enemy.CurrentHp.Value -= 99);
        MyToggle.OnValueChangedAsObservable().SubscribeToInteractable(MyButton);

        // Models notify Presenters via Rx, and Presenters update their views
        enemy.CurrentHp.SubscribeToText(text);
        enemy.IsDead.Where(isDead => isDead == true)
            .Subscribe(_ => { MyToggle.interactable = MyButton.interactable = false; });
    }
}

public class Enemy
{
    public ReactiveProperty<long> CurrentHp { get; private set; }

    public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }

    public Enemy(int initialHp)
    {
        CurrentHp = new ReactiveProperty<long>(initialHp);
        IsDead = CurrentHp.Select(x => x <= 0).ToReactiveProperty();
    }
}

public class Player
{
    public ReactiveProperty<int> Hp;
    public ReactiveCommand Resurrect;

    public Player()
    {
        Hp = new ReactiveProperty<int>(1000);

        Resurrect = Hp.Select(x => x <= 0).ToReactiveCommand();

        Resurrect.Subscribe(_ => { Hp.Value = 1000; });
    }
}

public class Presenter : MonoBehaviour
{
    public Button resurrectButton;

    Player player;

    void Start()
    {
        player = new Player();

        player.Resurrect.BindTo(resurrectButton);
    }
}

public class TestArgs:MonoBehaviour
{
    public int Value { get; set; }

    private void MessageBrokerExample()
    {
        MessageBroker.Default.Receive<TestArgs>().Subscribe(DoThis).AddTo(this);

        MessageBroker.Default.Publish(new TestArgs {Value = 1000});

        MessageBroker.Default.Receive<float>().Subscribe(x => Debug.Log(x)).AddTo(this);
        MessageBroker.Default.Publish(15);
    }

    private void DoThis(TestArgs test)
    {
        
    }
}