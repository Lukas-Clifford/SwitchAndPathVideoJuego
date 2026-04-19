using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public Transform BaseButton;
    public Vector3 pressedOffset = new Vector3(0, -0.2f, 0);

    private Vector3 initialPosition;
    private int objectsOnTop = 0;

    void Start()
    {
        initialPosition = BaseButton.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            objectsOnTop++;
            UpdateButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            objectsOnTop--;
            UpdateButton();
        }
    }

    void UpdateButton()
    {
        if (objectsOnTop > 0)
        {
            BaseButton.localPosition = initialPosition + pressedOffset;
            // Aquí puedes activar puerta
        }
        else
        {
            BaseButton.localPosition = initialPosition;
            // Aquí puedes desactivar puerta
        }
    }
}