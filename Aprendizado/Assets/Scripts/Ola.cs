using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ola : MonoBehaviour, IInteractable
{

    public void Interact() 
    {
        ola();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ola()
    {
        Debug.Log("Ola");
        Destroy(gameObject);
    }
}
