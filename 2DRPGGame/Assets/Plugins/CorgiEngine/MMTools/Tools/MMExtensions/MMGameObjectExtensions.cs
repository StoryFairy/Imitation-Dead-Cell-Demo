using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class MMGameObjectExtension
{
    private static List<Component> m_ComponentCache = new List<Component>();
    
    public static T MMGetComponentNoAlloc<T>(this GameObject @this) where T : Component
    {
        @this.GetComponents(typeof(T), m_ComponentCache);
        Component component = m_ComponentCache.Count > 0 ? m_ComponentCache[0] : null;
        m_ComponentCache.Clear();
        return component as T;
    }
}
