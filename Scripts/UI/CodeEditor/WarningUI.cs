using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUI : MonoBehaviour
{
    public static WarningUI instance;
    private void Awake()
    {
        instance = this;
    }

    public TMPro.TMP_Text warningContents;
    public TMPro.TMP_Text warningTitle;

    public void DisplayWarning(string title, string contents)
    {
        gameObject.SetActive(true);
        warningContents.text = contents;
        warningTitle.text = title;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
