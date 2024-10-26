using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShotStatsContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private TextMeshProUGUI forceText;
    [SerializeField] private TextMeshProUGUI xAngleText;
    [SerializeField] private TextMeshProUGUI yAngleText;
    [SerializeField] private TextMeshProUGUI impactForceText;

    public void SetValues(ShotInfoToDisplay info, int index)
    {
        indexText.text = index.ToString();
        forceText.text = info.Force.ToString();
        xAngleText.text = info.X_Angle.ToString();
        yAngleText.text= info.Y_Angle.ToString();
        impactForceText.text = info.ImpactForce.ToString();
    }
}
