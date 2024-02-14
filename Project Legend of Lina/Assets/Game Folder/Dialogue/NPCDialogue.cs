using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public Transform emote;
    public float dialogueRange;
    public LayerMask playerLayer;
    public DialogueSettings dialogue;
    bool playerHit;
    private List<string> sentences = new List<string>();
    private List<string> actorName = new List<string>();
    private List<Sprite> actorSprite = new List<Sprite>();

    // Start is called before the first frame update
    private void Start()
    {
        GetNPCInfo();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            Debug.Log("chamou o NPCdialogue");
            DialogueControl.instance.Speech(sentences.ToArray(), actorName.ToArray(), actorSprite.ToArray());
        }
    }
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetNPCInfo()
    {
        for (int i=0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueControl.instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;

                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;

                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }

            actorName.Add(dialogue.dialogues[i].actorName);
            actorSprite.Add(dialogue.dialogues[i].profileImage);
        }
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if (hit != null)
        {
            playerHit = true;
            emote.GetComponent<Animator>().Play("Emote1", 0);
            if(DialogueControl.instance.dialogueObj == false)
            {
                Debug.Log("dialogueObj está desativado");
            }
            //COLOCAR ANIMAÇÃO DE EMOTE
        }
        else
        {
            playerHit = false;
            DialogueControl.instance.dialogueObj.SetActive(false);
            emote.GetComponent<Animator>().Play("Emote2", 0);
            // COLOCAR ANIMAÇÃO DE EMOTE
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

}
