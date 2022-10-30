using UniRx;

public class PointsPresenter
{
    private PointsView _view;
    private PointsModel _model;

    public PointsPresenter(PointsView view, PointsModel model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _model.PointsAmount.Subscribe(x=>_view.ShowPoints(x));
        MessageBroker.Default.Receive<Meteor>().Subscribe(_ => _model.AddPoint());
    }
}