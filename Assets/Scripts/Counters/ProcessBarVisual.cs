using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBarVisual : MonoBehaviour
{
    [SerializeField] private IHasProcess iHasProcess;
    [SerializeField] private GameObject iHasProcessGameObject;
    [SerializeField] private Image processImage;
    private void Start()
    {
        iHasProcess= iHasProcessGameObject.GetComponent<IHasProcess>();
        if(iHasProcess == null)
        {
            Debug.LogError("Game Object " + iHasProcessGameObject.name + "does not implement IHasProcess interface");
        }

        iHasProcess.OnProcessChanged += CuttingCounter_OnCut;

        processImage.fillAmount = 0;
        Hide();
    }

    private void CuttingCounter_OnCut(object sender, IHasProcess.OnProcessChangedEventArg cuttingEventArg)
    {
        processImage.fillAmount = cuttingEventArg.processNomarlized;

        if(cuttingEventArg.processNomarlized == 0 || cuttingEventArg.processNomarlized >= 1)
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
