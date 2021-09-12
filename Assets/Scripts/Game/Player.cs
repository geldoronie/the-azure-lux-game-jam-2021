using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private CardsLibrary _library;
    // Start is called before the first frame update
    [SerializeField] public List<Card> Hand;

    public UnityAction OnHandCardsUpdate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    public void DrawNewHand(int cardCount){
        this.Hand = this._library.GetCards(cardCount);
        OnHandCardsUpdate?.Invoke();
    }
}
