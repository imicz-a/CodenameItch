using System.Collections.Generic;
using UnityEngine;
public class SerializableHashSet<T> : ScriptableObject, ISerializationCallbackReceiver
{
    private HashSet<T> hashSet = new HashSet<T>();

    [SerializeField]
    private List<T> serializableItems;

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
        this.serializableItems = new List<T>(this.hashSet);
        this.hashSet = null;
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        foreach (T item in this.serializableItems)
        {
            _ = this.hashSet.Add(item);
        }

        this.serializableItems = null;
    }

    public bool Add(T item) => this.hashSet.Add(item);

    public bool Contains(T item) => this.hashSet.Contains(item);
}