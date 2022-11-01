using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private HpView _hpView;
    [SerializeField] private PointsView _pointsView;

    private HpPresenter _hpPresenter;
    private PointsPresenter _pointsPresenter;

    private void Awake()
    {
        _pointsPresenter = new PointsPresenter(_pointsView, new PointsModel());
        _hpPresenter = new HpPresenter(_hpView, new HpModel(3));
    }

    private void OnEnable()
    {
        _pointsPresenter.Enable();
        _hpPresenter.Enable();
    }

    private void OnDisable()
    {
        _pointsPresenter.Disable();
        _hpPresenter.Disable();
    }
}