using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public void Quit()
    {
        // Закрывает игру в готовом билде (на Android, iOS, Windows и т.д.)
        Application.Quit();

        // Останавливает игру при тестировании в самом редакторе Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}