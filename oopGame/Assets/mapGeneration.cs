using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class mapGeneration : MonoBehaviour
{
    public GameObject placementBoxTest;

    public int lenght;
    public int height;
    public int nbCube;

    public float horizontalSpace;
    public float verticalSpace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doSomething()
    {
        Debug.Log("Hello");
        GameObject nexCube  =Instantiate(placementBoxTest,this.transform);
        nexCube.name = "CubeLocation";
    }
     public void RemoveAllCube()
     {
        while(transform.childCount > 0)
        {
            DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
     }

     public void randomizeCubeType()
    {
        this.gameObject.GetComponent<ChangeAllCube>().debugChangeaAllCube();
    }

     public void GenerateLayout()
    {
        RemoveAllCube();
        int currentCube = 0;

        int counter = 0;

        int verticalCounter = 0;
        bool firtCubeOfLine = true;
        while (currentCube  <  nbCube && currentCube < (height * lenght))
        {
            
            float flipFlop = (1 + currentCube % lenght) % 2;
            float side = 1;
            if (flipFlop == 1 )
            {
                side = -1;

            }

            if (firtCubeOfLine)
            {
                side = 1;
                firtCubeOfLine = false;
            }


            float horizontal = (this.transform.position.x + (counter) * horizontalSpace);

            float Vertical = (this.transform.position.z + (verticalCounter) * verticalSpace);
            GameObject nexCube = Instantiate(placementBoxTest,new Vector3(horizontal * side,this.transform.position.y , Vertical ),transform.rotation, this.transform);
            nexCube.name = "CubeLocation";
            currentCube++;

            
            if(side == -1 || counter == 0)
            {
                counter++;
            }

            if (currentCube % lenght == 0 && currentCube != 0)
            {
                verticalCounter++;
                counter = 0;
                firtCubeOfLine = true;
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(mapGeneration))]
public class generateMap : Editor
{
   // private SerializedProperty
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Future button");

        mapGeneration map = (mapGeneration)target;

        if (GUILayout.Button("Generate Cube position", GUILayout.Width(120f)))
        {
            //call function

            map.doSomething();
        }

        if (GUILayout.Button("Remove Cube", GUILayout.Width(120f)))
        {
            //call function

            map.RemoveAllCube();
        }

        if (GUILayout.Button("Generate grid Cube", GUILayout.Width(120f)))
        {
            //call function

            map.GenerateLayout();
        }

        if (GUILayout.Button("Randomize Cube", GUILayout.Width(120f)))
        {
            //call function

            map.randomizeCubeType();
        }
    }
}

#endif
