using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform topCenterPoint;
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private KitchenObject kitchenObject;
    public void Interact(Player player)
    {
        Debug.Log("Interact !!" + gameObject.name);
        
        if(HasKitchenObject() == false)
        {
            var kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, topCenterPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
            ClearKitchenObject();
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject()
    {
        if(kitchenObject != null)
        {
            kitchenObject = null;
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }

    public Transform GetFollowTransform()
    {
        return topCenterPoint;
    }
}
