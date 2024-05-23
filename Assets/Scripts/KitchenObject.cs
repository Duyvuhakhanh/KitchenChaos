using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    private IKitchenObjectParent kitchenObjectParent;

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);
        transform.parent = kitchenObjectParent.GetFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent ()
    {
        return kitchenObjectParent;
    }
    public static KitchenObject SwapKitchenObject(KitchenObjectsSO kitchenObjectsSO, IKitchenObjectParent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    public KitchenObjectsSO GetKitchenObjectsSO()
    {
        return kitchenObjectsSO;
    }
}
