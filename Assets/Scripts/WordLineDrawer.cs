using UnityEngine;
using UnityEngine.UI.Extensions;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(UILineRenderer))]
public class WordLineDrawer : MonoBehaviour
{
    private UILineRenderer _line;
    private readonly List<Vector2> _points = new();
    private readonly List<LetterNode> _selectedNodes = new();
    public GameManager gameManager;


    public AudioClip selectLetterSound;
    private AudioSource audioSource;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();


        _line = GetComponent<UILineRenderer>();

     
        LetterNode.OnLetterDown += StartLine;
        LetterNode.OnLetterEnter += AddLetter;
        LetterNode.OnLetterUp += FinishLine;
    }

    void OnDestroy()
    {
        LetterNode.OnLetterDown -= StartLine;
        LetterNode.OnLetterEnter -= AddLetter;
        LetterNode.OnLetterUp -= FinishLine;
    }

    void StartLine(LetterNode node)
    {

        _selectedNodes.Clear();
        _points.Clear();

        _selectedNodes.Add(node);
        AddPoint(node);
    }

    void AddLetter(LetterNode node)
    {
        if (_points.Contains(node.RectT.localPosition))
            return;

        if (_points.Count == 0 || _points[^1] != LocalPos(node))
        {
            _selectedNodes.Add(node);
            AddPoint(node);
            PlayLetterSound();
        }

    }

    void FinishLine()
    {
        //string word = string.Concat(_selectedNodes.Select(n => n.Letter.ToString().ToUpper()));

        string word = string.Join("", _selectedNodes.Select(n => n.Letter));

        Debug.Log("CREATED WORD: " + word);

        gameManager.OnWordSubmitted(word);

     
        _points.Clear();
        _selectedNodes.Clear();
        _line.Points = System.Array.Empty<Vector2>();
    }

   

    void AddPoint(LetterNode node)
    {
        _points.Add(LocalPos(node));
        _line.Points = _points.ToArray();
    }

    Vector2 LocalPos(LetterNode node) =>
        node.RectT.localPosition;


    private void PlayLetterSound()
    {
        if (selectLetterSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(selectLetterSound);
        }
    }

}
