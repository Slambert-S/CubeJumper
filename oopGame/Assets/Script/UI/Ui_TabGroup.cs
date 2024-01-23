using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_TabGroup : MonoBehaviour
{
    public List<Ui_TabButton> tabButtons;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public Ui_TabButton selectedTab;
    public List<GameObject> objectToSwap;
    public AudioClip clickAudio;
    public void Subscribe(Ui_TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<Ui_TabButton>();
        }

        tabButtons.Add(button);
    }
    public void OnTabEnter(Ui_TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }
    public void OnTabExit(Ui_TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(Ui_TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for(int i = 0; i < objectToSwap.Count; i++)
        {
            if(i == index)
            {
                // objectToSwap[i].SetActive(true);
                LeanTween.scaleY(objectToSwap[i],1,0.1f);
            }
            else
            {
                // objectToSwap[i].SetActive(false);
                LeanTween.scaleY(objectToSwap[i], 0, 0f);
            }
        }

        SoundFXManager.instance.PlaySoundFXClip(clickAudio, this.transform, 0.75f); 
    }

    public void ResetTabs()
    {
        foreach ( Ui_TabButton  button in tabButtons)
        {
            if (selectedTab != null && button == selectedTab) { continue; }
            
               
            
            button.background.color = tabIdle;
        }
    }
}
