using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelectedGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeArg e)
    {
        if(e.clearCounter != baseCounter)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        foreach(GameObject go in visualSelectedGameObjectArray)
        {
            go.SetActive(true);

        }
    }
    private void Hide()
    {
        foreach (GameObject go in visualSelectedGameObjectArray)
        {
            go.SetActive(false);

        }
    }
}
