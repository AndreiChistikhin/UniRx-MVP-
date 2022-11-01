using UniRx;

public class PointsPresenter
{
    private CompositeDisposable disposables = new CompositeDisposable();

    private PointsView _view;
    private PointsModel _model;

    public PointsPresenter(PointsView view, PointsModel model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _model.PointsAmount.Subscribe(x => _view.ShowPoints(x));
        MessageBroker.Default.Receive<MessageBase>().Where(t => t.MessageType == MessageType.BulletHitMeteor)
            .Subscribe(_ => _model.PointsAmount.Value++);
    }

    public void Disable()
    {
        disposables.Clear();
    }
}