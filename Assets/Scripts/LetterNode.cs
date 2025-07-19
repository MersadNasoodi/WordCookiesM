using UnityEngine;
using UnityEngine.EventSystems;

public class LetterNode : MonoBehaviour,
                          IPointerDownHandler,
                          IPointerEnterHandler,
                          IPointerUpHandler
{
    public char Letter;

    public static System.Action<LetterNode> OnLetterDown;  
    public static System.Action<LetterNode> OnLetterEnter; 
    public static System.Action OnLetterUp;                

    public RectTransform RectT { get; private set; }

    void Awake() => RectT = GetComponent<RectTransform>();

    public void OnPointerDown(PointerEventData eventData) =>
        OnLetterDown?.Invoke(this);

    public void OnPointerEnter(PointerEventData eventData) =>
        OnLetterEnter?.Invoke(this);

    public void OnPointerUp(PointerEventData eventData) =>
        OnLetterUp?.Invoke();
}
