using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private StatsUI statsUI;

    private void Start()
    {
        statsUI = FindObjectOfType<StatsUI>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Target")) return;

        Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;

        float xForce = Mathf.Abs(collisionForce.x);
        float yForce = Mathf.Abs(collisionForce.y);
        float zForce = Mathf.Abs(collisionForce.z);

        if (xForce > yForce && xForce > yForce)
            statsUI.SetImpactForceUI(xForce);
        else if (yForce > zForce)
            statsUI.SetImpactForceUI(yForce);
        else
            statsUI.SetImpactForceUI(zForce);

        this.enabled = false;
    }
}
