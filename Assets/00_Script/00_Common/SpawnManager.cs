using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    //랜덤 좌표의 위치 후보지
    public struct SpawnCandidate
    {
        public Vector3 position;
        public float weight;

        public SpawnCandidate(Vector3 position, float weight)
        {
            this.position = position;
            this.weight = weight;
        }
    }

    [SerializeField] private Collider groundCollider;

    [SerializeField] private Transform rocket;
    [SerializeField] private Transform pond;
    [SerializeField] private Transform campfire;
    [SerializeField] private Transform player;

    [SerializeField] private float rayStartHeight = 50.0f;
    [SerializeField] private int sampleCount = 50;
    [SerializeField] private float minDistanceFromItems = 5.0f;
    [SerializeField] private LayerMask itemLayerMask;

    [SerializeField] private Iron ironPrefab;
    [SerializeField] private Plant plantPrefab;

    public Transform Rocket => rocket;
    public Transform Pond => pond;
    public Transform Campfire => campfire;
    public Transform Player => player;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    public Item Spawn(Item prefab)
    {
        Vector3? spawnPosition = GetSpawnPosition(prefab);

        if (!spawnPosition.HasValue)
        {
            Debug.LogWarning($"{prefab.name} 스폰 위치를 찾지 못했습니다.");
            return null;
        }
        Vector3 finalPosition = spawnPosition.Value + Vector3.up * 0.55f;
        return Instantiate(prefab, finalPosition, Quaternion.identity);
    }

    private Vector3? GetSpawnPosition(Item item)
    {
        List<SpawnCandidate> candidates = BuildCandidates(item);

        if (candidates.Count == 0)
            return null;

        return PickWeightedRandom(candidates);
    }

    private List<SpawnCandidate> BuildCandidates(Item item)
    {

        //1.초기에 Ground(사각형)에 내접하는 원 범위 내로 스폰장소 필터링
        List<SpawnCandidate> candidates = new List<SpawnCandidate>();
        Bounds bounds = groundCollider.bounds;

        Vector3 center = bounds.center;
        float radius = Mathf.Min(bounds.extents.x, bounds.extents.z);



        //2.sampleCount까지 만약 적절한 위치를 찾지 못햇다면 다시 검사 ex)위치가 특정되도 그 자리에 y축 ray를 쐈을 때 이미 오브젝트가 탐지되면 재검사
        for (int i = 0; i < sampleCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * radius;

            float x = center.x + randomOffset.x;
            float z = center.z + randomOffset.y;

            Vector3 rayStart = new Vector3(x, bounds.max.y + rayStartHeight, z);

            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 200f))
                continue;

            if (hit.collider != groundCollider)
                continue;

            Vector3 point = hit.point;

            if (IsOverlapping(point))
                continue;

            //만약 적절한 위칠를 찾았다면 해당 위치를 기준 설치 오브젝트와의 거리에 비례해여 가중치 생성
            float weight = item.GetSpawnWeight(point, this);

            if (weight <= 0f)
                continue;

            candidates.Add(new SpawnCandidate(point, weight));
        }

        return candidates;
    }

    private bool IsOverlapping(Vector3 point)
    {   
       
        return Physics.CheckSphere(point, minDistanceFromItems, itemLayerMask);
    }

    private Vector3 PickWeightedRandom(List<SpawnCandidate> candidates)
    {   

        //모여진 최종 위치 후보군중에서 가중치에 따라 랜덤으로 뽑기 진행.
        float totalWeight = 0f;

        for (int i = 0; i < candidates.Count; i++)
        {
            totalWeight += candidates[i].weight;
        }

        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;

        for (int i = 0; i < candidates.Count; i++)
        {
            cumulative += candidates[i].weight;

            if (randomValue <= cumulative)
                return candidates[i].position;
        }

        return candidates[candidates.Count - 1].position;
    }

    private System.Collections.IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn(ironPrefab);
            Spawn(plantPrefab);
            yield return new WaitForSeconds(3.0f);
        }
    }


}