using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    #region Members
    [Header("Physics")]
    [SerializeField, Range(40f, 50f)] private float maxForce;
    [SerializeField, Range(5f, 15f)] private float minForce;
    [SerializeField, Range(15f, 40f)] private float startingForce;
    private float force;

    [Header("Shooting")]
    [SerializeField] private float fireRate;
    private float nextTimeToFire;

    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Rigidbody objectToThrow;
    private TrajectoryPredictor trajectoryPredictor;
    #endregion

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    private void Start()
    {
        force = startingForce;
        nextTimeToFire = Time.time;
    }

    private void Update()
    {
        Predict();
        AdjustForce();

        if (Input.GetMouseButton(0) && Time.timeScale != 0f)
        {
            ThrowObject();
        }
    }

    private void AdjustForce()
    {
        force = Mathf.Clamp(force += Input.mouseScrollDelta.y, minForce, maxForce);
    }

    private void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new();
        Rigidbody rb = objectToThrow.GetComponent<Rigidbody>();

        properties.direction = spawnPoint.forward;
        properties.initialPosition = spawnPoint.position;
        properties.initialSpeed = force;
        properties.mass = rb.mass;
        properties.drag = rb.drag;

        return properties;
    }

    private void ThrowObject()
    {
        if (nextTimeToFire >= Time.time) return;

        nextTimeToFire = Time.time + 1f / fireRate;

        Rigidbody thrownObject = Instantiate(objectToThrow, spawnPoint.position, spawnPoint.rotation);
        thrownObject.AddForce(spawnPoint.forward * force, ForceMode.Impulse);
    }
}