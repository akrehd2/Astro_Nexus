using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public TMP_Text dialogText;
    public TMP_Text dialogKorText;

    public List<string> dialogStrings;
    public List<string> dialogKorStrings;

    private void Awake()
    {
        instance = this;
    }
}
