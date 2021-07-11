using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button playButton; 
    [SerializeField] private GameCycle gameCycle;
    [SerializeField] private TargetShower targetShower;
    [SerializeField] private List<GameObject> objectsToHide = new List<GameObject>();

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
    }

    private void OnPlayButtonClick()
    {
        for (int i = 0; i < objectsToHide.Count; i++)
        {
            objectsToHide[i].SetActive(false);
        }

        targetShower.ShowTarget(() => gameCycle.StartGame());
    }
}
