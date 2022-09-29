using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    [SerializeField] public Animator animationController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TableDomain"))
        {
            //animationController.SetBool("menuApproachingInside", true);

            print("TRIGGERED");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("TableDomain"))
        {
            //animationController.SetBool("menuApproachingInside", false);
        }

    }
}
