using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperHover : MonoBehaviour
{
    public GameObject parentGameObject;
    public GameObject tableDomain;
    public GameObject wallDomain;

    [Range(0, 1.0f)]
    public float initialScale = 0.7f;
    public float scaleTime = 0.8f;
    private float alphaValue = 0.0f;

    //public float scaleSpeedValue = 0.03f;
    //public float alphaChangeValue = 0.07f;

    private GameObject leftMenu;
    private GameObject rightMenu_Table;
    private GameObject rightMenu_Wall;

    private Vector3 initS_LeftMenu;
    private Vector3 initS_RightMenu_T;
    private Vector3 initS_RightMenu_W;
    private int handStatus = 0;

    //get access to the value in other script
    private GameObject paperDomain;
    private PaperDomainCollision paperStatus;
    //private int paperStatus = 1;

    private Renderer[] children_LeftMenu;
    private Renderer[] children_RightMenu_T;
    private Renderer[] children_RightMenu_W;

    GameObject paperCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        //get gameObjects
        paperCollider = this.gameObject;
        leftMenu = parentGameObject.transform.Find("LeftMenu").gameObject;
        rightMenu_Table = parentGameObject.transform.Find("RightMenu_Table").gameObject;
        rightMenu_Wall = parentGameObject.transform.Find("RightMenu_Wall").gameObject;

        //save initial local scale value
        initS_LeftMenu = leftMenu.transform.localScale;
        initS_RightMenu_T = rightMenu_Table.transform.localScale;
        initS_RightMenu_W = rightMenu_Wall.transform.localScale;

        //determine local scale starting value
        leftMenu.transform.localScale.Set(leftMenu.transform.localScale.x * initialScale, leftMenu.transform.localScale.y * initialScale, leftMenu.transform.localScale.z * initialScale);
        rightMenu_Table.transform.localScale.Set(rightMenu_Table.transform.localScale.x * initialScale, rightMenu_Table.transform.localScale.y * initialScale, rightMenu_Table.transform.localScale.z * initialScale);
        rightMenu_Wall.transform.localScale.Set(rightMenu_Wall.transform.localScale.x * initialScale, rightMenu_Wall.transform.localScale.y * initialScale, rightMenu_Wall.transform.localScale.z * initialScale);

        //get lists of Renderer[]
        //children_LeftMenu = leftMenu.GetComponentsInChildren<Renderer>();
        //children_RightMenu_T = rightMenu_Table.GetComponentsInChildren<Renderer>();
        //children_RightMenu_W = rightMenu_Wall.GetComponentsInChildren<Renderer>();

        //set initial alpha to 0
        SetAlpha(leftMenu, alphaValue);
        SetAlpha(rightMenu_Table, alphaValue);
        SetAlpha(rightMenu_Wall, alphaValue);

        //disable all three menus
        leftMenu.SetActive(false);
        rightMenu_Table.SetActive(false);
        rightMenu_Wall.SetActive(false);

        paperDomain = parentGameObject.transform.Find("PaperDomain").gameObject;
        paperStatus = paperDomain.GetComponent<PaperDomainCollision>();

    }

    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime : B-A/T * Time.deltaTime = X/Frame - (distance per frame), deltaTime*Frame = 1, X is distance per one second

        if (paperStatus.paperStatusValue == 1)
        {
            if (rightMenu_Wall.activeSelf)
            {
                ObjectScaleDown(rightMenu_Wall, initS_RightMenu_W);
            }

            if (handStatus == 1)
            {
                ObjectScaleUp(leftMenu, initS_LeftMenu);
                ObjectScaleUp(rightMenu_Table, initS_RightMenu_T);
            }
            else if (handStatus == 0)
            {
                ObjectScaleDown(leftMenu, initS_LeftMenu);
                ObjectScaleDown(rightMenu_Table, initS_RightMenu_T);
            }
        }
        else if (paperStatus.paperStatusValue == 2)
        {
            if (rightMenu_Table.activeSelf)
            {
                ObjectScaleDown(rightMenu_Table, initS_RightMenu_T);
            }

                if (handStatus == 1)
            {
                ObjectScaleUp(leftMenu, initS_LeftMenu);
                ObjectScaleUp(rightMenu_Wall, initS_RightMenu_W);
            }
            else if (handStatus == 0)
            {
                ObjectScaleDown(leftMenu, initS_LeftMenu);
                ObjectScaleDown(rightMenu_Wall, initS_RightMenu_W);
            }
        }
        else
        {
            if (leftMenu.activeSelf)
            {
                ObjectScaleDown(leftMenu, initS_LeftMenu);
            }

            if (rightMenu_Table.activeSelf)
            {
                ObjectScaleDown(rightMenu_Table, initS_RightMenu_T);
            }

            if (rightMenu_Wall.activeSelf)
            {
                ObjectScaleDown(rightMenu_Wall, initS_RightMenu_W);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("HandModel"))
        {
            handStatus = 1;
        }
        //else if (other.CompareTag("TableDomain"))
        //{
            //iconFrame.GetComponent<MeshRenderer>().enabled = true;
            //paperStatus = 1;

            //print("In the table domain.");
        //}
        //else if (other.CompareTag("WallDomain"))
        //{
            //iconFrame.GetComponent<MeshRenderer>().enabled = true;
            //paperStatus = 2;

            //print("In the wall domain.");
        //}
        //else if (other.CompareTag("HandModel"))
        //{
            //handStatus = 1;
        //}

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandModel"))
        {
            handStatus = 0;
        }

    }



    public void SetAlpha(GameObject main, float alpha)
    {
        Renderer[] children;
        children = main.GetComponentsInChildren<Renderer>();

        foreach (Renderer child in children)
        {
            Color col = child.material.GetColor("_BaseColor");
            col.a = alpha;
            child.material.SetColor("_BaseColor", col);
        }
    }



    public void ObjectScaleUp (GameObject targetObj, Vector3 initVector)
    {

        targetObj.SetActive(true);
        
        //object scale up
        if (targetObj.transform.localScale.x < initVector.x)
        {
            var growSpeed = new Vector3(initVector.x * (1 - initialScale) / scaleTime * Time.deltaTime, initVector.y * (1 - initialScale) / scaleTime * Time.deltaTime, initVector.z * (1 - initialScale) / scaleTime * Time.deltaTime);
            //var growSpeed = new Vector3(scaleSpeedValue, scaleSpeedValue, scaleSpeedValue);
            targetObj.transform.localScale += growSpeed;

        }
        else
        {
            targetObj.transform.localScale = initVector;
        }

        //object shows
        float alphaSpeed = Time.deltaTime / scaleTime;

        if (alphaValue < 0)
        {
            alphaValue = 0;
        }
        else if (alphaValue < 1)
        {
            alphaValue += alphaSpeed;
            //alphaValue += alphaChangeValue;
        }
        else
        {
            alphaValue = 1;
        }

        SetAlpha(targetObj, alphaValue);


    }

    public void ObjectStay(GameObject targetObj, Vector3 initVector)
    {
        targetObj.transform.localScale = initVector;
        SetAlpha(targetObj, 1);
    }

    public void ObjectScaleDown(GameObject targetObj, Vector3 initVector)
    {

        //object scale down
        if (targetObj.transform.localScale.x > initVector.x * initialScale)
        {
            var growSpeed = new Vector3(initVector.x * (1 - initialScale) / scaleTime * Time.deltaTime, initVector.y * (1 - initialScale) / scaleTime * Time.deltaTime, initVector.z * (1 - initialScale) / scaleTime * Time.deltaTime);
            //var growSpeed = new Vector3(scaleSpeedValue, scaleSpeedValue, scaleSpeedValue);
            targetObj.transform.localScale -= growSpeed;

        }
        else if (targetObj.transform.localScale.x <= initVector.x * initialScale)
        {
            targetObj.transform.localScale = initVector * initialScale;
        }

        //object hide
        float alphaSpeed = Time.deltaTime / scaleTime;

        if (alphaValue <= 0)
        {
            alphaValue = 0;
        }
        else
        {
            alphaValue -= alphaSpeed;
            //alphaValue -= alphaChangeValue;
        }

        SetAlpha(targetObj, alphaValue);

        if (alphaValue == 0)
        {
            targetObj.SetActive(false);
        }
    }
}
