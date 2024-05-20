using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    public void Interact()
    {
        if(Time.frameCount % 30 == 0)
        {
            Debug.Log("Interact !!" + gameObject.name);
        }
    }
}
