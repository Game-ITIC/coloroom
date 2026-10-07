using UnityEngine;

public static class WebGLMobilePerformance
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Configure()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Application.isMobilePlatform)
            Application.targetFrameRate = 30;
#endif
    }
}
