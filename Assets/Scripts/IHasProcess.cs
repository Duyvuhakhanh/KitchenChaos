using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHasProcess 
{
    public event EventHandler<OnProcessChangedEventArg> OnProcessChanged;

    public class OnProcessChangedEventArg : EventArgs
    {
        public float processNomarlized;
    }
    private void CuttingCounter_OnCut(object sender, OnProcessChangedEventArg cuttingEventArg)
    {

    }

}
