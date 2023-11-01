using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool isClonable;
    public virtual void Grab()
    {
        if (isClonable)
        {
            var clone = Instantiate(gameObject);
            clone.transform.SetParent(transform.parent, false);
            clone.transform.position = transform.position;
            clone.name = name;
            clone.transform.SetAsLastSibling();
            (transform as RectTransform).anchorMax = Vector2.zero;
            (transform as RectTransform).anchorMin = Vector2.zero;
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
