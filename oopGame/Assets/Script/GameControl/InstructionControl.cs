using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playPage;
    [SerializeField] private GameObject instructionPage;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OpenInstruction()
    {
        instructionPage.SetActive(true);
        playPage.SetActive(false);
    }

    public void CloseInstruction()
    {
        instructionPage.SetActive(false);
        playPage.SetActive(true);
    }
}
