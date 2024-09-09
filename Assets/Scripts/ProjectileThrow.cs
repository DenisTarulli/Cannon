using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    #region Members
    [Header("Physics")]
    [SerializeField, Range(40f, 50f)] private float maxForce;
    [SerializeField, Range(5f, 15f)] private float minForce;
    private float force;

    [Header("Shooting")]
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileLifeSpan;
    private float nextTimeToFire;

    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform hitMarker;
    [SerializeField] private Rigidbody objectToThrow;
    [SerializeField] private Slider forceSlider;
    private TrajectoryPredictor trajectoryPredictor;

    public float Force { get => force; set => force = value; }
    public float DistanceToHit { get; private set; }
    #endregion

    void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    private void Start()
    {
        SetSliderValues();
        AdjustForce();
        nextTimeToFire = Time.time;
    }

    private void Update()
    {
        Predict();
        AdjustForce();
        MeasureDistanceToHit();
    }

    public void AdjustForce()
    {
        force = forceSlider.value;
    }
    
    private void SetSliderValues()
    {
        forceSlider.minValue = minForce;
        forceSlider.maxValue = maxForce;

        forceSlider.value = minForce + ((maxForce - minForce) / 2f);
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

    public void ThrowObject()
    {
        if (nextTimeToFire >= Time.time) return;

        nextTimeToFire = Time.time + 1f / fireRate;

        Rigidbody thrownObject = Instantiate(objectToThrow, spawnPoint.position, spawnPoint.rotation);
        thrownObject.AddForce(spawnPoint.forward * force, ForceMode.Impulse);

        Destroy(thrownObject.gameObject, projectileLifeSpan);
    }

    private void MeasureDistanceToHit()
    {
        DistanceToHit = Vector3.Distance(transform.position, hitMarker.position);
    }

    public void RandomForce()
    {
        float newForce = Random.Range(minForce, maxForce);
        force = newForce;
        forceSlider.value = force;
    }
}
