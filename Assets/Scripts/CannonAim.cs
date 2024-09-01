using UnityEngine;
using UnityEngine.UI;

public class CannonAim : MonoBehaviour
{
    [SerializeField] private Transform objectToTilt;
    [SerializeField] private Slider angleSlider;
    [SerializeField] private float minTilt;
    [SerializeField] private float maxTilt;
    [SerializeField] private float rotationSpeed;
    private bool rotatingLeft;
    private bool rotatingRight;

    [SerializeField] private float maxRandomRotation;
    private ProjectileThrow projectileThrow;

    public float YAngle { get; private set; }

    private void Start()
    {
        projectileThrow = GetComponent<ProjectileThrow>();
        SetSliderValues();
    }

    private void Update()
    {
        if (rotatingLeft)
            SpinBase(-1);
        else if (rotatingRight)
            SpinBase(1);
    }

    private void SpinBase(int direction)
    {
        transform.Rotate(Vector3.forward, direction * rotationSpeed * Time.deltaTime);
    }

    public void TriggerRotation(string dir)
    {
        if (dir == "Left")
            rotatingLeft = true;
        else
            rotatingRight = true;
    }

    public void StopRotation(string dir)
    {
        if (dir == "Left")
            rotatingLeft = false;
        else
            rotatingRight = false;
    }

    public void TiltCannon()
    {
        objectToTilt.localEulerAngles = new Vector3(-angleSlider.value, 0f, 0f);
        YAngle = objectToTilt.eulerAngles.x;
    }

    private void SetSliderValues()
    {
        angleSlider.minValue = minTilt;
        angleSlider.maxValue = maxTilt;

        angleSlider.value = minTilt + ((maxTilt - minTilt) / 2f);
    }

    public void SetRandomValues()
    {
        transform.eulerAngles = new(transform.eulerAngles.x, Random.Range(-maxRandomRotation, maxRandomRotation), transform.eulerAngles.z);

        float newTilt = Random.Range(minTilt, maxTilt);
        objectToTilt.localEulerAngles = new Vector3(-newTilt, 0f, 0f);
        angleSlider.value = newTilt;

        projectileThrow.RandomForce();
    }
}
