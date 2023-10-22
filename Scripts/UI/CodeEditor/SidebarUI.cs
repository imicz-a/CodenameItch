using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SidebarUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    RectTransform rtransform;
    [SerializeField] float speedMultiplier = 300;
    [SerializeField] float fadespeed = 5f;
    [SerializeField] GameObject children;
    [SerializeField] CanvasGroup childrenCGroup;
    // Start is called before the first frame update
    void Start()
    {
        rtransform = GetComponent<RectTransform>();
        childrenCGroup.alpha = 0;
    }

    void Open()
    {
        StopAllCoroutines();
        StartCoroutine(OpenEnumerator());
    }
    IEnumerator OpenEnumerator()
    {
        while (rtransform.anchoredPosition.x < 250)
        {
            rtransform.anchoredPosition += new Vector2(speedMultiplier * Time.deltaTime, 0);
            childrenCGroup.alpha += fadespeed * Time.deltaTime;
            yield return null;
        }
        rtransform.anchoredPosition = new Vector2(250, -25);
        childrenCGroup.alpha = 1;
    }
    void Close()
    {
        StopAllCoroutines();
        StartCoroutine(CloseEnumerator());
    }
    IEnumerator CloseEnumerator()
    {
        while (rtransform.anchoredPosition.x > -150)
        {
            rtransform.anchoredPosition -= new Vector2(speedMultiplier * Time.deltaTime, 0);
            childrenCGroup.alpha -= fadespeed * Time.deltaTime;
            yield return null;
        }
        rtransform.anchoredPosition = new Vector2(-150, -25);
    }
    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("enter");
        Open();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit");
        Close();
    }
}
