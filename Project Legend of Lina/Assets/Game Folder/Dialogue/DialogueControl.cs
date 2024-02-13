using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{

    [Header("Components")]
    public GameObject dialogueObj; //JANELA DO DIALOGO
    public Image profileSprite; // SPRITE DO PERFIL
    public Text speechText; // TEXTO DA FALA
    public Text actorNameText; // NOME DO NPC


    [Header("Settings")]
    public float typingSpeed; //VELOCIDADE DA FALA

    //VARIÁVEIS DE CONTROLE
    private bool isShowing; //SE A JANAELA ESTÁ VISÍVEL
    private int index; // INDEX DAS SENTENÇAS
    private string[] sentences;

    public static DialogueControl instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
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
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //QUANDO TERMINA OS TEXTOS
            {
                speechText.text = "";
                index = 0;
            }
        }
    }

    //CHAMA A FALA
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
