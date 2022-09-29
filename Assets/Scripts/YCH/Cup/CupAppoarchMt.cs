using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupAppoarchMt : MonoBehaviour
{
    [SerializeField] private Material emmisiveMt;

    //Change one object's mt
    //[SerializeField] private Renderer emmisiveMt;
    
    //instaniate material
    /*
    [SerializeField] private Renderer objectToChange;*/
    private float intensity = 7.0f;
    private Color color = Color.white;

    [SerializeField] private Animator ringAnime;

    private void Start()
    {
        emmisiveMt.SetColor("_EmissionColor", color * intensity);

        //instaniate material
        //emmisiveMt = objectToChange.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            color = Color.blue;
            //emmisiveMt.color = color;

            //Change one object's mt
            //emmisiveMt.material.color = color;

            //instaniate material
            emmisiveMt.SetColor("_EmissionColor", color * intensity);

            //Debug.Log("!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            color = Color.white;
            emmisiveMt.SetColor("_EmissionColor", color * intensity);
        }
    }
}
