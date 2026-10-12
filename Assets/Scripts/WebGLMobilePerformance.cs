using UnityEngine;

public static class WebGLMobilePerformance
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Configure()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.targetFrameRate = Application.isMobilePlatform || SystemInfo.deviceType == DeviceType.Handheld ? 30 : 60;
#endif
    }
}
