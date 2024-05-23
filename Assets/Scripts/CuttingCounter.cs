using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public event EventHandler OnCut;
    private int cuttingProcess;
    public class CuttingEventArg : EventArgs
    {
        public float processNomarlized;
    }
    public override void Interact(Player player)
    {

        if (HasKitchenObject())
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                ClearKitchenObject();
            }
        }
        else
        {
            if (player.HasKitchenObject() && HasOutput(player.GetKitchenObject().GetKitchenObjectsSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                
                player.ClearKitchenObject();
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            cuttingProcess ++ ;
            if(cuttingProcess >= 3)
            {
                var kitchenObject = GetKitchenObject();
                KitchenObjectsSO ouputKitchenObjectSO = GetOutputFromInput(kitchenObject.GetKitchenObjectsSO());
                kitchenObject.DestroySelf();
                KitchenObject.SwapKitchenObject(ouputKitchenObjectSO, this);
            }
        }
    }
    public KitchenObjectsSO GetOutputFromInput(KitchenObjectsSO input)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == input) return cuttingRecipeSO.output;
        }
        return null;
    }
    public bool HasOutput(KitchenObjectsSO input)
    {
        return GetOutputFromInput(input) != null;
    }
}
