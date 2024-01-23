using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuDropdownSkinManager : MonoBehaviour
{

    //ToDo : Refactor script to use singleton of skinDB;
    public SkinDatabase skinDb;
    public ModelOf modelOf;
    public GameObject parentOfSkin;

    private TMPro.TMP_Dropdown dropdownRef;
    // Start is called before the first frame update
    private void OnEnable()
    {
        /*
        if(SkinDatabase.Instance != null)
        {
            skinDb = SkinDatabase.Instance;
        }
        else
        {
            skinDb = GameObject.Find("SkinDB").gameObject.GetComponent<SkinDatabase>();
        }
        dropdownRef = this.gameObject.GetComponent<TMP_Dropdown>();
        dropdownRef.value = -1;
        dropdownRef.onValueChanged.AddListener(delegate
        {
            ChangeSelectedSkin(dropdownRef);
        });*/
    }
    void Start()
    {
        Debug.Log("activation of the skin selector");
        if (SkinDatabase.Instance != null)
        {
            skinDb = SkinDatabase.Instance;
        }
        else
        {
            skinDb = GameObject.Find("SkinDB").gameObject.GetComponent<SkinDatabase>();
        }
        dropdownRef = this.gameObject.GetComponent<TMP_Dropdown>();
        dropdownRef.value = -1;
        dropdownRef.onValueChanged.AddListener(delegate
        {
            ChangeSelectedSkin(dropdownRef);
        });

        InsertOptionFromDb();
        StartCoroutine(delayedStart());
        

    }
    IEnumerator delayedStart()
    {
        yield return new WaitForSeconds(1);

        if (SkinDatabase.Instance != null && SkinDatabase.Instance.getPlayerSkin()  != -1  && SkinDatabase.Instance.getPartnerSkin() != -1)
        {
            
            switch (modelOf)
            {
                case ModelOf.player:
                    dropdownRef.value = SkinDatabase.Instance.getPlayerSkin();
                    break;
                case ModelOf.partner:
                    dropdownRef.value = SkinDatabase.Instance.getPartnerSkin() ;
                    break;
                default:
                    dropdownRef.value = 0;
                    break;
            }
        }
        else
        {
            switch (modelOf)
            {
                case ModelOf.player:
                    dropdownRef.value = 1;
                    break;
                case ModelOf.partner:
                    dropdownRef.value = 2;
                    break;
                default:
                    dropdownRef.value = 0;
                    break;
            }
        }
        


       // dropdownRef.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dropdownRef.value = 2;
        }
    }

    /// <summary>
    /// Get call to add all skin present in the skind DB into the dropdown menu.
    /// </summary>
    private void InsertOptionFromDb()
    {
        foreach (PlayerSkin skin in skinDb.skinDatabase)
        {
            TMP_Dropdown.OptionData m_newdata = new TMP_Dropdown.OptionData();
            m_newdata.text = skin.name;
            dropdownRef.options.Add(m_newdata);

        }
    }

    private void ChangeSelectedSkin(TMP_Dropdown reference)
    {
        if (skinDb.ConvertNameToIndex(reference.options[reference.value].text) < 0)
        {
            Debug.LogWarning(" Skin index is not valid");
            return;
        }

        int value = skinDb.ConvertNameToIndex(reference.options[reference.value].text);
        GameObject kidReference = parentOfSkin.transform.GetChild(0).gameObject;
        
        if(kidReference == null)
        {
            Debug.LogWarning("No children game object was found");
            return;
        }
        Instantiate(skinDb.skinDatabase[value].model, kidReference.transform.position, kidReference.transform.rotation, kidReference.transform.parent);
        
        Destroy(kidReference);

        switch (modelOf)
        {
            case ModelOf.player:
                skinDb.setPlayerSkin(value);
                break;
            case ModelOf.partner:
                skinDb.setPartnerSkin(value);
                break;
            default:
                
                break;
        }

    }

    public enum ModelOf
    {
        player,
        partner
    }
}
