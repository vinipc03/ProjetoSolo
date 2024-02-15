using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }
    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //JANELA DO DIALOGO
    public Image profileSprite; // SPRITE DO PERFIL
    public Text speechText; // TEXTO DA FALA
    public Text actorNameText; // NOME DO NPC

    [Header("Settings")]
    public float typingSpeed; //VELOCIDADE DA FALA

    //VARIÁVEIS DE CONTROLE
    [SerializeField] private bool isShowing; //SE A JANAELA ESTÁ VISÍVEL
    private int index; // INDEX DAS SENTENÇAS
    private string[] sentences; // FALAS
    private string[] currentActorName;
    private Sprite[] actorSprite;
    private PlayerController player;
    public static DialogueControl instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    //PULA PARA PRÓXIMA FALA
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //QUANDO TERMINA OS TEXTOS
            {
                Debug.Log("Terminou a fala");
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
                player.onAttack = false;
                player.isTalking = false;
            }
        }
    }
    //CHAMA A FALA
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.onAttack = true;
            player.isTalking = true;
        }
    }
}
