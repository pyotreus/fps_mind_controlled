using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [TextArea] public string npcDialogue;
    [TextArea] public string[] dialogueMessages;

    public GameObject textBubblePrefab;
    private GameObject textBubbleInstance;
    private TextMeshProUGUI textComponent;
    public string[] messages;
    //TODO
    public AudioClip[] voiceClips;
    public float displayDuration = 3f;

    private AudioSource audioSource;
    private bool isPlayerNearby = false;

    public void Interact()
    {
        if (dialogueMessages != null && dialogueMessages.Length > 0)
        {
            DialogueManager.Instance.ShowDialogue(dialogueMessages);
        }
        else if (!string.IsNullOrEmpty(npcDialogue))
        {
            DialogueManager.Instance.ShowDialogue(npcDialogue);
        }
        else
        {
            Debug.LogWarning("No dialogue set for this NPC.");
        }
    }

    void Start()
    {
        if (textBubblePrefab == null)
        {
            return;
        }
        // Instantiate the text bubble and set it up
        textBubbleInstance = Instantiate(textBubblePrefab, transform);
        textBubbleInstance.transform.localPosition = new Vector3(0, 2, 0); // Position above NPC
        textComponent = textBubbleInstance.GetComponentInChildren<TextMeshProUGUI>();
        textBubbleInstance.SetActive(false); // Hide initially

        // Add an audio source
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerNearby)
        {
            isPlayerNearby = true;
            ShowTextBubble();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPlayerNearby)
        {
            isPlayerNearby = false;
            HideTextBubble();
        }
    }

    void ShowTextBubble()
    {
        if (messages.Length > 0)
        {
            // Randomly pick a message
            int randomIndex = Random.Range(0, messages.Length);
            textComponent.text = messages[randomIndex];

            // Play corresponding audio if available
            if (voiceClips != null && randomIndex < voiceClips.Length && voiceClips[randomIndex] != null)
            {
                audioSource.clip = voiceClips[randomIndex];
                audioSource.Play();
            }

            textBubbleInstance.SetActive(true);
        }

        // Hide the bubble after a duration
        Invoke(nameof(HideTextBubble), displayDuration);
    }

    void HideTextBubble()
    {
        textBubbleInstance.SetActive(false);
    }

}
