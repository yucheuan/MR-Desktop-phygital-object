using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHover : MonoBehaviour
{
    GameObject iconFrame;
    GameObject childIcon;
    int iconStatus = 0;

    Vector3 initialScale;

    [Range(1.0f, 2.0f)]
    public float maximalScale = 1.3f;
    public float expandSpeed = 0.05f;
    Vector3 speedVector;

    // Start is called before the first frame update
    void Start()
    {
        iconFrame = this.gameObject;
        childIcon = this.transform.GetChild(0).gameObject;

        //iconFrame.GetComponent<MeshRenderer>().enabled = false;

        initialScale = childIcon.transform.localScale;
        speedVector = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        childIcon.transform.localScale += speedVector;

        if (iconStatus == 1)
        {
            if(childIcon.transform.localScale.x < maximalScale)
            {
                speedVector.Set(expandSpeed, expandSpeed, expandSpeed);
            }
            else
            {
                speedVector.Set(0.0f, 0.0f, 0.0f);
            }
            
        }
        else if (iconStatus == 0)
        {
            if (childIcon.transform.localScale.x > initialScale.x)
            {
                speedVector.Set(-expandSpeed, -expandSpeed, -expandSpeed);
            }
            else
            {
                speedVector.Set(0.0f, 0.0f, 0.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandModel"))
        {
            //iconFrame.GetComponent<MeshRenderer>().enabled = true;
            iconStatus = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandModel"))
        {
            //iconFrame.GetComponent<MeshRenderer>().enabled = false;
            iconStatus = 0;
        }

    }

}
