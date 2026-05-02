using UnityEngine;
using System.Collections;

public class PressureButton : MonoBehaviour
{
    [Header("Referencias")]
    public Transform BaseButton;

    [Header("Trampillas")]
    public Transform[] trapdoorsToOpen;
    public Transform[] trapdoorsToClose;

    [Header("Ajustes botón")]
    public Vector3 pressedOffset = new Vector3(0, -0.2f, 0);

    [Header("Ajustes trampilla")]
    public Vector3 rotationOffset = new Vector3(90f, 0f, 0f);
    public float animationSpeed = 2f;

    private Vector3 initialPosition;
    private int objectsOnTop = 0;

    private Quaternion[] openRotations;
    private Quaternion[] closedRotations;
    private Quaternion[] openRotations2;
    private Quaternion[] closedRotations2;

    // Guardar referencia de cada coroutine
    private Coroutine[] coroutinesOpen;
    private Coroutine[] coroutinesClose;

    void Start()
    {
        initialPosition = BaseButton.localPosition;

        openRotations   = new Quaternion[trapdoorsToOpen.Length];
        closedRotations = new Quaternion[trapdoorsToOpen.Length];
        coroutinesOpen  = new Coroutine[trapdoorsToOpen.Length];

        for (int i = 0; i < trapdoorsToOpen.Length; i++)
        {
            if (trapdoorsToOpen[i] == null) continue;
            closedRotations[i] = trapdoorsToOpen[i].localRotation;
            openRotations[i]   = closedRotations[i] * Quaternion.Euler(rotationOffset);
        }

        openRotations2   = new Quaternion[trapdoorsToClose.Length];
        closedRotations2 = new Quaternion[trapdoorsToClose.Length];
        coroutinesClose  = new Coroutine[trapdoorsToClose.Length];

        for (int i = 0; i < trapdoorsToClose.Length; i++)
        {
            if (trapdoorsToClose[i] == null) continue;
            openRotations2[i]   = trapdoorsToClose[i].localRotation;
            closedRotations2[i] = openRotations2[i] * Quaternion.Euler(-rotationOffset);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        objectsOnTop = Mathf.Max(0, objectsOnTop + 1);
        UpdateButton();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody == null) return;
        objectsOnTop = Mathf.Max(0, objectsOnTop - 1);
        UpdateButton();
    }

    void UpdateButton()
    {
        bool pressed = objectsOnTop > 0;

        BaseButton.localPosition = pressed
            ? initialPosition + pressedOffset
            : initialPosition;

        for (int i = 0; i < trapdoorsToOpen.Length; i++)
        {
            if (trapdoorsToOpen[i] == null) continue;
            Quaternion target = pressed ? openRotations[i] : closedRotations[i];

            // Parar coroutine anterior antes de lanzar la nueva
            if (coroutinesOpen[i] != null)
                StopCoroutine(coroutinesOpen[i]);
            coroutinesOpen[i] = StartCoroutine(AnimateTrapdoor(trapdoorsToOpen[i], target));
        }

        for (int i = 0; i < trapdoorsToClose.Length; i++)
        {
            if (trapdoorsToClose[i] == null) continue;
            Quaternion target = pressed ? closedRotations2[i] : openRotations2[i];

            if (coroutinesClose[i] != null)
                StopCoroutine(coroutinesClose[i]);
            coroutinesClose[i] = StartCoroutine(AnimateTrapdoor(trapdoorsToClose[i], target));
        }
    }

    IEnumerator AnimateTrapdoor(Transform trapdoor, Quaternion targetRotation)
    {
        Quaternion startRotation = trapdoor.localRotation;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * animationSpeed;
            trapdoor.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }

        trapdoor.localRotation = targetRotation;
    }
}