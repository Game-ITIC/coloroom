using System.Collections;
using UnityEngine;

public class SelectOutliner: MonoBehaviour
{
    public static SelectOutliner Instance;

    [SerializeField] private Outline.Mode mode = Outline.Mode.OutlineVisible;
    [SerializeField] private Color color = Color.black;
    [SerializeField] private float width = 0.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void Select(GameObject obj)
    {
        Outline o = obj.GetComponent<Outline>();
        
        if (o == null)
            o = obj.AddComponent<Outline>();

        o.OutlineMode = mode;
        o.OutlineColor = color;
        o.OutlineWidth = width;

        o.enabled = true;
    }

    public void Unselect(GameObject obj)
    {
        Outline o = obj.GetComponent<Outline>();

        if (o == null) return;

        o.enabled = false;
    }
}
