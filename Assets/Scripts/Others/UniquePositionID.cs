using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniquePositionID
{
    public static string GetUPID(this GameObject gameObject)
    {
        return gameObject.transform.position.GetHashCode().ToString();
    }
}
