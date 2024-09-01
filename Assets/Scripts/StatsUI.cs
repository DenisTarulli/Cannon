using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private TextMeshProUGUI forceText;
    [SerializeField] private TextMeshProUGUI xAngleText;
    [SerializeField] private TextMeshProUGUI yAngleText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI impactForceText;

    private ProjectileThrow projectileThrow;
    private CannonAim cannonAim;

    private void Start()
    {
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        cannonAim = FindObjectOfType<CannonAim>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleUI();

        SetForceUI();
        SetAnglesUI();
        SetDistanceUI();
    }

    private void ToggleUI()
    {
        if (statsPanel.activeSelf)
            statsPanel.SetActive(false);
        else
            statsPanel.SetActive(true);
    }

    private void SetForceUI()
    {
        forceText.text = $"Force: {string.Format("{0:0.##}", projectileThrow.Force)}";
    }

    private void SetAnglesUI()
    {
        float xAngleToDisplay = cannonAim.transform.eulerAngles.y;

        if (xAngleToDisplay > 180f)        
            xAngleToDisplay -= 360f;
        
        xAngleText.text = $"xAngle: {string.Format("{0:0.##}", xAngleToDisplay)}°";

        float yAngleToDisplay = cannonAim.YAngle;

        if (yAngleToDisplay <= 360f && yAngleToDisplay > 305f)
            yAngleToDisplay = Mathf.Abs(yAngleToDisplay - 360f);
        else
            yAngleToDisplay = -yAngleToDisplay;

        yAngleText.text = $"yAngle: {string.Format("{0:0.##}", yAngleToDisplay)}°";
    }

    private void SetDistanceUI()
    {
        distanceText.text = $"Hit distance: {string.Format("{0:0.##}", projectileThrow.DistanceToHit)}";
    }

    public void SetImpactForceUI(float force)
    {
        impactForceText.text = $"Impact force: {string.Format("{0:0.##}", force)}";
    }
}
