using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Parametre")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset IntroInkJSON;

    //public GameObject dialogueIntro;


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private Text displayNameText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private Text[] choicesText;


    //[Header("Audio")]
    //public AudioSource Porte;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    private bool canContinueToNextLine = false;

    public bool canTalkAgain = true;


    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    [SerializeField] private Animator layoutAnimator;

    [Header("Tag")]
    private const string speakerTag = "speaker";
    private const string portraitTag = "portrait";
    private const string layoutTag = "layout";

    private const string GiveAppareilPhotoTag = "GiveAppareilPhoto";
    private const string EndGameTag = "EndGame";


  
    [Header("Inventaire")]
    public GameObject AppareilPhotoPrefab;
    public Transform spawnAppareilPhoto;



    [Header("Ref")]
    public PlayerInput playerControls;
    public GameObject player;
    public PlayerController playerController;
    //public DialogueTrigger dialogueTriggerRef;
    //public PauseManager pauseManagerRef;


    public GameObject pnl_fin;
    //[Header("SwitchFakeItem")]
    //public GameObject aActiver;
    //public GameObject aDesactiver;
    public bool triggered { get; }

    private void Awake()
    {
        playerControls = player.GetComponent<PlayerInput>();
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        canTalkAgain = true;



        layoutAnimator = dialoguePanel.GetComponent<Animator>();
        //dialogueIntro.SetActive(true);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        Debug.Log("panleDesac");


        // get all of the choices text 
        choicesText = new Text[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<Text>();
            index++;
        }
        //DialogueManager.GetInstance().EnterDialogueMode(IntroInkJSON);
    }

    private void Update()
    {
        //DontDestroyOnLoad(this.gameObject);
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && playerControls.actions["Interagir"].WasPerformedThisFrame() && canTalkAgain == true)//Input.GetButtonDown("InteractDialogue"))
        {
            ContinueStory();
        }

        if (dialogueIsPlaying == true)
        {
            Time.timeScale = 0f;
        }

        
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        if (canTalkAgain == true)
        {
            //Debug.Log("CanTalk");
            //PlayerController.canMove = false;
            //PlayerController.animator.SetBool("IsWalking", false);
            //PlayerController.footStep.Stop();
            //playerController.rb.constraints = RigidbodyConstraints2D.FreezeAll;


            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);


            ContinueStory();
        }
    }

    //public IEnumerator ExitDialogueMode()
    //{
    //    yield return new WaitForSeconds(0.2f);

    //    //PlayerController.canMove = true;
    //    //playerController.rb.constraints = RigidbodyConstraints2D.None;
    //    //playerController.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

    //    dialogueIsPlaying = false;
    //    dialoguePanel.SetActive(false);
    //    dialogueText.text = "";

    //    canTalkAgain = false;
    //    yield return new WaitForSeconds(0.5f);
    //    Debug.Log("Can't Talk");
    //    canTalkAgain = true;
    //}

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            HandleTags(currentStory.currentTags);
            //// display choices, if any, for this dialogue line
            Debug.Log("continue story");

        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }
    private IEnumerator ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        canTalkAgain = false;
        yield return new WaitForSeconds(1f);
        canTalkAgain = true;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            if (splitTag.Length != 2)
            {
                Debug.Log("Tag could not be appropriately parsed:" + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                //Portrait//
                case speakerTag:
                    displayNameText.text = tagValue;
                    Debug.Log("speaker=" + tagValue);
                    break;

                case portraitTag:
                    Debug.Log("portrait=" + tagValue);
                    break;

                case layoutTag:
                    layoutAnimator.Play(tagValue);
                    Debug.Log("layout=" + tagValue);
                    break;

                case GiveAppareilPhotoTag:
                    //Instantiate(AppareilPhotoPrefab, Vector3.zero, Quaternion.identity, spawnAppareilPhoto.transform);
                    Instantiate(AppareilPhotoPrefab, spawnAppareilPhoto.position, spawnAppareilPhoto.rotation);
                    break;

                case EndGameTag:
                    pnl_fin.SetActive(true);
                    Time.timeScale = 0f;
                    break;


                default:
                    Debug.Log("Tag came in but is not currently being handled" + tag);
                    break;
            }
        }
        



    }

    IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";

        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        for (int i = 0; i < line.Length; i++)
        {
            dialogueText.text += line[i];

            // Attendre un court instant après l'affichage de chaque lettre
            yield return new WaitForSeconds(typingSpeed);

            // Vérifier si l'utilisateur veut passer au texte suivant
            if (playerControls.actions["Interagir"].WasPerformedThisFrame())
            {
                // Afficher le reste du texte immédiatement
                dialogueText.text = line;
                break;
            }
        }

        canContinueToNextLine = true;
        DisplayChoices();
        continueIcon.SetActive(true);
    }

    private void HideChoices()
    {
        foreach (GameObject choicebutton in choices)
        {
            choicebutton.SetActive(false);
        }
    }
    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made

            Debug.Log("MakeChoice");
            ContinueStory();
        }

    }    
}
