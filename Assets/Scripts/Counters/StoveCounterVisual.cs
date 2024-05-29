using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] visualGameObject;
    [SerializeField] private StoveCounter stoveCounter;
    private void Start()
    {
        stoveCounter.OnStoveCounterStateChanged += StoveCounter_OnStoveCounterStateChanged; 
    }

    private void StoveCounter_OnStoveCounterStateChanged(object sender, StoveCounter.StoveCounterEventArg e)
    {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        ShowVisualFrying(showVisual);
    }

    public void ShowVisualFrying(bool isShow)
    {
        foreach (GameObject go in visualGameObject)
        {
            go.SetActive(isShow);
        }
    }


}
