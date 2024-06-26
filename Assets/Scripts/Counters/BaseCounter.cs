using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform topCenterPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        
    }
    public virtual void InteractAlternate(Player player)
    {

    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public void ClearKitchenObject()
    {
        if (kitchenObject != null)
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
