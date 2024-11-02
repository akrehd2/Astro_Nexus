using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public TMP_Text dialogText;

    public List<string> dialogStrings;

    private void Awake()
    {
        instance = this;
    }
}
