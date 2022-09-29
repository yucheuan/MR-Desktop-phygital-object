using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetingController : MonoBehaviour
{

    public GameObject a;
    public GameObject b;
    public GameObject c;

    public bool aValue = false;
    public bool bValue = false;
    public bool cValue = false;
    

    // Start is called before the first frame update
    void Start()
    {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        a.SetActive(aValue);
        b.SetActive(bValue);
        c.SetActive(cValue);
    }
}
