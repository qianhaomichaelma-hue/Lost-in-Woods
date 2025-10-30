using UnityEngine;

public class PortalPairTeleport : MonoBehaviour
{
    public Transform linkedPortalExit; // ✅ 不是传送门本体，而是出口点
    public bool keepRotation = true;

    private bool canTeleport = true;

    private AudioSource audioSource;
    public AudioClip teleportSound;

    private void OnTriggerEnter(Collider other)
    {
        if (canTeleport && other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
        }
    }

    private System.Collections.IEnumerator Teleport(Collider player)
    {
        canTeleport = false;

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) controller.enabled = false;

        // ✅ 传送到传送门出口点，而不是传送门本体
        player.transform.position = linkedPortalExit.position;
        if (keepRotation)
            player.transform.rotation = linkedPortalExit.rotation;

        if (controller != null) controller.enabled = true;

        AudioSource.PlayClipAtPoint(teleportSound, player.transform.position, 1f);

        // 防止传送后立刻再次触发
        yield return new WaitForSeconds(0.4f);

        // 重新允许传送
        linkedPortalExit.parent.GetComponent<PortalPairTeleport>().canTeleport = true;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
