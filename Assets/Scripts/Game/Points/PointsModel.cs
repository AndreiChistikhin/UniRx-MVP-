using UniRx;

public class PointsModel
{
    public ReactiveProperty<int> PointsAmount { get; }

    public PointsModel()
    {
        PointsAmount = new ReactiveProperty<int>();
    }
    
    public void AddPoint()
    {
        PointsAmount.Value++;
    }
}
