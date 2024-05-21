using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private Transform topCenterPoint;
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public void Interact()
    {
        Debug.Log("Interact !!" + gameObject.name);
        var kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, topCenterPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
    }
}
