using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
  public TextMeshProUGUI characterNameText;
  public TextMeshProUGUI dialogueText;
  public Image background;
  public Button nextButton;

  private Queue<string> sentences;

  private void Start()
  {
    // Disable on Start
    characterNameText.enabled = false;
    dialogueText.enabled = false;
    background.enabled = false;
    nextButton.enabled = false;
    nextButton.GetComponentInChildren<Image>().enabled = false;
    nextButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    
    sentences = new Queue<string>();
  }

  public void StartDialogue( Dialogue dialogue )
  {
    // Enable GUI
    characterNameText.enabled = true;
    dialogueText.enabled = true;
    background.enabled = true;
    nextButton.enabled = true;
    nextButton.GetComponentInChildren<Image>().enabled = true;
    nextButton.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

    characterNameText.text = dialogue.characterName;

    sentences.Clear();

    foreach( string sentence in dialogue.sentences )
    {
      sentences.Enqueue( sentence );
    }
      
    DisplayNextSentence();
  }

  public void DisplayNextSentence()
  {
    if( sentences.Count == 0 )
    {
      EndDialogue();
      return;
    }

    string sentence = sentences.Dequeue();

    StopAllCoroutines();
    StartCoroutine( Typewriter( sentence ) );
  }

  public void EndDialogue()
  {
    // Disable on Start
    characterNameText.enabled = false;
    dialogueText.enabled = false;
    background.enabled = false;
    nextButton.enabled = false;
    nextButton.GetComponentInChildren<Image>().enabled = false;
    nextButton.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
  }

  private IEnumerator Typewriter( string sentence )
  {
    dialogueText.text = "";
    foreach( char letter in sentence.ToCharArray() )
    {
      dialogueText.text += letter;
      // Single frame delay
      yield return null;
    }
  }
}
