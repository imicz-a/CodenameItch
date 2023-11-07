using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SidebarUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    RectTransform rtransform;
    [SerializeField] float speedMultiplier = 100;
    [SerializeField] decimal fadespeed = 12;
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
        while (rtransform.anchoredPosition.x < 110)
        {
            rtransform.anchoredPosition += new Vector2(speedMultiplier * Time.deltaTime, 0);
            childrenCGroup.alpha += (float)fadespeed * Time.deltaTime;
            Debug.Log(childrenCGroup.alpha);
            yield return null;
        }
        rtransform.anchoredPosition = new Vector2(110, -25);
        childrenCGroup.alpha = 1;
    }
    void Close()
    {
        StopAllCoroutines();
        StartCoroutine(CloseEnumerator());
    }
    IEnumerator CloseEnumerator()
    {
        yield return new WaitForSeconds(.25f);
        while (rtransform.anchoredPosition.x > -290)
        {
            rtransform.anchoredPosition -= new Vector2(speedMultiplier * Time.deltaTime, 0);
            childrenCGroup.alpha -= (float)fadespeed * Time.deltaTime;
            yield return null;
        }
        rtransform.anchoredPosition = new Vector2(-290, -25);
        childrenCGroup.alpha = 0;
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
