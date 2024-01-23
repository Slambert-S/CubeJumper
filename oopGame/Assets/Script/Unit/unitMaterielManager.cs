using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitMaterielManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private List<Renderer> rendererList;
    public Color yellowCollor = new Color(255f, 247f, 4f);
    public Color redColor = new Color(255f, 23f, 4f);
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

    public void ActivateYellowShader()
    {
        foreach (Renderer rend in rendererList)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_Color", yellowCollor);
                mat.SetFloat("_activatecoloor", 0);
            }
        }
    }

    public void DeactivateYellowShader()
    {
        foreach (Renderer rend in rendererList)
        {
            foreach (Material mat in rend.materials)
            {
                mat.SetFloat("_activatecoloor", 1);
                mat.SetColor("_Color", redColor);
            }
        }
    }

    //yellow 255 247 4
}
