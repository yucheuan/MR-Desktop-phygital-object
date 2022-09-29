using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperMenuController : MonoBehaviour
{
    [Range(0,10)]
    public int stage = 0;
    public bool confirmOption = false;
    [Range(1.0f, 1.5f)]
    public float maximalScale = 1.2f;

    Vector3 initialScale00, initialScale01, initialScale02, initialScale03, initialScale04, initialScale05;
    float initialPosX00, initialPosX01, initialPosX02, initialPosX03, initialPosX04, initialPosX05;
    Vector3 scaleSpeedVector;
    public float scaleSpeed = 0.04f;
    public float movementUnit = 0.5f;
    float movementSpeed = 0.012f;
    float scaleSmoothing = 0.1f;
    public float rotateDistance = 1f;
    public float rotateSpeed = 1f;

    GameObject container00, container01, container02, container03, container04, container05;

    //floating addon
    Vector3 initialPos00, initialPos01, initialPos02, initialPos03, initialPos04, initialPos05;
    public Vector3 moveDirection;
    public float moveDistance = 0.0015f;
    public float moveSpeed = 1f;

    private Vector3 startPosition02;

    // Start is called before the first frame update
    void Start()
    {
        //assign each icon to a gameObject (all can be optimized with for loop)
        container00 = this.gameObject.transform.GetChild(0).gameObject;
        container01 = this.gameObject.transform.GetChild(1).gameObject;
        container02 = this.gameObject.transform.GetChild(2).gameObject;
        container03 = this.gameObject.transform.GetChild(3).gameObject;
        container04 = this.gameObject.transform.GetChild(4).gameObject;
        container05 = this.gameObject.transform.GetChild(5).gameObject;

        //setup initial states
        initialScale00 = container00.transform.localScale;
        initialScale01 = container01.transform.localScale;
        initialScale02 = container02.transform.localScale;
        initialScale03 = container03.transform.localScale;
        initialScale04 = container04.transform.localScale;
        initialScale05 = container05.transform.localScale;

        initialPosX00 = container00.transform.localPosition.x;
        initialPosX01 = container01.transform.localPosition.x;
        initialPosX02 = container02.transform.localPosition.x;
        initialPosX03 = container03.transform.localPosition.x;
        initialPosX04 = container04.transform.localPosition.x;
        initialPosX05 = container05.transform.localPosition.x;

        moveDirection = new Vector3(0.0f, 1.0f, 0.0f);

        initialPos02 = container02.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (confirmOption == false)
        {
            //scale controlling (all can be optimized with for loop)
            containerUpdate(container00, initialScale00, 1);
            containerUpdate(container01, initialScale01, 2);
            containerUpdate(container02, initialScale02, 3);
            containerUpdate(container03, initialScale03, 4);
            containerUpdate(container04, initialScale04, 5);
            containerUpdate(container05, initialScale05, 6);

            //movement controlling
            containerPosUpdate(container00, initialPosX00, 1);
            containerPosUpdate(container01, initialPosX01, 2);
            containerPosUpdate(container02, initialPosX02, 3);
            containerPosUpdate(container03, initialPosX03, 4);
            containerPosUpdate(container04, initialPosX04, 5);
            containerPosUpdate(container05, initialPosX05, 6);
        }
        else if (confirmOption == true)
        {
            StartCoroutine(ObjScale(container00, 0f, 0f, scaleSmoothing));
            StartCoroutine(ObjScale(container01, 0f, 0f, scaleSmoothing));
            StartCoroutine(ObjScale(container02, 0f, 0f, scaleSmoothing));
            StartCoroutine(ObjScale(container04, 0f, 0f, scaleSmoothing));
            StartCoroutine(ObjScale(container05, 0f, 0f, scaleSmoothing));

            //container03.transform.localEulerAngles = new Vector3 (0, 0, rotateDistance * Mathf.Sin(Time.time * rotateSpeed));
            StartCoroutine(ObjScale(container03, 0f, 2f, scaleSmoothing));
        }

        //floating controlling - spent time debugging the conflict between floating effect and honrizontal movement. can't be in seperated scripts
        container00.transform.localPosition = container00.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 0.1f));
        container01.transform.localPosition = container01.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 1.4f));
        container02.transform.localPosition = container02.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 4.3f));
        container03.transform.localPosition = container03.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 2.3f));
        container04.transform.localPosition = container04.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 7.3f));
        container05.transform.localPosition = container05.transform.localPosition + moveDirection * (moveDistance * Mathf.Sin(Time.time * moveSpeed + 3.1f));


    }

    public void containerUpdate(GameObject container, Vector3 initialScale, int activeStage) {

        Vector3 localScaleSpeedVector;
        localScaleSpeedVector = new Vector3(0.0f, 0.0f, 0.0f);

        if (activeStage == stage)
        {
            if (container.transform.localScale.x < maximalScale)
            {
                localScaleSpeedVector.Set(scaleSpeed, scaleSpeed, scaleSpeed);
            }
            
        }
        else {
            if (container.transform.localScale.x > initialScale.x)
            {
                localScaleSpeedVector.Set(-scaleSpeed, -scaleSpeed, -scaleSpeed);
            }
        }

        container.transform.localScale += localScaleSpeedVector;

    }


    public void containerPosUpdate(GameObject container, float initialPos, int activeStage)
    {

        Vector3 localMovementSpeedVector;
        localMovementSpeedVector = new Vector3(0.0f, 0.0f, 0.0f);

        float currentPosX = container.transform.localPosition.x;

        float minPos = initialPos - movementUnit;
        float maxPos = initialPos + movementUnit;

        //Debug.Log(minPos);
        //Debug.Log(maxPos);
        //Debug.Log("CurrentPos" + container.transform.localPosition.x);

        if (activeStage == stage || stage == 0 || stage >= 7)
        {
            if (container.transform.localPosition.x < initialPos)
            {
                localMovementSpeedVector.Set(movementSpeed, 0.0f, 0.0f);
            }
            else if (container.transform.localPosition.x > initialPos)
            {
                localMovementSpeedVector.Set(-movementSpeed, 0.0f, 0.0f);
            }
        }
        else if (activeStage > stage)
        {
            if (container.transform.localPosition.x > minPos)
            {
                localMovementSpeedVector.Set(-movementSpeed, 0.0f, 0.0f);
            }
        }
        else if (activeStage < stage)
        {
            if (container.transform.localPosition.x < maxPos)
            {
                localMovementSpeedVector.Set(movementSpeed, 0.0f, 0.0f);
            }
        }

        container.transform.localPosition += localMovementSpeedVector;
    }


    IEnumerator ObjScale(GameObject monitor, float s, float t, float smooth)
    {
        yield return new WaitForSeconds(t);

        //print(monitor.transform.GetChild(i).gameObject.transform.localEulerAngles.z);

        while (Vector3.Distance(monitor.transform.localScale, new Vector3(s, s, s)) > 0.005f)
        {

            monitor.transform.localScale = Vector3.Lerp(monitor.transform.localScale, new Vector3(s, s, s), smooth * Time.deltaTime);

            yield return null;
        }

    }

}
