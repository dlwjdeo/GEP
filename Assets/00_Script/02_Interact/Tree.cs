using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Item, IInteractable
{
    [SerializeField] private GameObject logPrefab; // 나무가 베어질 때 생성될 통나무 프리팹
    [SerializeField] private bool isGrown = true; // 나무가 자란 상태인지 여부
    [SerializeField] private float growTime = 5f; // 나무가 자라는 데 걸리는 시간

    void Awake()
    {
        isGrown = false;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // 나무가 자라기 전에는 보이지 않도록 설정
    }

    private void Update()
    {
        if (!isGrown)
        {
            growTime -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f),
                                                Vector3.one, (5f - growTime) / 5f); // 나무가 자라는 동안 크기를 점점 키움

            if (growTime <= 0f)
            {
                isGrown = true;
                transform.localScale = Vector3.one; // 나무가 자란 후에는 원래 크기로 설정
            }
        }
    }

    public void Interact(Player player)
    {
        if(!isGrown) return;
        
        player.SetPrefabToSpawn(logPrefab);
        player.stateMachine.ChangeState(player.stateMachine.loggingState);
    }
}
