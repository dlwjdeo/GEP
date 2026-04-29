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