using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private DefeatTexts defeatTexts;
    [SerializeField] private TextMeshProUGUI defeatText;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip paperSlide;
    [SerializeField] private AudioClip loss;

    public void ShowDefeatScreen(int defeatAttribute, int defeatType)
    {
        // set the defeat text
        switch(defeatAttribute)
        {
            case 0:
                defeatText.text = defeatTexts.loyalty[defeatType];
                break;
            case 1:
                defeatText.text = defeatTexts.might[defeatType];
                break;
            case 2:
                defeatText.text = defeatTexts.influence[defeatType];
                break;
            default:
                break;
        }

        // move the defeat screen down
        transform.DOMove(Vector3.zero, 2f).SetEase(Ease.OutQuart);

        // stop the music and play the paper and loss sounds
        audioSource.Stop();
        audioSource.PlayOneShot(paperSlide);
        audioSource.PlayOneShot(loss);
    }
}
