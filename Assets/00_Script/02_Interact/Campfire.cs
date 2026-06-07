using UnityEngine;

public class Campfire : MonoBehaviour , IInteractable
{
    [SerializeField] private ParticleSystem fire;

    private void Update()
    {
        if (fire == null || GameManager.Instance == null)
            return;

        float fireRatio = GameManager.Instance.FireCurrentHealth / GameManager.Instance.FireMaxHealth;
        fireRatio = Mathf.Clamp01(fireRatio);

        var main = fire.main;
        main.startSize = Mathf.Lerp(0.1f, 1, fireRatio);

        var emission = fire.emission;
        emission.rateOverTime = Mathf.Lerp(0, 30.0f, fireRatio);

        if (fireRatio <= 0f && fire.isPlaying)
        {
            fire.Stop();
        }
        else if (fireRatio > 0f && !fire.isPlaying)
        {
            fire.Play();
        }


    }
    public void Interact(Player player)
    {
        var itemType = player.PlayerInventory.GetCurrentItemType();
        switch(itemType)
        {
            //========요리===========
            case ItemType.Apple:
                {
                    var item = player.PlayerInventory.GetCurrentItem() as Apple;
                    if (item == null)
                        return;

                    player.SetCookingDuration(item.CookDuration); // 플레이어의 요리 시간 설정
                    player.stateMachine.ChangeState(player.stateMachine.cookingState); // 플레이어의 상태를

                    break;
                }

            case ItemType.Fish:
                {
                    var item = player.PlayerInventory.GetCurrentItem() as Fish;
                    if (item == null)
                        return;

                    player.SetCookingDuration(item.CookDuration); // 플레이어의 요리 시간 설정
                    player.stateMachine.ChangeState(player.stateMachine.cookingState); // 플레이어의 상태를

                    break;
                }

            //========뗄감 넣기==========
            case ItemType.Plant:
                {
                    player.PlayerInventory.RemoveItem();
                    GameManager.Instance.IncreaseFireHealth(10.0f);
                    break;
                }
            case ItemType.Wood:
                {
                    player.PlayerInventory.RemoveItem();
                    GameManager.Instance.IncreaseFireHealth(30.0f);
                    break;
                }
            default:
                {
                    Debug.Log("모닥불에서 할 수 있는 행동이 없습니다");
                    return;
                }

        }

    }

}
