using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private RectTransform _playerRectTransform;

    private const int PlayerSpeed = 2000;

    private void Start()
    {
        Observable.EveryUpdate().Where(_ => Input.GetAxis("Horizontal") != 0).Where(_ => CalculateIfCanMove())
            .Subscribe(Move).AddTo(this);
    }

    private void Move(long frame)
    {
        Vector3 localPosition = transform.localPosition;
        localPosition.x += Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        transform.localPosition = localPosition;
    }

    private bool CalculateIfCanMove()
    {
        float halfOfPlayerWidth = _playerRectTransform.rect.width / 2;
        float halfOfCanvasResolution = _canvasScaler.referenceResolution.x / 2;
        float input = Input.GetAxis("Horizontal");
        float currentPosition = transform.localPosition.x;
        if (currentPosition + halfOfPlayerWidth > halfOfCanvasResolution && input > 0)
            return false;
        if (currentPosition - halfOfPlayerWidth < -halfOfCanvasResolution && input < 0)
            return false;
        return true;
    }
}