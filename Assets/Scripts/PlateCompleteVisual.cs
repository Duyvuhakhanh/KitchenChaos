using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public GameObject gameObject;
        public KitchenObjectsSO KitchenObjectsSO;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList;
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArg e)
    {
        foreach (var item in kitchenObjectSO_GameObjectList)
        {
            if(item.KitchenObjectsSO == e.kitchenObjectsSO)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
