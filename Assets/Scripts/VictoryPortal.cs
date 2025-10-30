using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPortal : MonoBehaviour
{
    [Header("下一关卡名称（必须与 Build Settings 中一致）")]
    public string nextSceneName = "Victory Scene";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
