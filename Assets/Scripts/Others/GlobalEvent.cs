using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEvent : MonoBehaviour
{
    private static List<GlobalEvent> _all = new List<GlobalEvent>();

    [Serializable] public class KeyEvent
    {
        public string globalKey = "";
        public float delay = 0f;
        public UnityEvent action = new UnityEvent();
        public Coroutine delayedAction = null;
    }

    [SerializeField] private bool active = true;
    [SerializeField] private KeyEvent[] events;

    private void Awake()
    {
        if (active) _all.Add(this);
    }

    public static void InvokeGlobal(string key)
    {
        foreach (var ge in _all.ToList()) ge.Invoke(key);
    }

    public void Invoke(string key)
    {
        foreach (var e in events)
            if (e.globalKey != "" && e.globalKey == key)
            {
                if (gameObject.activeInHierarchy)
                {
                    if (e.delayedAction != null) StopCoroutine(e.delayedAction);

                    e.delayedAction = this.DelayedAction(e.delay, e.action.Invoke);
                }
                else
                {
                    e.action.Invoke();
                }
                
            }
    }

    private void OnDestroy()
    {
        _all.Remove(this);
    }
}
