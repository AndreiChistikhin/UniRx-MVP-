using UnityEngine;

[CreateAssetMenu(fileName = "hpConfig",menuName = "Configs/HP")]
public class HpConfig : ScriptableObject
{
    [SerializeField] private int _hpAmount;

    public int HpAmount => _hpAmount;
}