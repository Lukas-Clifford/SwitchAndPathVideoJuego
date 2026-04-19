using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [Header("Referencias")]
    public Transform BaseButton;
    public Transform trappdoorActive;

    [Header("Ajustes botón")]
    public Vector3 pressedOffset = new Vector3(0, -0.2f, 0);

    [Header("Ajustes trampilla")]
    public Vector3 rotationOffset = new Vector3(90f, 0f, 0f); // eje de rotación

    private Vector3 initialPosition;

    private Quaternion initialRotation;
    private Quaternion openRotation;

    private int objectsOnTop = 0;

    void Start()
    {
        // Guardar posición inicial del botón
        initialPosition = BaseButton.localPosition;

        // Guardar rotación inicial de la trampilla (LOCAL)
        initialRotation = trappdoorActive.localRotation;

        // Calcular rotación abierta RELATIVA
        openRotation = initialRotation * Quaternion.Euler(rotationOffset);
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
            // Botón baja
            BaseButton.localPosition = initialPosition + pressedOffset;

            // Trampilla abre (rotación local)
            trappdoorActive.localRotation = openRotation;
        }
        else
        {
            // Botón sube
            BaseButton.localPosition = initialPosition;

            // Trampilla vuelve a estado inicial
            trappdoorActive.localRotation = initialRotation;
        }
    }
}