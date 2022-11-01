using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HpView : MonoBehaviour
{
    [SerializeField] private List<Image> _hearts;
    [SerializeField] private EndGameMenu _endGameMenu;

    public void DisableHeart(int hpAmount)
    {
        if (hpAmount == _hearts.Count)
            return;
        Image heart = _hearts.FirstOrDefault();
        if (heart == null)
            throw new Exception("Нет ссылки на сердце");
        heart.gameObject.SetActive(false);
        _hearts.Remove(heart);
    }

    public void EndGame()
    {
        _endGameMenu.Init();
    }
}