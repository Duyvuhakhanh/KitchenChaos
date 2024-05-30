using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static IHasProcess;

public class CuttingCounter : BaseCounter, IHasProcess
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public event EventHandler OnCut;
    public event EventHandler<OnProcessChangedEventArg> OnProcessChanged;

    private int cuttingProcess;
    
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

                cuttingProcess = 0;
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectsSO());
                OnProcessChanged?.Invoke(this, new OnProcessChangedEventArg() { processNomarlized = (float)cuttingProcess / cuttingRecipeSO.cuttingProcessMax });
            }
        }
    }
    public override void InteractAlternate(Player player)
    {
        if(HasKitchenObject())
        {
            if (HasOutput(GetKitchenObject().GetKitchenObjectsSO()))
            {
                cuttingProcess++;
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectsSO());
                OnProcessChanged?.Invoke(this, new OnProcessChangedEventArg() { processNomarlized = (float)cuttingProcess / cuttingRecipeSO.cuttingProcessMax });
                if (cuttingProcess >= cuttingRecipeSO.cuttingProcessMax)
                {
                    var kitchenObject = GetKitchenObject();
                    KitchenObjectsSO ouputKitchenObjectSO = GetOutputFromInput(kitchenObject.GetKitchenObjectsSO());
                    kitchenObject.DestroySelf();
                    KitchenObject.SwapKitchenObject(ouputKitchenObjectSO, this);
                }
                OnCut?.Invoke(this, EventArgs.Empty);
            }

        }
    }
    public KitchenObjectsSO GetOutputFromInput(KitchenObjectsSO input)
    {
        var cuttingRecipe = GetCuttingRecipeSO(input);
        return cuttingRecipe? cuttingRecipe.output : null;
    }
    public bool HasOutput(KitchenObjectsSO input)
    {
        return GetOutputFromInput(input) != null;
    }
    public CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (var cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == kitchenObjectsSO) return cuttingRecipeSO;
        }
        return null;
    }
}
