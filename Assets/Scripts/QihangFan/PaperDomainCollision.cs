using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperDomainCollision : MonoBehaviour
{
    GameObject paperDomain;
    public int paperStatusValue = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        paperDomain = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("TableDomain"))
        {
            paperStatusValue = 1;
        }
        else if (other.CompareTag("WallDomain"))
        {
            paperStatusValue = 2;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TableDomain") || (other.CompareTag("WallDomain")))
        {
            paperStatusValue = 0;
        }
    }
}
