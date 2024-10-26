using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject generalPanel;
    [SerializeField] private GameObject shotListPanel;
    [SerializeField] private TextMeshProUGUI forceText;
    [SerializeField] private TextMeshProUGUI xAngleText;
    [SerializeField] private TextMeshProUGUI yAngleText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI impactForceText;

    [SerializeField] private LastShotInfo shotInfo;
    private float lastXAngle;
    private float lastYAngle;

    private ProjectileThrow projectileThrow;
    private CannonAim cannonAim;
    private bool listIsOpen;

    private void Start()
    {
        projectileThrow = FindObjectOfType<ProjectileThrow>();
        cannonAim = FindObjectOfType<CannonAim>();

        shotInfo.ResetValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            ToggleUI();

        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleShotList();

        SetForceUI();
        SetAnglesUI();
        SetDistanceUI();
    }

    private void ToggleUI()
    {
        if (listIsOpen) return;

        if (statsPanel.activeSelf)
            statsPanel.SetActive(false);
        else
            statsPanel.SetActive(true);
    }

    private void ToggleShotList()
    {
        if (!shotListPanel.activeSelf)
        {
            generalPanel.SetActive(false);
            shotListPanel.SetActive(true);
            listIsOpen = true;
        }
        else
        {
            generalPanel.SetActive(true);
            shotListPanel.SetActive(false);
            listIsOpen = false;
        }
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
        
        lastXAngle = xAngleToDisplay;
        xAngleText.text = $"xAngle: {string.Format("{0:0.##}", xAngleToDisplay)}°";

        float yAngleToDisplay = cannonAim.YAngle;

        if (yAngleToDisplay <= 360f && yAngleToDisplay > 305f)
            yAngleToDisplay = Mathf.Abs(yAngleToDisplay - 360f);
        else
            yAngleToDisplay = -yAngleToDisplay;

        lastYAngle = yAngleToDisplay;
        yAngleText.text = $"yAngle: {string.Format("{0:0.##}", yAngleToDisplay)}°";
    }

    private void SetDistanceUI()
    {
        distanceText.text = $"Hit distance: {string.Format("{0:0.##}", projectileThrow.DistanceToHit)}";
    }

    public void SetImpactForceUI(float force)
    {
        impactForceText.text = $"Impact force: {string.Format("{0:0.##}", force)}";

        SetLastShotInfo(force);
    }

    private void SetLastShotInfo(float lastImpactForce)
    {
        shotInfo.Force = projectileThrow.Force;
        shotInfo.X_Angle = lastXAngle;
        shotInfo.Y_Angle = lastYAngle;
        shotInfo.ImpactForce = lastImpactForce;
    }
}
