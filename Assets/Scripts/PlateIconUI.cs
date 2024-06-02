using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArg e)
    {
        foreach(Transform child in transform)
        {
            if (child != iconTemplate) Destroy(child.gameObject);
        }

        foreach (var item in plateKitchenObject.GetKitchenObjectsSOList())
        {
            var icon = Instantiate(iconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.GetComponent<PlateIconSingleUI>().SetUIIcon(item);
        }
    }
}
