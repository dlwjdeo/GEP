using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    [Header("낚시 시 획득 가능한 아이템 목록")]
    [SerializeField] private GameObject FishPrefab; // 낚시로 얻을 수 있는 아이템 프리팹
    [SerializeField] private GameObject IronPrefab; // 낚시로 얻을 수 있는 아이템 프리팹
    [SerializeField] private GameObject WoodPrefab; // 낚시로 얻을 수 있는 아이템 프리팹
    [SerializeField] private GameObject PlantPrefab; // 낚시로 얻을 수 있는 아이템 프리팹
    [Header("낚시 성공 확률")]
    [SerializeField] private float fishSuccessRate = 0.5f; // 낚시 성공 확률 (0.0 ~ 1.0)
    [SerializeField] private float woodSuccessRate = 0.2f; // 나무 획득 확률 (0.0 ~ 1.0)
    [SerializeField] private float plantSuccessRate = 0.2f; // 식물 획득 확률 (0.0 ~ 1.0)
    [SerializeField] private float ironSuccessRate = 0.1f; // 철 획득 확률 (0.0 ~ 1.0)

    public GameObject GetRandomFishingItem()
    {
        float randomValue = Random.value; // 0.0 ~ 1.0 사이의 랜덤 값

        if (randomValue < fishSuccessRate)
        {
            fishSuccessRate -= 0.05f; // 낚시 성공 확률 감소
            ironSuccessRate += 0.05f; // 철 획득 확률 증가
            return FishPrefab; // 낚시 성공, 물고기 아이템 반환
        }
        else if (randomValue < fishSuccessRate + woodSuccessRate)
        {
            woodSuccessRate -= 0.05f; // 나무 획득 확률 감소
            ironSuccessRate += 0.05f; // 철 획득 확률 증가
            return WoodPrefab; // 나무 획득
        }
        else if (randomValue < fishSuccessRate + woodSuccessRate + plantSuccessRate)
        {
            plantSuccessRate -= 0.05f; // 식물 획득 확률 감소
            ironSuccessRate += 0.05f; // 철 획득 확률 증가
            return PlantPrefab; // 식물 획득
        }
        else if (randomValue < fishSuccessRate + woodSuccessRate + plantSuccessRate + ironSuccessRate)
        {
            initialization(); // 확률 초기화
            return IronPrefab; // 철 획득
        }
        else
        {
            return null; // 아무것도 획득하지 못함
        }
    }

    private void initialization()
    {
        // 초기 확률 설정
        fishSuccessRate = 0.5f;
        woodSuccessRate = 0.2f;
        plantSuccessRate = 0.2f;
        ironSuccessRate = 0.1f;
    }
}
