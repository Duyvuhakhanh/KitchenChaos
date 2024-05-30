using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;
    public event EventHandler OnAddPlate;
    public event EventHandler OnRemovePlate;
    private float timer;
    private float timeToSpawnPlate = 2f;
    private int maxPlatesAmount = 4;
    private int platesSpawnedAmount;

    private void Update()
    {
        if (platesSpawnedAmount >= maxPlatesAmount) return;
        timer += Time.deltaTime;
        if(timer > timeToSpawnPlate)
        {
            timer = 0f;
            OnAddPlate?.Invoke(this, EventArgs.Empty);
            platesSpawnedAmount++;
        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (platesSpawnedAmount > 0)
            {
                platesSpawnedAmount--;
                KitchenObject.SwapKitchenObject(kitchenObjectsSO, player);
                OnRemovePlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
