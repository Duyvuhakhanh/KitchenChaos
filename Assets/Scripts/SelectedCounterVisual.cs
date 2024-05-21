using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedGameObject;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChange += Player_OnSelectedCounterChange;
    }

    private void Player_OnSelectedCounterChange(object sender, Player.OnSelectedCounterChangeArg e)
    {
        if(e.clearCounter != clearCounter)
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
        visualSelectedGameObject.SetActive(true);
    }
    private void Hide()
    {
        visualSelectedGameObject.SetActive(false);
    }
}
