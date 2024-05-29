using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]

public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectsSO uncookedObjectSO;
    public KitchenObjectsSO friedObjectSO;
    public float cookingTime;
}
