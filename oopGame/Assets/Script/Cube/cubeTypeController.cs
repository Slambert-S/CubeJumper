using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class cubeTypeController : MonoBehaviour
{
    private CubeType cubeType;
    public bool fixType = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void switchCubeType(CubeType newType)
    {
        if(fixType == false)
        {
            cubeType = newType;
            this.GetComponentInChildren<BasicPlatform>().ChangeCubeObject(this.transform.parent.gameObject.GetComponent<ChangeAllCube>().cubePrefabWithRating[(int)cubeType].spawnObject, cubeType);
        }
     
    }

    public enum CubeType
    {
        Normal,
        Block,
        MouvementBonus,
        Bomb,
        Push
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(cubeTypeController))]
public class ChangeCube : Editor
{
    // private SerializedProperty
    public cubeTypeController.CubeType option;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        option = (cubeTypeController.CubeType)EditorGUILayout.EnumPopup("Type to change to : ", option);

        cubeTypeController controller = (cubeTypeController)target;

        if (GUILayout.Button("Create Cube", GUILayout.Width(120f)))
        {
            //call function

            Debug.Log(option);
            controller.switchCubeType(option);
        }

        
    }
}

#endif
