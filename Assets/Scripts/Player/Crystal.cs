using nl.ma.utopia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public CanvasGroup crystalVision;
    private bool crystalActivated;
    private GameObject highlightedObject;
    private float hoverTime = 0f;
    private float interactionTime = 3f;

    public bool IsActive()
    {
        return crystalActivated;
    }

    void Start()
    {
        crystalVision.alpha = 0f;
    }

    void Update()
    {
        ActivateCrystal();
        if (crystalActivated && !GlobalReferences.Instance.BCI)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var objectToInteract = hit.collider.GetComponent<CrystalInteractable>();
                if (objectToInteract != null)
                {
                    HighlightObject(hit.collider.gameObject);
                    StartInteractionCountdown(objectToInteract);
                }
            }
            else
            {
                RemoveHighlight();
            }
        }
    }

    private void ActivateCrystal()
    {
        if (Input.GetMouseButton(0) && InventoryManager.Instance.crystal)
        {
            crystalVision.alpha = 1f;
            crystalActivated = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            crystalVision.alpha = 0f;
            crystalActivated = false;
        }
    }

    private void HighlightObject(GameObject obj)
    {
        if (highlightedObject != obj)
        {
            RemoveHighlight();
            highlightedObject = obj;

            var renderer = highlightedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.yellow;
            }
        }
    }

    private void RemoveHighlight()
    {
        if (highlightedObject != null)
        {
            var renderer = highlightedObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = Color.white;
            }

            highlightedObject = null;
            hoverTime = 0f;
        }
    }

    void StartInteractionCountdown(CrystalInteractable interactable)
    {
        if (highlightedObject != null)
        {
            hoverTime += Time.deltaTime;
            if (hoverTime >= interactionTime)
            {
                interactable.Interact();
                RemoveHighlight();
            }
        }
    }
}
