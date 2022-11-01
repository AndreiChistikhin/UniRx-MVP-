using UniRx;

public class HpPresenter
{
    CompositeDisposable _disposables;

    private HpView _hpView;
    private HpModel _hpModel;

    public HpPresenter(HpView view, HpModel model)
    {
        _hpView = view;
        _hpModel = model;
        _disposables = new CompositeDisposable();
    }

    public void Enable()
    {
        _hpModel.HpAmount.Subscribe(x => _hpView.DisableHeart(x)).AddTo(_disposables);
        _hpModel.IsDead.Where(x => x == true).Subscribe(_ => _hpView.EndGame()).AddTo(_disposables);
        MessageBroker.Default.Receive<MessageBase>().Where(t => t.MessageType == MessageType.MeteorHitFinish)
            .Subscribe(_ => _hpModel.HpAmount.Value--).AddTo(_disposables);
    }

    public void Disable()
    {
        _disposables.Clear();
    }
}