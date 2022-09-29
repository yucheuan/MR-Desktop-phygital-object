using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CupTapTwice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textElem1;
    [SerializeField] private TextMeshProUGUI textElem2;
    [SerializeField] private TextMeshProUGUI textElem3;

    [SerializeField] private Animator panelAnime;

    int tapCount;

    private void OnTriggerEnter(Collider other)
    {
        tapCount++;
        Debug.Log(tapCount);

        if (other.CompareTag("Player") && tapCount % 2 == 0)
        {
            panelAnime.SetBool("playPanelDrinkAnime", true);

            textElem1.text = "You did it !";
            textElem2.text = "Great";
            textElem3.text = "starting 2-hour working";

            tapCount = 0;
            //Debug.Log("Tap" + tapCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {        
        if (other.CompareTag("Player"))
        {
            panelAnime.SetBool("playPanelDrinkAnime", false);

            Debug.Log("Drink false" + tapCount);
        }
    }

    void Update()
    {   
        //Optimization:when animation finish, then reset text
        if (panelAnime.GetBool("playPanelAppearAnime") == false)
        {
                textElem1.text = "Get hydrated !";
                textElem2.text = "3 Sips";
                textElem3.text = "100ml for 2-hour working";

                //Debug.Log("Reset" + tapCount);
        }        
    }
}
