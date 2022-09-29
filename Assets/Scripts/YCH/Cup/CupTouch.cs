using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupTouch : MonoBehaviour
{
    [SerializeField] private Animator ringAnime;
    [SerializeField] private Animator panelAnime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ringAnime.SetBool("playRingGoneAnime", true);
            //panelAnime.SetBool("playPanelAppearAnime", true);
        }

        if (other.CompareTag("Player"))
        {
            //ringAnime.SetBool("playRingGoneAnime", true);
            panelAnime.SetBool("playPanelAppearAnime", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ringAnime.SetBool("playRingGoneAnime", false);
            //panelAnime.SetBool("playPanelAppearAnime", false);
        }

        if (other.CompareTag("Player"))
        {
            //ringAnime.SetBool("playRingGoneAnime", false);
            panelAnime.SetBool("playPanelAppearAnime", false);
        }
    }
}
