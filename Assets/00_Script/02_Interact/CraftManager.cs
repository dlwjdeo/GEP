using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    // 재료 순서가 바뀌어도 같은 레시피로 처리하기 위해 비교자 사용
    private Dictionary<(ItemType, ItemType), Func<Player, bool>> _recipeTable
        = new Dictionary<(ItemType, ItemType), Func<Player, bool>>(new RecipeItemTypeComparer());

    [SerializeField] private GameObject bucketObj;

    private Item _hodItem;
    private Item _tagetItem;

    private event Action OnCraftCompleted;

    private void Awake()
    {
        OnCraftCompleted += HandleCraftCompleted;
    }

    private void Start()
    {
        // 예: 나무 + 철 = 낚시 해금
        _recipeTable.Add((ItemType.Wood, ItemType.Plant), player => UnlockFishing(player));
        _recipeTable.Add((ItemType.Iron, ItemType.Iron), player => CraftBucket(player));
        _recipeTable.Add((ItemType.Wood, ItemType.Iron), player => UnlockAxe(player));
    }

    private void OnDestroy()
    {
        OnCraftCompleted -= HandleCraftCompleted;
    }

    private void HandleCraftCompleted()
    {
        if (_hodItem != null)
            Destroy(_hodItem.gameObject);

        if (_tagetItem != null)
            Destroy(_tagetItem.gameObject);

        _hodItem = null;
        _tagetItem = null;
    }

    public bool IsCraft(ItemType holdItem, ItemType targetItem)
    {
        

        var key = (holdItem ,targetItem);
        return _recipeTable.ContainsKey(key);
    }

    public bool Craft(Item holdItem, Item targetItem, Player player)
    {

        if (holdItem == null || targetItem == null)
            return false;

        if (!IsCraft(holdItem.ItemType, targetItem.ItemType))
        {
            Debug.Log("레시피 없음");
            return false;
        }

        var key = (holdItem.ItemType, targetItem.ItemType);
        var craftFunc = _recipeTable[key];

        Debug.Log("제작성공");
        _hodItem = holdItem;
        _tagetItem = targetItem;

        return craftFunc.Invoke(player);
    }

    private bool UnlockAxe(Player player)
    {
        // 벌목 속도 증가
        var LD = player.loggingDuration;
        player.SetLoggingDuration(LD * 0.6f);

        OnCraftCompleted?.Invoke();
        return true;
    }

    private bool UnlockFishing(Player player)
    {
        var inven = player.PlayerInventory;

        if (inven.isUnlockFishing)
            return false;

        inven.isUnlockFishing = true;

        OnCraftCompleted?.Invoke();
        return true;
    }

    private bool CraftBucket(Player player)
    {
        var spawnPosition = player.FowardGameObject.transform.position;
        Instantiate(bucketObj, spawnPosition, Quaternion.identity);

        OnCraftCompleted?.Invoke();
        return true;
    }
}

// 재료 순서가 달라도 같은 키값처럼 취급
public class RecipeItemTypeComparer : IEqualityComparer<(ItemType, ItemType)>
{
    public bool Equals((ItemType, ItemType) x, (ItemType, ItemType) y)
    {
        return
            (x.Item1 == y.Item1 && x.Item2 == y.Item2) ||
            (x.Item1 == y.Item2 && x.Item2 == y.Item1);
    }

    public int GetHashCode((ItemType, ItemType) obj)
    {
        return obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode();
    }
}