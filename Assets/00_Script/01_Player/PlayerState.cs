using UnityEngine;

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}
public class IdleState : IState
{
    private Player player;

    public IdleState(Player player)
    {
        this.player = player;
    }
    public void Enter()
    {
        player.PlayerMovement.SetMoveSpeed(0f);
    }

    public void Update()
    {
        if(player.movementInput.magnitude > 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.moveState);
            return;
        }
        
    }

    public void Exit()
    {
        
    }
}

public class MoveState : IState
{
    private Player player;

    public MoveState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.PlayerMovement.SetMoveSpeed(player.PlayerMovement.MoveSpeed);
    }

    public void Update()
    {
        if(player.movementInput.magnitude < 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.idleState);
            return;
        }
        if (player.movementInput.magnitude > 0.1f)
        {
            player.PlayerMovement.Move(player.movementInput);

        }
    }

    public void Exit()
    {
        
    }
}

public class FishingState : IState
{
    private Player player;
    private float fishingDuration;
    public FishingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.PlayerMovement.SetMoveSpeed(0f);
        fishingDuration = player.fishingDuration;
        //fishingDuration = Random.Range(2f, 5f); // 낚시 시간 랜덤 설정 (예시)
    }

    public void Update()
    {
        if(player.movementInput.magnitude > 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.moveState);
            return;
        }
        if(fishingDuration > 0f)
        {
            fishingDuration -= Time.deltaTime;
        }
        else
        {
            // 낚시 완료 처리
            var item = GameObject.Instantiate(GameManager.Instance.FishingManager.GetRandomFishingItem());
            item.GetComponent<IPickupable>()?.Pickup(player);
            GameManager.Instance.MissionManager.CompleteMission(MissionType.Fish);
            player.stateMachine.ChangeState(player.stateMachine.idleState);
        }
    }
    public void Exit()
    {
        
    }
}

public class LoggingState : IState
{
    private Player player;
    private float loggingDuration;

    public LoggingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        loggingDuration = player.loggingDuration;
    }

    public void Update()
    {
        if(player.movementInput.magnitude > 0.1f)
        {
            player.stateMachine.ChangeState(player.stateMachine.moveState);
            return;
        }
        if(loggingDuration > 0f)
        {
            loggingDuration -= Time.deltaTime;
        }
        else
        {
            // 나무 베기 완료 처리
            if (player.prefabToSpawn != null)
            {
                Debug.Log("나무 베기 완료! 통나무 획득!");
                GameObject.Instantiate(player.prefabToSpawn, player.FowardGameObject.transform.position + Vector3.forward, Quaternion.identity);
                GameObject.Destroy(player.FowardGameObject);
                player.SetPrefabToSpawn(null);
            }
            player.stateMachine.ChangeState(player.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        
    }
}

public class EatingState : IState
{
    private Player player;
    private float eatingDuration;

    public EatingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.PlayerMovement.SetMoveSpeed(0f);
        eatingDuration = player.eatingDuration;
    }

    public void Update()
    {
        if(eatingDuration > 0f)
        {
            eatingDuration -= Time.deltaTime;
        }
        else
        {
            // 먹기 완료 처리
            GameManager.Instance.IncreasePlayerHealth(player.PlayerInventory.currentItemObject.GetComponent<IEatable>().FillAmount); // 체력 회복
            player.PlayerInventory.RemoveItem(); // 아이템 제거
            
            Debug.Log("먹기 완료!");

            player.stateMachine.ChangeState(player.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        
    }
}

public class CookingState : IState
{
    private Player player;
    private float cookingDuration;

    public CookingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.PlayerMovement.SetMoveSpeed(0f);
        cookingDuration = player.cookingDuration;
    }

    public void Update()
    {
        if(cookingDuration > 0f)
        {
            cookingDuration -= Time.deltaTime;
        }
        else
        {
            // 요리 완료 처리
            GameObject cookedItem = player.PlayerInventory.currentItemObject; // 현재 아이템 가져오기
            if (cookedItem != null && cookedItem.TryGetComponent<ICookable>(out var cookable))
            {
                cookedItem = cookable.Cooked(); // 요리 완료 처리
                if (cookedItem != null)
                {
                    player.PlayerInventory.RemoveItem(); // 기존 아이템 제거
                    GameObject.Instantiate(cookedItem).GetComponent<IPickupable>().Pickup(player); // 요리된 아이템을 플레이어가 픽업
                }
            }
            player.stateMachine.ChangeState(player.stateMachine.idleState);
        }
    }

    public void Exit()
    {
        
    }
}