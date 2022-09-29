using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupPanelController : MonoBehaviour
{
    GameObject cupPanelToday, cupPanelRecom, cupPanelRemind, cupPanelSelector, cupSignifier, envirMoni;
    [Range(0, 13)]
    public int stage = 0;
    float smoothing = 0.1f;
    float smoothingScale = 0.4f;

    //variables for specific panels
    float todayPanelPosY = 0f;
    float panelSelectorY = 0f;
    float activityTracker = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //game object setup
        cupPanelToday = this.gameObject.transform.GetChild(0).GetChild(0).gameObject;
        cupPanelRecom = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
        cupPanelRemind = this.gameObject.transform.GetChild(0).GetChild(2).gameObject;
        cupPanelSelector = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
        cupSignifier = this.gameObject.transform.GetChild(1).gameObject;
        envirMoni = this.gameObject.transform.GetChild(2).gameObject;

        //setup envir monitor initial localscale and local rotation
        for (int i = 0; i < envirMoni.transform.childCount; i++)
        {
            envirMoni.transform.GetChild(i).gameObject.transform.localScale = new Vector3 (0.0f, 0.0f, 0.0f);
            envirMoni.transform.GetChild(i).gameObject.transform.localEulerAngles = new Vector3 (0.0f, 0.0f, 340.0f);
            envirMoni.transform.GetChild(i).gameObject.SetActive(false);
        }

        cupPanelToday.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        todayPanelPosY = cupPanelToday.transform.localPosition.y;
        cupPanelToday.SetActive(false);

        cupPanelRecom.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        cupPanelRecom.SetActive(false);

        cupPanelRemind.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        cupPanelRemind.SetActive(false);

        cupPanelSelector.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        panelSelectorY = cupPanelSelector.transform.localPosition.y;
        cupPanelSelector.SetActive(false);

        cupSignifier.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        cupSignifier.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (stage == 1)
        {
            cupSignifier.SetActive(true);
            StartCoroutine(ObjScale(cupSignifier, 1.5f, 0f, 0.5f));

            cupSignifier.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4 (191f, 146f, 95f, 1f) * 0.002f);
        }
        else if (stage == 2)
        {
            //signifer sacles up
            StartCoroutine(ObjScale(cupSignifier, 2.0f, 0f, 0.2f));
            cupSignifier.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.01f);

            //time panel scale up
            cupPanelToday.SetActive(true);
            StartCoroutine(ObjScale(cupPanelToday, 1.0f, 1.0f, 0.1f));

            //envir-monitor shows up
            for (int i = 0; i < envirMoni.transform.childCount; i++)
            {
                envirMoni.transform.GetChild(i).gameObject.SetActive(true);
            }

            //can be optimized with for loop
            StartCoroutine(EnvirMoniShow(envirMoni, 0, 2.0f));
            StartCoroutine(EnvirMoniScaleup(envirMoni, 0, 2.0f));

            StartCoroutine(EnvirMoniShow(envirMoni, 1, 2.5f));
            StartCoroutine(EnvirMoniScaleup(envirMoni, 1, 2.5f));

            StartCoroutine(EnvirMoniShow(envirMoni, 2, 3.0f));
            StartCoroutine(EnvirMoniScaleup(envirMoni, 2, 3.0f));

            StartCoroutine(EnvirMoniShow(envirMoni, 3, 3.5f));
            StartCoroutine(EnvirMoniScaleup(envirMoni, 3, 3.5f));
        }
        else if (stage == 3)
        {
            //recommendation panel shows up
            cupPanelRecom.SetActive(true);
            StartCoroutine(ObjScale(cupPanelRecom, 1.0f, 0f, 0.1f));

            //today panel moves up
            StartCoroutine(ObjMoveY(cupPanelToday, todayPanelPosY + 0.5f, 0f, 0.1f));

        }
        else if (stage == 4)
        {
            cupPanelSelector.SetActive(true);
            StartCoroutine(ObjScale(cupPanelSelector, 1.0f, 0f, 0.5f));
            StartCoroutine(ObjScale(cupPanelToday.transform.GetChild(0).gameObject, 1.1f, 0f, 0.3f));
        }
        else if (stage == 5)
        {
            StartCoroutine(ObjMoveY(cupPanelSelector, panelSelectorY - 0.8f, 0f, 0.2f));
            StartCoroutine(ObjScale(cupPanelToday.transform.GetChild(0).gameObject, 1.0f, 0f, 0.3f));
            StartCoroutine(ObjScale(cupPanelRecom.transform.GetChild(0).gameObject, 1.1f, 0f, 0.3f));
        }
        else if (stage == 6)
        {
            //StartCoroutine(ObjScale(cupPanelSelector, 1.1f, 0f, 0.5f));
            StartCoroutine(ObjScale(cupPanelSelector, 0f, 0f, 0.5f));
            StartCoroutine(ObjScale(cupPanelRecom, 0f, 0f, 0.5f));
            StartCoroutine(ObjMoveY(cupPanelToday, todayPanelPosY, 0f, 0.1f));

            //hide
            StartCoroutine(ObjDeactive(cupPanelRecom, 0.3f));
            StartCoroutine(ObjDeactive(cupPanelSelector, 0.3f));
        }
        else if (stage == 7)
        {
            //signifer sacles down
            StartCoroutine(ObjScale(cupSignifier, 0.0f, 0f, 0.2f));
        }
        else if (stage == 8)
        {
            //hydrate panel shows up
            cupPanelRemind.SetActive(true);
            StartCoroutine(ObjScale(cupPanelRemind, 1.0f, 0f, 0.1f));

            //today panel moves up
            StartCoroutine(ObjMoveY(cupPanelToday, todayPanelPosY + 0.53f, 0f, 0.1f));
        }
        else if (stage == 9)
        {
            //signifer sacles up
            StartCoroutine(ObjScale(cupSignifier, 2.0f, 0f, 0.2f));
        }
        else if (stage == 10)
        {
            //envir-monitor sacles down and hides

            for (int i = 0; i < envirMoni.transform.childCount; i++)
            {
                StartCoroutine(ObjScale(envirMoni.transform.GetChild(i).gameObject, 0f, 0f, 0.5f));
                StartCoroutine(ObjDeactive(envirMoni.transform.GetChild(i).gameObject, 0.4f));

            }

            //time panel scale down
            StartCoroutine(ObjScale(cupPanelToday, 0f, 0f, 0.5f));
            StartCoroutine(ObjMoveY(cupPanelToday, todayPanelPosY, 0f, 0.1f));
            StartCoroutine(ObjDeactive(cupPanelToday, 0.4f));

            //reminder panel scale down
            StartCoroutine(ObjScale(cupPanelRemind, 0f, 0f, 0.5f));
            StartCoroutine(ObjDeactive(cupPanelRemind, 0.4f));

            //signifer sacles down
            StartCoroutine(ObjScale(cupSignifier, 1.5f, 0f, 0.2f));
            cupSignifier.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

            activityTracker = 1f;
        }
        else if (stage == 11)
        {
            //print(activityTracker);

            if (activityTracker == 1f)
            {
                StopAllCoroutines();
                activityTracker = 2f;

                for (int i = 0; i < envirMoni.transform.childCount; i++)
                {
                    envirMoni.transform.GetChild(i).gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                    envirMoni.transform.GetChild(i).gameObject.transform.localEulerAngles = new Vector3(0.0f, 0.0f, 340.0f);
                }

                cupPanelToday.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
                todayPanelPosY = cupPanelToday.transform.localPosition.y;

            }
            else if (activityTracker == 2f)
            {
                StartCoroutine(waitForSeconds());
            }
            else if (activityTracker == 0f)
            {
                //time panel scale up
                cupPanelToday.SetActive(true);
                StartCoroutine(ObjScale(cupPanelToday, 1.0f, 1.0f, 0.1f));

                //envir-monitor shows up
                for (int i = 0; i < envirMoni.transform.childCount; i++)
                {
                    envirMoni.transform.GetChild(i).gameObject.SetActive(true);
                }

                //can be optimized with for loop
                StartCoroutine(EnvirMoniShow(envirMoni, 0, 2.0f));
                StartCoroutine(EnvirMoniScaleup(envirMoni, 0, 2.0f));

                StartCoroutine(EnvirMoniShow(envirMoni, 1, 2.5f));
                StartCoroutine(EnvirMoniScaleup(envirMoni, 1, 2.5f));

                StartCoroutine(EnvirMoniShow(envirMoni, 2, 3.0f));
                StartCoroutine(EnvirMoniScaleup(envirMoni, 2, 3.0f));

                StartCoroutine(EnvirMoniShow(envirMoni, 3, 3.5f));
                StartCoroutine(EnvirMoniScaleup(envirMoni, 3, 3.5f));
            }

        }
        else if (stage == 12)
        {
            //signifer sacles down
            StartCoroutine(ObjScale(cupSignifier, 0.0f, 0f, 0.2f));
        }
        else if (stage == 13)
        {
            if (activityTracker == 0f)
            {
                StopAllCoroutines();
                activityTracker = 2f;

            }
            else if (activityTracker == 2f)
            {
                StartCoroutine(waitForSeconds2());
            }
            else if (activityTracker == 1f)
            {
                //envir-monitor sacles down and hides
                for (int i = 0; i < envirMoni.transform.childCount; i++)
                {
                    StartCoroutine(ObjScale(envirMoni.transform.GetChild(i).gameObject, 0f, 0f, 0.5f));
                    StartCoroutine(ObjDeactive(envirMoni.transform.GetChild(i).gameObject, 0.4f));
                }

                //time panel scale down
                StartCoroutine(ObjScale(cupPanelToday, 0f, 0f, 0.5f));
                StartCoroutine(ObjDeactive(cupPanelToday, 0.4f));

                //signifer sacles down
                StartCoroutine(ObjScale(cupSignifier, 0f, 0f, 0.2f));
                StartCoroutine(ObjDeactive(cupSignifier, 0.4f));
            }


        }

    }

    IEnumerator EnvirMoniShow(GameObject monitor, int i, float t)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z < 359.9f)
        {

            monitor.transform.GetChild(i).gameObject.transform.localEulerAngles = Vector3.Lerp(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles, new Vector3 (0.0f, 0.0f, 360.0f), smoothing * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator EnvirMoniScaleup(GameObject monitor, int i, float t)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (monitor.transform.GetChild(i).gameObject.transform.localScale.x < 1.0f)
        {

            monitor.transform.GetChild(i).gameObject.transform.localScale = Vector3.Lerp(monitor.transform.GetChild(i).gameObject.transform.localScale, new Vector3(1.01f, 1.01f, 1.01f), smoothingScale * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ObjScale(GameObject monitor, float s, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localScale, new Vector3(s, s, s)) > 0.05f)
        {

            monitor.transform.localScale = Vector3.Lerp(monitor.transform.localScale, new Vector3(s, s, s), smooth * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ObjMoveY(GameObject monitor, float y, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localPosition, new Vector3(monitor.transform.localPosition.x, y, monitor.transform.localPosition.z)) > 0.05f)
        {

            monitor.transform.localPosition = Vector3.Lerp(monitor.transform.localPosition, new Vector3(monitor.transform.localPosition.x, y, monitor.transform.localPosition.z), smooth * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ObjDeactive(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (obj.active)
        {

            obj.SetActive(false);

            yield return null;


        }

    }

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(0.2f);

        activityTracker = 0f;
    }

    IEnumerator waitForSeconds2()
    {
        yield return new WaitForSeconds(0.2f);

        activityTracker = 1f;
    }

}
