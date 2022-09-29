using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTap : MonoBehaviour
{
    [SerializeField] private Animator padAnime;
    int initials = 0;  

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") && initials == 0)
        {
            
            Debug.Log("true");
            padAnime.SetBool("playPadInitials", true);
        }

        else
        {
            padAnime.SetBool("playPadComfirm", true);
        }        

        initials++;
        Debug.Log(initials);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            padAnime.SetBool("playPadComfirm", false);
            //panelAnime.SetBool("playPanelAppearAnime", false);
        }
    }
}
