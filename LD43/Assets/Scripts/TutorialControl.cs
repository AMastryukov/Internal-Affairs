using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControl : MonoBehaviour
{
    [SerializeField] private static bool enabledByDefault = true;

    [SerializeField] private Canvas tutorialCanvas;

    [SerializeField] private List<GameObject> tutorialMasks;
    [SerializeField] private List<GameObject> tutorialTexts;

    [SerializeField] private Button previousStepButton;
    [SerializeField] private Button nextStepButton;

    [SerializeField] private int currentStep = -1;

    private void Start()
    {
        DisableAllMasksAndTexts();

        if (enabledByDefault)
        {
            StartTutorial();
        }
    }

    public void StartTutorial()
    {
        tutorialCanvas.enabled = true;
        EnableTutorialStep(0);
    }

    public void EndTutorial()
    {
        DisableAllMasksAndTexts();
        tutorialCanvas.enabled = false;
        currentStep = -1;

        enabledByDefault = false;
    }

    public void EnableNextStep()
    {
        // end tutorial if at last step
        if (currentStep == tutorialMasks.Count - 1)
        {
            EndTutorial();
            return;
        }

        currentStep++;

        DisableAllMasksAndTexts();
        EnableTutorialStep(currentStep);
    }

    public void EnablePreviousStep()
    {
        // ensure we don't go into negative steps
        if (currentStep <= 0)
        {
            return;
        }

        currentStep--;

        DisableAllMasksAndTexts();
        EnableTutorialStep(currentStep);
    }

    private void DisableAllMasksAndTexts()
    {
        // disable all masks
        for (int i = 0; i < tutorialMasks.Count; i++)
        {
            tutorialMasks[i].SetActive(false);
        }

        // disable all texts
        for (int i = 0; i < tutorialTexts.Count; i++)
        {
            tutorialTexts[i].SetActive(false);
        }
    }

    private void EnableTutorialStep(int step)
    {
        currentStep = step;

        // enable buttons
        nextStepButton.gameObject.SetActive(true);

        if (step > 0)
        {
            previousStepButton.gameObject.SetActive(true);
        }
        else
        {
            previousStepButton.gameObject.SetActive(false);
        }

        tutorialMasks[step].SetActive(true);
        tutorialTexts[step].SetActive(true);
    }
}
