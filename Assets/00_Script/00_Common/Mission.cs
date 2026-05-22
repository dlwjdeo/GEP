using UnityEngine;


[CreateAssetMenu]
public class Mission : ScriptableObject
{
    public int id;
    public string content;
    public bool isCompleted=false;
}
