using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Singleton<DialogueTrigger>
{
  public Dialogue dialogue;

  public void TriggerDialogue()
  {
    DialogueManager.instance.StartDialogue( dialogue );
  }

  private void OnTriggerEnter2D( Collider2D other )
  {
    if( other.gameObject.CompareTag( "Player" ) )
    {
      TriggerDialogue();
    }
  }

  private void OnTriggerExit2D( Collider2D other )
  {
    if( other.gameObject.CompareTag( "Player" ) )
    {
      DialogueManager.instance.EndDialogue();
    }
  }
}
