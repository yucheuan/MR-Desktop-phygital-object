using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject f;



    [Range(0.01f, 0.2f)]
    public float abcScaleValue = 0;
    public bool abcActive = false;

    [Range(0.01f, 0.2f)]
    public float defScaleValue = 0;
    public bool defActive = false;


    // Start is called before the first frame update
    void Start()
    {
        a.SetActive(abcActive);
        b.SetActive(abcActive);
        c.SetActive(abcActive);
        d.SetActive(defActive);
        e.SetActive(defActive);
        f.SetActive(defActive);
    }

    // Update is called once per frame
    void Update()
    {
        a.SetActive(abcActive);
        b.SetActive(abcActive);
        c.SetActive(abcActive);
        d.SetActive(defActive);
        e.SetActive(defActive);
        f.SetActive(defActive);

        var abcScaleVector = new Vector3(abcScaleValue, abcScaleValue, abcScaleValue);
        var defScaleVector = new Vector3(defScaleValue, defScaleValue, defScaleValue);
        a.transform.localScale = abcScaleVector;
        b.transform.localScale = abcScaleVector;
        c.transform.localScale = abcScaleVector;
        d.transform.localScale = defScaleVector;
        e.transform.localScale = defScaleVector;
        f.transform.localScale = defScaleVector;
    }
}
