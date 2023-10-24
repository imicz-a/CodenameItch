using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeDragManager : MonoBehaviour
{
    public static NodeDragManager instance;
    public static Transform nodeParent {
        get { return instance._nodeParent; }
        set { instance._nodeParent = value; } }
    public Transform _nodeParent;
    public static Transform nodeSpawnerParent
    {
        get { return instance._nodeSpawnerParent; }
        set { instance._nodeSpawnerParent = value; }
    }
    public Transform _nodeSpawnerParent;
    private void Awake()
    {
        instance = this;
    }
    public Transform current;
    // Update is called once per frame
    void Update()
    {
        if(current != null)
        {
            Debug.Log("dragging");
            current.position = Input.mousePosition;
        }
    }
    public void onPutDown(LangNode display)
    {
        LangNode overlappingNode = null;
        if (!isOverAnotherNode(display, out overlappingNode)) {
            return;
        }
        var dnode = display.GetComponent<LangNode>();
        if (overlappingNode.nextNode != null)
        {
            dnode.nextNode = overlappingNode.nextNode;
        }
        overlappingNode.nextNode = dnode;
        SnapNodes(overlappingNode, display);
    }
    /// <summary>
    /// Returns whether or not this node is over another node
    /// </summary>
    /// <param name="display">the display node to check</param>
    /// <param name="node">outputs the intersecting langnode if true and null otherwise</param>
    /// <returns></returns>
    bool isOverAnotherNode(LangNode display, out LangNode node)
    {
        foreach(RectTransform child in _nodeParent)
        {
            if (child == display.transform)
                continue;
            if (rectOverlaps((RectTransform)display.transform, child))
            {
                node = child.GetComponent<LangNode>();
                return true;
            }
        }
        node = null;
        return false;
    }
    bool isOverAnotherNode(VariableNode display, out LangNode node)
    {
        foreach (RectTransform child in _nodeParent)
        {
            if (child == display.transform)
                continue;
            if (rectOverlaps((RectTransform)display.transform, child))
            {
                node = child.GetComponent<LangNode>();
                return true;
            }
        }
        node = null;
        return false;
    }
    /// <summary>
    /// Snaps b to a
    /// </summary>
    void SnapNodes(LangNode a, LangNode b)
    {
        var recta = (RectTransform)a.transform;
        var rectb = (RectTransform)b.transform;
        rectb.anchoredPosition = recta.anchoredPosition + new Vector2(0, -recta.rect.height/2 - rectb.rect.height/2);
        rectb.SetParent(recta);
    }
    void SnapNodes(VariableNode a, LangNode b)
    {
        var recta = (RectTransform)a.transform;
        var rectb = (RectTransform)b.transform;
        rectb.anchoredPosition = recta.anchoredPosition + new Vector2(0, -recta.rect.height / 2 - rectb.rect.height / 2);
        rectb.SetParent(recta);
    }
    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
    {
        Rect rect1 = new Rect(rectTrans1.anchoredPosition.x, rectTrans1.anchoredPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
        Rect rect2 = new Rect(rectTrans2.anchoredPosition.x, rectTrans2.anchoredPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

        return rect1.Overlaps(rect2);
    }

}
