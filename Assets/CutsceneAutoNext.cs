using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneAutoNext : MonoBehaviour
{
    [SerializeField] private float duration = 6f;

    private void Start()
    {
        Invoke(nameof(Next), duration);
    }

    private void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}