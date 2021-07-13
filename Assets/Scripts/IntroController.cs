using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
  public GameObject nextText;
  public string[] speaks;
  public TextMeshProUGUI textIntro;
  public float timeBetweenLetters;

  private int idSpeaks;
  private bool isFinishLetters;


  void Start()
  {
    textIntro.text = "";
    nextText.SetActive(false);
    StartCoroutine("StepLetters");
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && isFinishLetters)
    {
      NextFala();
    }

    if (Input.GetKeyDown(KeyCode.Return))
    {
      ClosePanel();
    }
  }

  public void NextFala()
  {
    if (idSpeaks == speaks.Length - 1)
    {
      ClosePanel();
      idSpeaks = 0;
      return;
    }

    nextText.SetActive(false);
    textIntro.text = "";
    idSpeaks += 1;
    StartCoroutine("StepLetters");
  }

  IEnumerator StepLetters()
  {
    isFinishLetters = false;
    char[] letters = speaks[idSpeaks].ToCharArray();
    int checkLetters = 0;

    for (int idLetter = 0; idLetter < letters.Length; idLetter++)
    {
      textIntro.text += letters[idLetter].ToString();
      yield return new WaitForSeconds(timeBetweenLetters);
      checkLetters = idLetter;
    }

    if (checkLetters == letters.Length - 1)
    {
      StopCoroutine("StepLetters");
      checkLetters = 0;
      nextText.SetActive(true);
      isFinishLetters = true;
    }
  }

  public void ClosePanel()
  {
    StopCoroutine("StepLetters");
   
    isFinishLetters = false;
    idSpeaks = 0;
    textIntro.text = "";

    nextText.SetActive(false);

    return;
  }
}
