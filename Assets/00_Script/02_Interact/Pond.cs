using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour , IInteractable
{   
    public void Interact(Player player)
    {
        var itemType = player.PlayerInventory.GetCurrentItemType();
        if(itemType==ItemType.Bucket)
        {
            var bucket=player.PlayerInventory.currentItemObject.GetComponent<Bucket>();
            if (bucket == null)
            {
                Debug.Log("Bucket.cs찾을 수 없음");
                return;
            }
            if(bucket.isFilled)
            {
                Debug.Log("이미 양동이가 차있음");
                return;
            }

            bucket.Fill();
            return;
            //여기서 버킷에 물 채우는 효과
        }
        else if(itemType==ItemType.None && player.PlayerInventory.isUnlockFishing)  //손이 비어있으면서 낚시대가 해금
        {
            //낚시
            Debug.Log("낚시");
            player.SetFishingDuration(2.0f);
            player.stateMachine.ChangeState(player.stateMachine.fishingState);
            return;
        }
        else
        {
            Debug.Log("연못에 관련된 행동을 할 수 없습니다");
        }
        
    }
}
