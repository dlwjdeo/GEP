using System.Collections;
using UnityEngine;
using TMPro;

public class SystemMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro messageText;   // 3D TextMeshPro
    [SerializeField] private float displayDuration = 3f;

    public string currentMessage { get; private set; }

    private Camera mainCam;
    private Player player;
    private GameObject currentTarget;
    private void Awake()
    {
        mainCam = Camera.main;
        player = FindObjectOfType<Player>();
        messageText.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (mainCam == null) return;
        if (currentTarget != null)
        {
            messageText.transform.position = currentTarget.transform.position + new Vector3(0, 2f, 0);
        }
        messageText.transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
    }

    void OnEnable()
    {
        if(player != null)
        {
            player.OnChangeForwardGameObject += ShowMessage;
        }
    }

    void OnDisable()
    {
        if(player != null)
        {
            player.OnChangeForwardGameObject -= ShowMessage;
        }
    }

    public void ShowMessage(string message, GameObject targetObject)
    {
        if(player.PlayerInventory.GetCurrentItemType() == ItemType.Apple && targetObject == null)
        {
            targetObject = player.gameObject;
        }
        else
        {
            if(targetObject != null)
            {
                currentTarget = targetObject;
            }
            else
            {
                currentTarget = gameObject;
            }
        }
        messageText.transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
        messageText.transform.localPosition = new Vector3(0, 2f, 0);
        ShowMessage(message);
    }

    public void ShowMessage(string message)
    {
        currentMessage = message;
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }

    public void SetPosition(Vector3 worldPosition)
    {
        transform.position = worldPosition;
    }

    public void Hide()
    {
        messageText.transform.SetParent(transform);
        messageText.gameObject.SetActive(false);
    }
}
