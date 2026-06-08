using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro messageText;
    [SerializeField] private float yOffset = 2f;
    [SerializeField] private float zOffset = 0f;
    private Vector3 fixedWorldEulerAngles = new Vector3(0f, 0f, 0f);

    public string currentMessage { get; private set; }

    private Player player;
    private GameObject followTarget;
    private bool shouldFollowTarget;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        messageText.gameObject.SetActive(false);
        messageText.transform.rotation = Quaternion.Euler(fixedWorldEulerAngles);
    }

    private void OnEnable()
    {
        if (player != null)
        {
            player.OnChangeForwardGameObject += ShowMessage;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnChangeForwardGameObject -= ShowMessage;
        }
    }

    private void LateUpdate()
    {
        if (!messageText.gameObject.activeSelf) return;
        if (!shouldFollowTarget) return;
        if (followTarget == null)
        {
            Hide();
            return;
        }

        messageText.transform.position = followTarget.transform.position + new Vector3(0f, yOffset, zOffset);
        messageText.transform.rotation = Quaternion.Euler(fixedWorldEulerAngles);
    }

    public void ShowMessage(string message, GameObject targetObject)
    {
        if (string.IsNullOrEmpty(message))
        {
            Hide();
            return;
        }

        currentMessage = message;
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        if (player.PlayerInventory.GetCurrentItemType() == ItemType.Apple)
        {
            followTarget = player.gameObject;
            shouldFollowTarget = true;
            messageText.transform.position = followTarget.transform.position + new Vector3(0f, yOffset, zOffset);
        }
        else
        {
            shouldFollowTarget = false;
            followTarget = null;

            Vector3 basePosition;

            if (targetObject != null)
            {
                basePosition = targetObject.transform.position;
            }
            else
            {
                basePosition = transform.position;
            }

            messageText.transform.position = basePosition + new Vector3(0f, yOffset, zOffset);
        }

        messageText.transform.rotation = Quaternion.Euler(fixedWorldEulerAngles);
    }

    public void Hide()
    {
        currentMessage = string.Empty;
        followTarget = null;
        shouldFollowTarget = false;
        messageText.gameObject.SetActive(false);
    }
}