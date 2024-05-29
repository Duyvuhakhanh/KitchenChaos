using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingProcessBar : MonoBehaviour, IHasProcess
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image processImage;
    private void Start()
    {
        cuttingCounter.OnProcessChanged += CuttingCounter_OnCut;

        processImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnCut(object sender, IHasProcess.CuttingEventArg cuttingEventArg)
    {
        processImage.fillAmount = cuttingEventArg.processNomarlized;

        if(cuttingEventArg.processNomarlized == 0 || cuttingEventArg.processNomarlized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive (false);
    }
}
