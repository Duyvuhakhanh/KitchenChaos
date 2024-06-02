using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngridientAddedEventArg> OnIngridientAdded;
    public class OnIngridientAddedEventArg : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }
    [SerializeField] private List<KitchenObjectsSO> validObject;
    [SerializeField] private List<KitchenObjectsSO> kitchenObjectsSOList;
    public bool TryAddIngridient(KitchenObjectsSO obj)
    {
        if (IsVailidObject(obj))
        {
            if (!kitchenObjectsSOList.Contains(obj))
            {
                kitchenObjectsSOList.Add(obj);
                OnIngridientAdded?.Invoke(this, new() { kitchenObjectsSO = obj });
                return true;
            }
        }
        return false;
        
    }
    private bool IsVailidObject(KitchenObjectsSO obj)
    {
        return obj != null && validObject.Contains(obj);
    }
    public List<KitchenObjectsSO> GetKitchenObjectsSOList() => kitchenObjectsSOList;
}
