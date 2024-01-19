using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is used to load the proper texture material on the humanoid unit 
 * 
 * 
 */
public class UnitySkinSetUp : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> rendererList;

    [SerializeField] private Texture OriginalTexture;
    [SerializeField] private Material MaterialToSet;
    [SerializeField] private Shader newShader;

    // Start is called before the first frame update
    void Start()
    {
        //shaderSetUp();
    }

    public void shaderSetUp()
    {
        GameObject unitSkinRef = this.transform.GetChild(0).gameObject;
        rendererList.Clear();
        foreach (Transform activeObject in unitSkinRef.transform)
        {
            if (activeObject.GetComponent<Renderer>() && activeObject.gameObject.activeSelf == true)
            {
                rendererList.Add(activeObject.gameObject.GetComponent<Renderer>());
            }
        }
        OriginalTexture = rendererList[0].materials[0].GetTexture("_MainTex");
        // Material []newMat = rendererList[0].materials;
        // newMat[0].shader = newShader;
        foreach(Renderer rend in rendererList)
        {
            foreach(Material mat in rend.materials)
            {
                mat.shader = newShader;
                mat.SetTexture("_Texture2D", OriginalTexture);
            }
        }
       // rendererList[0].materials[0].shader = newShader;

        // rendererList[0].materials = newMat;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
