using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]

public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectsSO cookedObjectSO;
    public KitchenObjectsSO burnedObjectSO;
    public float buringMaxTime;
}
