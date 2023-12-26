using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueAnswerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public DialogueInteractionUI dialogueInteractionUI;
    public int answerIndex;

    public void OnPointerClick(PointerEventData eventData) {
        dialogueInteractionUI.OnAnswerSelected(answerIndex);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        dialogueInteractionUI.OnAnswerHovered(answerIndex);
    }

    public void OnPointerExit(PointerEventData eventData) {
        dialogueInteractionUI.OnAnswerUnhovered(answerIndex);
    }
}
