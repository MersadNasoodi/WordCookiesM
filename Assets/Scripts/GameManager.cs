using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private WordShowUI wordDisplayUI;
    [SerializeField] private List<WordStage> stages;

    private int currentStageIndex = 0;
    private WordStage CurrentStage => stages[currentStageIndex];

    private HashSet<string> _foundWords = new();

    public GameObject afarin;
    public GameObject repeated;
    public GameObject completed;

    private void Start()
    {
        wordDisplayUI.SetupWords(CurrentStage.validWords);
    }


    
    public void OnWordSubmitted(string word)
    {
        if (_foundWords.Contains(word))
        {
            StartCoroutine(Reapeted());
            Debug.Log("Word is found!");

            return;
        }

        if (CurrentStage.validWords.Contains(word))
        {
            _foundWords.Add(word);

            wordDisplayUI.RevealWord(word);

            StartCoroutine(Afarin());
            Debug.Log("Correct: " + word);

          
            if (_foundWords.Count >= CurrentStage.validWords.Count)
            {
                Debug.Log("The Level is Done...");
                Invoke(nameof(NextStage), 1f);
            }


        }
        else
        {
            Debug.Log("WrongWord: " + word);
        }
    }

    void NextStage()
    {

        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            Debug.Log("All The Levels Are Done");
            StartCoroutine(Completed());
            return;
        }




        _foundWords.Clear();
        Debug.Log("New Level: " + CurrentStage.letters);
    }

    IEnumerator Afarin()
    {
        afarin.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        afarin.SetActive(false);
    }
    IEnumerator Reapeted()
    {
        repeated.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        repeated.SetActive(false);
    }

    IEnumerator Completed()
    {
        completed.SetActive(true);
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(0);


    }



}
