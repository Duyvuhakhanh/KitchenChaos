using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnInteractCounter;

    public override void Interact(Player player)
    {
        if(player.HasKitchenObject() == false)
        {
            KitchenObject.SwapKitchenObject(GetKitchenObjectsSO(), player);

            OnInteractCounter?.Invoke(this, EventArgs.Empty);
        }
    }
}
