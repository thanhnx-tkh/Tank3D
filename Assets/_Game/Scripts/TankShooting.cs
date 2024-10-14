using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Để sử dụng EventTrigger

public class TankShooting : MonoBehaviour
{
    [SerializeField] private Button fireButton;
    public Rigidbody shell_Prefab;
    public Transform fireTransform;
    public Slider aimSlider;
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 0.75f;
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool isFired;

    private void OnEnable()
    {
        isFired = true;
        currentLaunchForce = minLaunchForce;
        aimSlider.value = minLaunchForce;
    }

    private void Start()
    {
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;

        // Gán sự kiện sử dụng EventTrigger
        EventTrigger trigger = fireButton.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
        pointerDownEntry.eventID = EventTriggerType.PointerDown;
        pointerDownEntry.callback.AddListener((data) => { StartCharging(); });
        trigger.triggers.Add(pointerDownEntry);

        EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
        pointerUpEntry.eventID = EventTriggerType.PointerUp;
        pointerUpEntry.callback.AddListener((data) => { Fire(); });
        trigger.triggers.Add(pointerUpEntry);
    }

    public void StartCharging()
    {
        isFired = false;
        currentLaunchForce = minLaunchForce;
    }

    private void Update()
    {
        aimSlider.value = minLaunchForce;

        if (!isFired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;
            currentLaunchForce = Mathf.Clamp(currentLaunchForce, minLaunchForce, maxLaunchForce);
            aimSlider.value = currentLaunchForce;
        }
    }

    private void Fire()
    {
        if (isFired) return;

        isFired = true;

        Rigidbody shellInstance = Instantiate(shell_Prefab, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = currentLaunchForce * fireTransform.forward;

        currentLaunchForce = minLaunchForce;
    }
}
