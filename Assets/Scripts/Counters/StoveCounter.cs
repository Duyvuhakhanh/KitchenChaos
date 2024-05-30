using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProcess
{
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] buringRecipeSOArray;

    public event EventHandler<StoveCounterEventArg> OnStoveCounterStateChanged;
    public event EventHandler<IHasProcess.OnProcessChangedEventArg> OnProcessChanged;
    public class StoveCounterEventArg : EventArgs
    {
        public State state;
    }

    private FryingRecipeSO fryingRecipe;
    private BurningRecipeSO burningRecipe;
    private float cookingTimer;
    private float burningTimer;
    private State state;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                cookingTimer += Time.deltaTime;
                if (cookingTimer >= fryingRecipe.cookingTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SwapKitchenObject(fryingRecipe.friedObjectSO, this);
                    burningTimer = 0f;
                    state = State.Fried;
                    OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArg { state = state });
                }
                OnProcessChanged?.Invoke(this, new() { processNomarlized = cookingTimer / fryingRecipe.cookingTime });
                break;
            case State.Fried:
                burningTimer += Time.deltaTime;
                if (burningTimer >= burningRecipe.buringMaxTime)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SwapKitchenObject(burningRecipe.burnedObjectSO, this);

                    state = State.Burned;

                    OnProcessChanged?.Invoke(this, new() { processNomarlized = 1f });
                    OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArg { state = state });
                }
                OnProcessChanged?.Invoke(this, new() { processNomarlized = burningTimer / burningRecipe.buringMaxTime });

                break;
            case State.Burned:
                break;
            default:
                break;
        }
    }
    public override void Interact(Player player)
    {
        if (HasKitchenObject() && state != State.Frying)
        {
            if (!player.HasKitchenObject())
            {
                state = State.Idle;
                OnProcessChanged?.Invoke(this, new() { processNomarlized = 0f });
                GetKitchenObject().SetKitchenObjectParent(player);
                ClearKitchenObject();
            }
        }
        else
        {
            if (player.HasKitchenObject() && HasOutputFrying(player.GetKitchenObject().GetKitchenObjectsSO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                player.ClearKitchenObject();
                fryingRecipe = GetFryingRecipeSO(GetKitchenObject().GetKitchenObjectsSO());
                burningRecipe = GetBurningRecipeSO(fryingRecipe.friedObjectSO);
                state = State.Frying;
                cookingTimer = 0f;
            }
        }
        OnStoveCounterStateChanged?.Invoke(this, new StoveCounterEventArg { state = state });

    }

    public KitchenObjectsSO GetOutputFryingFromInput(KitchenObjectsSO input)
    {
        var cuttingRecipe = GetFryingRecipeSO(input);
        return cuttingRecipe ? cuttingRecipe.friedObjectSO : null;
    }
    public bool HasOutputFrying(KitchenObjectsSO input)
    {
        return GetOutputFryingFromInput(input) != null;
    }
    public FryingRecipeSO GetFryingRecipeSO(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (var cookingRecipeSO in fryingRecipeSOArray)
        {
            if (cookingRecipeSO.uncookedObjectSO == kitchenObjectsSO) return cookingRecipeSO;
        }
        return null;
    }
    public KitchenObjectsSO GetOutputBurningFromInput(KitchenObjectsSO input)
    {
        var cuttingRecipe = GetBurningRecipeSO(input);
        return cuttingRecipe ? cuttingRecipe.burnedObjectSO : null;
    }
    public bool HasOutputBurning(KitchenObjectsSO input)
    {
        return GetOutputBurningFromInput(input) != null;
    }
    public BurningRecipeSO GetBurningRecipeSO(KitchenObjectsSO kitchenObjectsSO)
    {
        foreach (var cookingRecipeSO in buringRecipeSOArray)
        {
            if (cookingRecipeSO.cookedObjectSO == kitchenObjectsSO) return cookingRecipeSO;
        }
        return null;
    }
}
