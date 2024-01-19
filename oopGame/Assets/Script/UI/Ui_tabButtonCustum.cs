using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class Ui_tabButtonCustum : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{

    public Ui_TabGroup tabGroup;
    public Image background;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public void OnPointerClick(PointerEventData eventData)
    {
        ResetTabs();
        this.background.color = tabActive;
        Select();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ResetTabs();
        this.background.color = tabHover;
     
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ResetTabs();
    }

    public void ResetTabs()
    {
            this.background.color = tabIdle; 
    }

    // Start is called before the first frame update
    void Start()
    {
        background = GetComponent<Image>();
        
    }

  

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
            ResetTabs();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }

    }
}
