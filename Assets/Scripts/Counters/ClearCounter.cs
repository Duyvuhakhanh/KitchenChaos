using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                var playerObj = player.GetKitchenObject();
                var clearCounterKitchenObj = GetKitchenObject();
                if (playerObj.TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngridient(clearCounterKitchenObj.GetKitchenObjectsSO()))
                    {
                        clearCounterKitchenObj.DestroySelf();
                    }
                }
                else
                {
                    if(clearCounterKitchenObj.TryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngridient(playerObj.GetKitchenObjectsSO()))
                        {
                            playerObj.DestroySelf();
                        }
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                ClearKitchenObject();
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                player.ClearKitchenObject();
            }
        }
        

    }

 
}
