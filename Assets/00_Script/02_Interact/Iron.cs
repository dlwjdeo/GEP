using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Item, IInteractable, IPickupable
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
        if (player.PlayerInventory.HasItem()) return; 
        gameObject.transform.SetParent(player.transform); 
        gameObject.transform.localPosition = Vector3.up;
        player.PlayerInventory.SetCurrentItem(gameObject); 
    }

    
    public override float GetSpawnWeight(Vector3 point, SpawnManager spawnManager)
    {   

        //특정 설치형 오브젝트를 기준으로 잡고 후보거리와 오브젝트 사이의 거리를 기반으로 가중치 측정
        Vector3 rocketPos = spawnManager.Rocket.transform.position;

        float distance = Vector2.Distance(
            new Vector2(point.x, point.z),
            new Vector2(rocketPos.x, rocketPos.z)
        );

        if (distance <= nearRadius)
            return minWeight;

        if (distance >= farRadius)
            return maxWeight;

        //두 좌표사이의 거리에 따른 가중치값을 0~1로 반환
        float t = Mathf.InverseLerp(nearRadius, farRadius, distance);
        return Mathf.Lerp(minWeight, maxWeight, t);
    }
}
