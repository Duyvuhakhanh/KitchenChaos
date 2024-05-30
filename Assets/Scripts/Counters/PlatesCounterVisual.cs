using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform platePrefabs;
    private List<Transform> platesList = new();

    private void Start()
    {
        platesCounter.OnAddPlate += OnAddPlate;
        platesCounter.OnRemovePlate += OnRemovePlate;
    }

    private void OnRemovePlate(object sender, EventArgs e)
    {
        var plate = platesList[^1];
        platesList.Remove(plate);
        Destroy(plate.gameObject);
    }

    private void OnAddPlate(object sender, EventArgs e)
    {
        float spacingY = 0.1f;

        var plateTrans = Instantiate(platePrefabs, platesCounter.GetFollowTransform());

        plateTrans.localPosition = Vector3.up * spacingY * platesList.Count;

        platesList.Add(plateTrans);
    }

}
