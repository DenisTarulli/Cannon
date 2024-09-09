using UnityEngine;

[CreateAssetMenu(fileName = "Shot", menuName = "New Shot")]
public class LastShotInfo : ScriptableObject
{
    public void ResetValues()
    {
        Force = 0f;
        X_Angle = 0f;
        Y_Angle = 0f;
        ImpactForce = 0f;
    }

    public void ResetIndex()
    {
        ShotIndex = 0;
    }

    public float ShotIndex;
    public float Force;
    public float X_Angle;
    public float Y_Angle;
    public float ImpactForce;
}
