using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class AllaLoadingBuild
{
    public static void BuildWebGL()
    {
        var oldTemplate = PlayerSettings.WebGL.template;
        var oldSplash = PlayerSettings.SplashScreen.show;
        var output = Environment.GetEnvironmentVariable("ITIC_WEBGL_OUTPUT_PATH");
        if (string.IsNullOrWhiteSpace(output))
            output = "Build/WebGL-AllaLoadingTest";

        try
        {
            PlayerSettings.WebGL.template = "PROJECT:AllaLoading";
            PlayerSettings.SplashScreen.show = false;
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
                locationPathName = output,
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            });

            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception($"Alla loading-screen WebGL build failed: {report.summary.result}");

            Debug.Log($"Alla loading-screen WebGL build succeeded: {output}");
        }
        finally
        {
            PlayerSettings.WebGL.template = oldTemplate;
            PlayerSettings.SplashScreen.show = oldSplash;
        }
    }
}
