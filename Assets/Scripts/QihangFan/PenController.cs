using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenController : MonoBehaviour
{
    GameObject strokeSize, strokeColor, StrokeType, functionSetting,signifierLeft, signifierRight;
    [Range(0, 12)]
    public int stage = 0;

    //stroke position
    float strokeSizeX = 0f, strokeColorX = 0f;
    float colorWheelY = 0f, eraserY = 0f;
    float blinkController = 0f, blinkController2 = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //game object setup
        strokeSize = this.gameObject.transform.GetChild(0).gameObject;
        strokeColor = this.gameObject.transform.GetChild(1).gameObject;
        StrokeType = this.gameObject.transform.GetChild(2).gameObject;
        functionSetting = this.gameObject.transform.GetChild(3).gameObject;
        signifierLeft = this.gameObject.transform.GetChild(4).GetChild(0).gameObject;
        signifierRight = this.gameObject.transform.GetChild(4).GetChild(1).gameObject;


        //setup envir monitor initial localscale
        strokeSize.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        strokeSize.SetActive(false);

        strokeColor.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        strokeColor.SetActive(false);

        StrokeType.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        StrokeType.SetActive(false);

        functionSetting.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        functionSetting.SetActive(false);

        signifierLeft.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        signifierLeft.SetActive(false);

        signifierRight.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        signifierRight.SetActive(false);

        //stroke type initial
        for (int i = 1; i < 4; i++)
        {
            StrokeType.transform.GetChild(i).gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            StrokeType.transform.GetChild(i).gameObject.transform.localPosition = new Vector3(0.0f, -0.2f, 0.0f);
        }

        //stroke size and color setup
        strokeSizeX = strokeSize.transform.GetChild(0).gameObject.transform.localPosition.x;
        strokeColorX = strokeColor.transform.GetChild(0).gameObject.transform.localPosition.x;

        strokeSize.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);
        strokeColor.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

        //function setting setup
        colorWheelY = functionSetting.transform.GetChild(0).gameObject.transform.localPosition.y;
        eraserY = functionSetting.transform.GetChild(1).gameObject.transform.localPosition.y;

        //arrow initial
        StrokeType.transform.GetChild(4).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);
        StrokeType.transform.GetChild(5).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

        functionSetting.transform.GetChild(2).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);
        functionSetting.transform.GetChild(3).gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

        //eraser position initiate
        functionSetting.transform.GetChild(1).gameObject.transform.localPosition = new Vector3(functionSetting.transform.GetChild(1).gameObject.transform.localPosition.x, functionSetting.transform.GetChild(1).gameObject.transform.localPosition.y - 0.2f, functionSetting.transform.GetChild(1).gameObject.transform.localPosition.z);
        functionSetting.transform.GetChild(1).gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (stage == 1)
        {
            signifierRight.SetActive(true);
            StartCoroutine(ObjScale(signifierLeft, 1.0f, 0f, 0.1f));
            signifierLeft.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

            signifierLeft.SetActive(true);
            StartCoroutine(ObjScale(signifierRight, 1.0f, 0f, 0.1f));
            signifierRight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);
        }
        else if (stage == 2)
        {
            //left signifier
            StartCoroutine(ObjScale(signifierLeft, 1.5f, 0f, 0.1f));
            signifierLeft.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.01f);

            //stroke size scale up
            strokeSize.SetActive(true);
            StartCoroutine(ObjScale(strokeSize, 1.0f, 0f, 0.1f));

            //stroke color scale up
            strokeColor.SetActive(true);
            StartCoroutine(ObjScale(strokeColor, 1.0f, 0f, 0.1f));

            //stroke size scale up
            StrokeType.SetActive(true);
            StartCoroutine(ObjScale(StrokeType, 1.0f, 0f, 0.1f));

            blinkController = 1;
        }
        else if (stage == 3)
        {
            //hide the current storke
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(0).gameObject, 0.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(0).gameObject, 0.2f, 0f, 0.5f));

            //show another one
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(1).gameObject, 1.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(1).gameObject, 0.0f, 0f, 0.5f));

            StartCoroutine(Blink(StrokeType.transform.GetChild(4).gameObject, 0.5f));

            blinkController2 = 1;
        }
        else if (stage == 4)
        {
            //hide the current storke
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(1).gameObject, 0.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(1).gameObject, 0.2f, 0f, 0.5f));

            //show another one
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(2).gameObject, 1.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(2).gameObject, 0.0f, 0f, 0.5f));

            StartCoroutine(Blink2(StrokeType.transform.GetChild(4).gameObject, 0.5f));

            blinkController = 1;
        }
        else if (stage ==  5)
        {
            //hide the current storke
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(2).gameObject, 0.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(2).gameObject, 0.2f, 0f, 0.5f));

            //show another one
            StartCoroutine(ObjScale(StrokeType.transform.GetChild(3).gameObject, 1.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(StrokeType.transform.GetChild(3).gameObject, 0.0f, 0f, 0.5f));

            StartCoroutine(Blink(StrokeType.transform.GetChild(4).gameObject, 0.5f));

            blinkController2 = 1;
        }
        else if (stage == 6)
        {
            StartCoroutine(Blink2(strokeSize.transform.GetChild(0).gameObject, 1.0f));
            StartCoroutine(ObjMoveX(strokeSize.transform.GetChild(0).gameObject, strokeSizeX - 0.25f, 0f, 0.1f));

            blinkController = 1;
        }
        else if (stage == 7)
        {
            StartCoroutine(Blink(strokeColor.transform.GetChild(0).gameObject, 1.0f));
            StartCoroutine(ObjMoveX(strokeColor.transform.GetChild(0).gameObject, strokeColorX - 0.18f, 0f, 0.1f));
        }
        else if (stage == 8)
        {
            //left signifier
            StartCoroutine(ObjScale(signifierLeft, 1f, 0f, 0.1f));
            signifierLeft.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

            //stroke size scale down
            //strokeSize.SetActive(true);
            StartCoroutine(ObjScale(strokeSize, 0f, 0f, 0.1f));

            //stroke color scale down
            //strokeColor.SetActive(true);
            StartCoroutine(ObjScale(strokeColor, 0f, 0f, 0.1f));

            //stroke size scale down
            //StrokeType.SetActive(true);
            StartCoroutine(ObjScale(StrokeType, 0f, 0f, 0.1f));
        }
        else if (stage == 9)
        {
            //right signifier
            StartCoroutine(ObjScale(signifierRight, 1.5f, 0f, 0.1f));
            signifierRight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.01f);

            //stroke size scale up
            //strokeSize.SetActive(true);
            StartCoroutine(ObjScale(strokeSize, 1.0f, 0f, 0.1f));

            //stroke size scale up
            functionSetting.SetActive(true);
            StartCoroutine(ObjScale(functionSetting, 1.0f, 0f, 0.1f));

            blinkController = 1;
        }
        else if (stage == 10)
        {
            //hide the current function
            StartCoroutine(ObjScale(functionSetting.transform.GetChild(0).gameObject, 0.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(functionSetting.transform.GetChild(0).gameObject, colorWheelY + 0.2f, 0f, 0.5f));

            //show another one
            StartCoroutine(ObjScale(functionSetting.transform.GetChild(1).gameObject, 1.0f, 0f, 0.3f));
            StartCoroutine(ObjMoveY(functionSetting.transform.GetChild(1).gameObject, eraserY, 0f, 0.5f));

            //stroke size scale down
            StartCoroutine(ObjScale(strokeSize, 0f, 0f, 0.1f));

            StartCoroutine(Blink(functionSetting.transform.GetChild(2).gameObject, 0.5f));
        }
        else if (stage == 11)
        {
            //function setting scale down
            StartCoroutine(ObjScale(functionSetting, 0f, 0f, 0.1f));

            //right signifier
            StartCoroutine(ObjScale(signifierRight, 1f, 0f, 0.1f));
            signifierRight.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);
        }
        else if (stage == 12)
        {
            StartCoroutine(ObjScale(signifierRight, 0f, 0f, 0.1f));
            StartCoroutine(ObjScale(signifierLeft, 0f, 0f, 0.1f));
        }
    }

    IEnumerator ObjScale(GameObject monitor, float s, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localScale, new Vector3(s, s, s)) > 0.001f)
        {

            monitor.transform.localScale = Vector3.Lerp(monitor.transform.localScale, new Vector3(s, s, s), smooth * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ObjMoveY(GameObject monitor, float y, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localPosition, new Vector3(monitor.transform.localPosition.x, y, monitor.transform.localPosition.z)) > 0.001f)
        {

            monitor.transform.localPosition = Vector3.Lerp(monitor.transform.localPosition, new Vector3(monitor.transform.localPosition.x, y, monitor.transform.localPosition.z), smooth * Time.deltaTime);

            yield return null;
        }

    }

    IEnumerator ObjMoveX(GameObject monitor, float x, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localPosition, new Vector3(x, monitor.transform.localPosition.y, monitor.transform.localPosition.z)) > 0.001f)
        {

            monitor.transform.localPosition = Vector3.Lerp(monitor.transform.localPosition, new Vector3(x, monitor.transform.localPosition.y, monitor.transform.localPosition.z), smooth * Time.deltaTime);

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

    IEnumerator Blink(GameObject obj, float t)
    {
        if (blinkController == 0)
        {
            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

            yield return null;
        }
        else if (blinkController == 1)
        {
            //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.01f);

            yield return new WaitForSeconds(t);

            blinkController = 0;

        }
    }

    IEnumerator Blink2(GameObject obj, float t)
    {
        if (blinkController2 == 0)
        {
            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.002f);

            yield return null;
        }
        else if (blinkController2 == 1)
        {
            //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Vector4(191f, 146f, 95f, 1f) * 0.01f);

            yield return new WaitForSeconds(t);

            blinkController2 = 0;

        }
    }


}
