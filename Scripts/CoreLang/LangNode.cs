using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(RectTransform))]
public class LangNode : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] bool isClonable;
    public LangNode nextNode;
    public virtual void Init() { }
    public virtual void CrossCompile()
        { throw new System.NotImplementedException("this should be implemented!"); }
    public virtual void Grab()
    {
        if (isClonable)
        {
            var clone = Instantiate(gameObject);
            clone.transform.SetParent(NodeDragManager.nodeSpawnerParent, false);
            clone.transform.position = transform.position;
            clone.name = name;
            isClonable = false;
            transform.SetParent(NodeDragManager.nodeParent, false);
            NodeDragManager.instance.current = transform;
            return;
        }
        NodeDragManager.instance.current = transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging begins");
        Grab();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging stops");
        PutDown();
    }

    public virtual void PutDown()
    {
        NodeDragManager.instance.current = null;
        NodeDragManager.instance.onPutDown(this);
    }
}
