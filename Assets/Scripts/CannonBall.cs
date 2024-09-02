using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private StatsUI statsUI;
    private bool collisioned;

    private void Start()
    {
        statsUI = FindObjectOfType<StatsUI>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Target") || collisioned) return;

        collisioned = true;
        Vector3 collisionForce = collision.impulse / Time.fixedDeltaTime;

        float xForce = Mathf.Abs(collisionForce.x);
        float yForce = Mathf.Abs(collisionForce.y);
        float zForce = Mathf.Abs(collisionForce.z);

        float forceVectorMod = Mathf.Sqrt(Mathf.Pow(xForce, 2) +  Mathf.Pow(yForce, 2) + Mathf.Pow(zForce, 2));

        statsUI.SetImpactForceUI(forceVectorMod);

        this.enabled = false;
    }
}
