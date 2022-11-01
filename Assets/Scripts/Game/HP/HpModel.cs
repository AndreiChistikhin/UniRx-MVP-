using UniRx;

public class HpModel
{
    public ReactiveProperty<int> HpAmount { get; }
    public ReadOnlyReactiveProperty<bool> IsDead { get; }
    
    public HpModel(int hpAmount)
    {
        HpAmount = new ReactiveProperty<int>(hpAmount);
        IsDead = HpAmount.Select(t => t <= 0).ToReadOnlyReactiveProperty();
    }
}