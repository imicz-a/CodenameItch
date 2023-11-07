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
    public Vector2 refResolution = new Vector2(1920, 1080);
    // Update is called once per frame
    void Update()
    {
        if(current != null)
        {
            Debug.Log("dragging");
            current.position = Input.mousePosition;
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Delete))
            {
                if(current.gameObject != CrossCompiler.entryPoint.gameObject)
                    Destroy(current.gameObject);
            }
        }
    }
    public void onPutDown(DraggableObject o)
    {
        if(o is InstructionNode)
        {
            onPutDown(o as InstructionNode);
        }
        else if(o is VariableNode)
        {
            onPutDown(o as VariableNode);
        }
        else
        {
            throw new System.Exception("this class cannot be put down!");
        }
    }
    public void onPutDown(InstructionNode display)
    {
        InstructionNode overlappingNode;
        if (!isOverAnotherNode(display, out overlappingNode))
        {
            return;
        }
        bool wasInserted = false;
        if(overlappingNode is ContainerNode)
        {
            display.currentContainer = overlappingNode as ContainerNode;
        }
        else if(overlappingNode.currentContainer != null)
        {
            display.currentContainer = overlappingNode.currentContainer;
        }

        if (overlappingNode.nextNode != null)
        {
            if (display is ContainerNode)
            {
                overlappingNode.nextNode.previousNode = (display as ContainerNode).end;
                (display as ContainerNode).end.nextNode = overlappingNode.nextNode;
                wasInserted = true;
            }
            else
            {
                overlappingNode.nextNode.previousNode = display;
                display.nextNode = overlappingNode.nextNode;
                wasInserted = true;
            }
        }
        display.previousNode = overlappingNode;
        overlappingNode.nextNode = display;
        SnapNodes(overlappingNode, display);
        if (wasInserted)
        {
            display.ReSnap();
        }
    }
    public void onPutDown(VariableNode v)
    {
        Debug.Log("Putting var down");
        LangNode overlappingNode;
        if (!isOverAnotherNodeIncludingVars(v, out overlappingNode))
        {
            Debug.Log("var is not over another node");
            return;
        }
        Debug.Log("Var is over another node");
        if (!overlappingNode.hasVariable)
        {
            Debug.Log("Node does not have variable");
            return;
        }
        Debug.Log("inserting node");
        overlappingNode.insertVariable(v, localPointerPos);
    }
    /// <summary>
    /// Returns whether or not this node is over another node
    /// </summary>
    /// <param name="d">the draggable object to check</param>
    /// <param name="node">outputs the intersecting instructionnode if true and null otherwise</param>
    /// <returns></returns>
    bool isOverAnotherNode(DraggableObject d, out InstructionNode node)
    {
        SetPointer();
        foreach(RectTransform child in _nodeParent)
        {
            InstructionNode capturedNode;
            if(!child.TryGetComponent(out capturedNode))
            {
                continue;
            }
            if (searchTransform(capturedNode, d.transform as RectTransform, out node))
            {
                return true;
            }
        }
        node = null;
        return false;
    }
    bool isOverAnotherNodeIncludingVars(DraggableObject d, out LangNode node)
    {
        SetPointer();
        foreach (RectTransform child in _nodeParent)
        {
            LangNode capturedNode;
            if (!child.TryGetComponent(out capturedNode))
            {
                continue;
            }
            if (searchTransformIncludingVars(capturedNode, d.transform as RectTransform, out node))
            {
                return true;
            }
        }
        node = null;
        return false;
    }
    bool searchTransformIncludingVars(LangNode check, RectTransform dobject, out LangNode node)
    {
        if (check.transform as RectTransform == dobject)
        {
            goto childsearch;
        }
        if (pointerOverlaps(check.transform as RectTransform))
        {
            InstructionArgument argument;
            if (check.doArgsOverlap(out argument))
            {
                if (argument.isAssigned)
                {
                    if(searchTransformIncludingVars(argument.assignedVar, dobject, out node))
                    {
                        return true;
                    }
                    
                }
            }
            node = check.GetComponent<LangNode>();
            return true;
        }
    childsearch:
        if (check is InstructionNode)
        {
            var ins = check as InstructionNode;
            if (ins.nextNode == null)
                goto end;
            if (searchTransformIncludingVars(ins.nextNode, dobject, out node))
            {
                return true;
            }
        }
    end:
        node = null;
        return false;
    }
    bool searchTransform(InstructionNode check, RectTransform dobject, out InstructionNode node)
    {
        if (check.transform as RectTransform == dobject)
        {
            goto childsearch;
        }
        if (pointerOverlaps(check.transform as RectTransform))
        {
            node = check.GetComponent<InstructionNode>();
            return true;
        }
    childsearch:
        if (check.nextNode == null)
            goto end;
        if (searchTransform(check.nextNode, dobject, out node))
        {
            return true;
        }
    end:
        node = null;
        return false;
    }
    /// <summary>
    /// Snaps b to a
    /// </summary>
    public void SnapNodes(InstructionNode a, InstructionNode b)
    {
        Debug.Log($"Snapping {b.gameObject.name} to {a.gameObject.name}");
        var recta = (RectTransform)a.transform;
        var rectb = (RectTransform)b.transform;
        rectb.SetParent(recta);
        rectb.anchoredPosition = new Vector2(recta.rect.width/2,
            -rectb.rect.height/2);
        
    }

    [SerializeField] RectTransform tempPointerChecker;
    [SerializeField] RectTransform currentCanvas;
    Vector2 localPointerPos;
    void SetPointer()
    {
        Vector2 guiScale = getUIScale();
        Vector2 debog = Input.mousePosition * guiScale;
        tempPointerChecker.anchoredPosition = debog;
        tempPointerChecker.SetParent(_nodeParent);
        localPointerPos = tempPointerChecker.anchoredPosition;
        tempPointerChecker.SetParent(currentCanvas);
    }
    public bool pointerOverlaps(RectTransform rectTrans)
    {
        Transform parent = rectTrans.parent;
        int chindex = rectTrans.GetSiblingIndex();
        rectTrans.SetParent(_nodeParent);
        
        Rect rect2 = new Rect(rectTrans.anchoredPosition.x-(rectTrans.rect.width/2),
            rectTrans.anchoredPosition.y-(rectTrans.rect.height/2),
            rectTrans.rect.width, rectTrans.rect.height);
        bool debug = rect2.Contains(localPointerPos);
        rectTrans.SetParent(parent);
        rectTrans.SetSiblingIndex(chindex);
        return debug;
    }
    public bool pointerOverlaps(RectTransform rectTrans, InstructionArgument arg)
    {
        Transform parent = rectTrans.parent;
        int chindex = rectTrans.GetSiblingIndex();
        rectTrans.SetParent(_nodeParent);
        var lay = arg.GetComponent<UnityEngine.UI.LayoutElement>();
        lay.ignoreLayout = true;
        Vector3 pos = rectTrans.position;
        rectTrans.anchorMin = Vector2.zero;
        rectTrans.anchorMax = Vector2.zero;
        rectTrans.position = pos;
        Rect rect2 = new Rect(rectTrans.anchoredPosition.x - (rectTrans.rect.width / 2),
            rectTrans.anchoredPosition.y - (rectTrans.rect.height / 2),
            rectTrans.rect.width, rectTrans.rect.height);
        bool debug = rect2.Contains(localPointerPos);
        lay.ignoreLayout = false;
        rectTrans.SetParent(parent);
        rectTrans.SetSiblingIndex(chindex);
        return debug;
    }
    Vector2 getUIScale()
    {
        return refResolution/new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }
}
