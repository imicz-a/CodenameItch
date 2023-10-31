using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingContentSizeFitter : MonoBehaviour
{
    RectTransform t;
    private void Start()
    {
        t = transform as RectTransform;
    }
    // Update is called once per frame
    void Update()
    {
        float size = 0;
        foreach(RectTransform child in t)
        {
            size += child.rect.height;
        }
        t.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
    }
}
