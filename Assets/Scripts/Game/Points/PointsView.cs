using TMPro;
using UnityEngine;

public class PointsView : MonoBehaviour
{
    [SerializeField] private TMP_Text _points;

    public void ShowPoints(int pointsAmount)
    {
        _points.text = pointsAmount.ToString();
    }
}