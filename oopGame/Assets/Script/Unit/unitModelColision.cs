using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitModelColision : MonoBehaviour
{
    public bool touchedWater = false;
    // Start is called before the first frame update
   // public GameObject modelRef;
    //public Material newMaterial;
    //public List<GameObject> listActiveObject;
    void Start()
    {
       /* foreach (Transform activeObject in transform)
        {
            if (activeObject.GetComponent<Renderer>() && activeObject.gameObject.activeSelf == true)
            {
                listActiveObject.Add(activeObject.gameObject);
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "water")
        {
            Debug.Log("touch water");
           // this.gameObject.transform.parent.GetChild(2).gameObject.SetActive(true);
            touchedWater = true;
           /* foreach (Material mat in modelRef.GetComponent<Renderer>().materials)
            {
                mat.SetFloat("_activatecoloor", 0);
            }*/
            
        }
    }

    public void touchedWaterUsed()
    {
        touchedWater = false;
    }
}
