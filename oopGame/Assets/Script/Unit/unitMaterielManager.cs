using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitMaterielManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Renderer> rendererList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setUp(GameObject unitSkinRef)
    {
        rendererList.Clear();
        foreach (Transform activeObject in unitSkinRef.transform)
        {
            if (activeObject.GetComponent<Renderer>() && activeObject.gameObject.activeSelf == true)
            {
                rendererList.Add(activeObject.gameObject.GetComponent<Renderer>());
            }
        }
    }

    public void ActivateRedShader()
    {
        foreach(Renderer rend in rendererList)
        {
            foreach( Material mat in rend.materials)
            {
                mat.SetFloat("_activatecoloor", 0);
            }
        }
    }

    public void DeactivateRedShader()
    {
        foreach (Renderer rend in rendererList)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetFloat("_activatecoloor", 1);
            }
        }
    }
}
