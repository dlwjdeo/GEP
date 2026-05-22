using UnityEngine;


[CreateAssetMenu]
public class Mission : ScriptableObject
{
    public int id;
    public MissionType type;
    public string content;
    public bool isCompleted=false;
}
