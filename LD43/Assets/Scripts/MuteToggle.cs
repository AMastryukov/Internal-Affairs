using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    [SerializeField] private bool muted = false;
    [SerializeField] private GameObject cross;
    [SerializeField] private AudioSource audioSource;

    public void ToggleSounds()
    {
        muted = !muted;

        if (muted)
        {
            cross.SetActive(true);
            audioSource.Stop();
        }
        else
        {
            cross.SetActive(false);
            audioSource.Play();
        }
    }
}
