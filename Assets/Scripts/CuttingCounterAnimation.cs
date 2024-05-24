using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;
    private const string CUT = "Cut";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnInteractAlternate;
    }

    private void CuttingCounter_OnInteractAlternate(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
