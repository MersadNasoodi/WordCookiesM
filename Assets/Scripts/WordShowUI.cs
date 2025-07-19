using UnityEngine;
using TMPro;
using System.Collections.Generic;
using RTLTMPro;

public class WordShowUI : MonoBehaviour
{
    [SerializeField] private GameObject wordSlotPrefab;
    [SerializeField] private Transform wordSlotParent;

    private Dictionary<string, RTLTextMeshPro> wordToSlot = new();

   
    public void SetupWords(List<string> words)
    {
       
        foreach (Transform child in wordSlotParent)
            Destroy(child.gameObject);

        wordToSlot.Clear();

        foreach (var word in words)
        {
            var slot = Instantiate(wordSlotPrefab, wordSlotParent);
            var text = slot.GetComponentInChildren<RTLTextMeshPro>();
            text.text = new string('ء', word.Length); 
            wordToSlot[word] = text;
        }
    }

    public void RevealWord(string word)
    {
        string key = word;
        if (wordToSlot.TryGetValue(key, out var text))
        {
            text.text = word;
        }
    }
}
