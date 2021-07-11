using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private List<GameObject> objectsToShow;

    private void OnEnable()
    {
        gameCycle.GameIsLost += OnGameIsLost;
    }

    private void OnDestroy()
    {
        gameCycle.GameIsLost -= OnGameIsLost;
    }

    private void OnGameIsLost()
    {
        for (int i = 0; i < objectsToShow.Count; i++)
        {
            objectsToShow[i].SetActive(true);
        }
    }
}
