using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanelToActive : MonoBehaviour
{

    public void SetPanelActive(int panelIndex)
    {
        foreach (RectTransform p in GameManager.instance.m_panelsArray)
        {
            p.gameObject.SetActive(false);
        }

        GameManager.instance.m_panelsArray[panelIndex].gameObject.SetActive(true);
    }
}
