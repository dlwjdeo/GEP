using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class CraftManager : MonoBehaviour
{   

    //Type로 선언한 이유는, ItemType을 사용하지 않고 인스턴스된 클래스가 아닌 해당 클래스 존재자체(이름)를 비교하게 하기 위해서.
    private Dictionary<(Type, Type), Func<Player,bool>> _recipeTable = new Dictionary<(Type, Type), Func<Player, bool>>(new RecipeTypeComparer());



    private void Start()
    {   
        //제작의 결과물이 아이템이 될 수도 있지만, 만약 Equipment들이 아이템이 아닌 해금형식이나 등록 형식으로 사용된다면 유연하게 함수호출하여 해당 클래스 내부 값을 꾸게 해야하기 때문에 Value를 함수로.
        _recipeTable.Add((typeof(Wood), typeof(Iron)), (player)=>UnlockFishing(player));

    }

    public bool Craft(Item holdItem, Item targetItem,Player player)
    {
        var key = (holdItem.GetType(), targetItem.GetType());

        if (_recipeTable.TryGetValue(key, out var craftFunc))
        {
           
            return craftFunc.Invoke(player);
        }
        else
        {
            Debug.Log("레시피 없음");
            return false;
        }
    }

    private bool UnlockAxe()
    {   

        //나무 캐는 속도를 변경
        return true;
    }

    private bool UnlockFishing(Player player)
    {
        var inven = player.PlayerInventory;
        if (inven.isUnlockFishing)
            return false;
        
        inven.isUnlockFishing = true;
        return true;
    }

    private bool CraftBucket()
    {
        //Bucket을 아이템으로 본다면 아이템 생성 또는 물을 담을 수 있는 지역 활성화
        return true;
    }

}



//재료 순서가 달라도 같은 키값처럼 취급하기 위해 오퍼레이터 오버로딩
public class RecipeTypeComparer : IEqualityComparer<(Type, Type)>
{
    public bool Equals((Type, Type) x, (Type, Type) y)
    {
        return
            (x.Item1 == y.Item1 && x.Item2 == y.Item2) ||
            (x.Item1 == y.Item2 && x.Item2 == y.Item1);
    }

    public int GetHashCode((Type, Type) obj)
    {
        return obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode();
    }
}