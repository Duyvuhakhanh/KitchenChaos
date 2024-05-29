using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHasProcess 
{
  
    public class CuttingEventArg : EventArgs
    {
        public float processNomarlized;
    }
    private void CuttingCounter_OnCut(object sender, CuttingEventArg cuttingEventArg)
    {

    }

}
