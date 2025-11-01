using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission Object")]
public class MissionObject : ScriptableObject
{
    [SerializeField] private string missionName = "";
    [TextArea]
    [SerializeField] private string description = "";
    [SerializeField] private bool isAssigned = false;
    [SerializeField] private bool isCompleted = false;

    public string MissionName => missionName;
    public string Description => description;
    public bool IsAssigned => isAssigned;
    public bool IsCompleted => isCompleted;

    public void LoadMission()
    {
        isAssigned = true;
        isCompleted = false;
        Debug.Log($"Mission '{missionName}' loaded.");
    }

    public void CompleteMission()
    {
        if (isAssigned)
        {
            isCompleted = true;
            Debug.Log($"Mission '{missionName}' completed.");
        }
        else
        {
            Debug.LogWarning($"Mission '{missionName}' not assigned yet.");
        }
    }




#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(missionName))
        {
            string path = AssetDatabase.GetAssetPath(this);
            missionName = System.IO.Path.GetFileNameWithoutExtension(path);
            EditorUtility.SetDirty(this);
        }
    }
#endif

    // ---------- Equality by Mission Name ----------

    public override bool Equals(object obj)
    {
        if (obj is MissionObject other)
            return missionName == other.missionName;
        return false;
    }

    public override int GetHashCode()
    {
        return missionName != null ? missionName.GetHashCode() : 0;
    }

    public static bool operator ==(MissionObject a, MissionObject b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.missionName == b.missionName;
    }

    public static bool operator !=(MissionObject a, MissionObject b)
    {
        return !(a == b);
    }
}
