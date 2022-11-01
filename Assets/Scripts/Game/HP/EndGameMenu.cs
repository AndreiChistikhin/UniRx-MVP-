using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button _startNewGame;

    public void Init()
    {
        gameObject.SetActive(true);
        _startNewGame.OnClickAsObservable().Subscribe(_ =>
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }).AddTo(this);
        Time.timeScale = 0;
    }
}