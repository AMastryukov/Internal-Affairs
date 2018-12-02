using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefeatTexts", menuName = "DefeatTexts")]
public class DefeatTexts : ScriptableObject
{
    [TextArea]
    public string[] loyalty = new string[2];

    [TextArea]
    public string[] might = new string[2];

    [TextArea]
    public string[] influence = new string[2];
}
