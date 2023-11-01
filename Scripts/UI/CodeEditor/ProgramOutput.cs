using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramOutput : MonoBehaviour
{
    public RectTransform scrollContent;
    public TMPro.TMP_Text text;
    public static ProgramOutput instance;
    [SerializeField] bool isOut = false;
    [SerializeField] float consoleOpenSpeed = 4000;
    private void Awake()
    {
        instance = this;
    }
    public void handleButton()
    {
        if (isOut)
            Close();
        else
            Open();
    }
    public void Open()
    {
        isOut = true;
        StopAllCoroutines();
        StartCoroutine(openEnumerator());
    }
    IEnumerator openEnumerator()
    {
        while ((transform as RectTransform).anchoredPosition.x > -350)
        {
            (transform as RectTransform).anchoredPosition -= new Vector2(consoleOpenSpeed*Time.deltaTime, 0);
            yield return null;
        }
        (transform as RectTransform).anchoredPosition = new Vector2(-350, -25);
    }
    public void Close()
    {
        isOut = false;
        StopAllCoroutines();
        StartCoroutine(closeEnumerator());
    }
    IEnumerator closeEnumerator()
    {
        while ((transform as RectTransform).anchoredPosition.x < 350)
        {
            (transform as RectTransform).anchoredPosition += new Vector2(consoleOpenSpeed * Time.deltaTime, 0);
            yield return null;
        }
        (transform as RectTransform).anchoredPosition = new Vector2(350, -25);
    }
    public void Write(string s)
    {
        text.text = s;
        scrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.preferredWidth);
        scrollContent.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, text.preferredHeight);
        return;
    }
}
