
using UnityEngine;

public class Plant : Item, IPickupable,IInteractable
{
    private float nearRadius = 3f;
    private float farRadius = 12f;
    private float minWeight = 0.1f;
    private float maxWeight = 0.8f;
    public void Interact(Player player)
    {
        if (player.PlayerInventory.GetCurrentItemType() == ItemType.None)  //만약 맨손이라면 수리
        {
            //수리
            return;
        }
        //여기서부턴 제작

        var CM = GameManager.Instance.CraftManager;
        CM.Craft(player.PlayerInventory.GetCurrentItem(), this, player);

    }
    public void Pickup(Player player)
    {
        if (player.PlayerInventory.HasItem()) return; // 이미 아이템이 있는 경우 픽업 불가
        player.PlayerInventory.SetCurrentItem(gameObject);
        gameObject.transform.SetParent(player.transform); // 플레이어의 자식으로 설정하여 함께 이동하도록 함
        gameObject.transform.localPosition = Vector3.up; // 플레이어의 위치에 아이템
    }


    public override float GetSpawnWeight(Vector3 point, SpawnManager spawnManager)
    {
        Vector3 rocketPos = spawnManager.Rocket.transform.position;

        float distance = Vector2.Distance(
            new Vector2(point.x, point.z),
            new Vector2(rocketPos.x, rocketPos.z)
        );

        if (distance <= nearRadius)
            return minWeight;

        if (distance >= farRadius)
            return maxWeight;

        float t = Mathf.InverseLerp(nearRadius, farRadius, distance);
        return Mathf.Lerp(minWeight, maxWeight, t);
    }

}
