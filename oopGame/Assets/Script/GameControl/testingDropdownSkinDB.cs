using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class testingDropdownSkinDB : MonoBehaviour
{
    // Start is called before the first frame update
    public SkinDatabase skinDb;
    public TMPro.TMP_Dropdown dropdownRef;
    public GameObject parentOfSkin;
    void Start()
    {
        dropdownRef = this.gameObject.GetComponent<TMP_Dropdown>();
        SetNewOption();

        dropdownRef.onValueChanged.AddListener(delegate
        {
            testingSelection(dropdownRef);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNewOption()
    {
        foreach(PlayerSkin skin in skinDb.skinDatabase)
        {
            TMP_Dropdown.OptionData m_newdata = new TMP_Dropdown.OptionData();
            m_newdata.text = skin.name;
            dropdownRef.options.Add(m_newdata);

        }

        
    }

    public void testingSelection(TMP_Dropdown reference)
    {
        Debug.Log(reference.options[reference.value].text);
        Debug.Log(skinDb.ConvertNameToIndex(reference.options[reference.value].text));
        int value = skinDb.ConvertNameToIndex(reference.options[reference.value].text);
        GameObject kidReference = parentOfSkin.transform.GetChild(0).gameObject;
        Instantiate(skinDb.skinDatabase[value].model, kidReference.transform.position, kidReference.transform.rotation, kidReference.transform.parent);
        Destroy(kidReference);
    }
}
