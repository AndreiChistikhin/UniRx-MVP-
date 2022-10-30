using UnityEngine;

public class BootStrapper : MonoBehaviour
{
    [SerializeField] private PointsView _pointsView;

    private PointsPresenter _pointsPresenter;

    private void Awake()
    {
        _pointsPresenter = new PointsPresenter(_pointsView, new PointsModel());
    }

    private void OnEnable()
    {
        _pointsPresenter.Enable();
    }
}