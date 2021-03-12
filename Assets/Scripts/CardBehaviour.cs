using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardBehaviour : MonoBehaviour
{
    // All card types
    public Button safeCard;
    public Button bombCard;
    public Button reverseCard;
    public Button attackCard;
    public Button snipeCard;

    public Button reshuffle;

    public Transform parent;

    public int totalCards = 15;


    List<Button> cardsToInst = new List<Button>();
    List<Button> clones = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        reshuffle.onClick.AddListener(() => OnButtonPress(reshuffle));

        AddCards();
        Randomize();
        GenerateCards();
    }

    // Adds possible cards to the list
    void AddCards()
    {
        int numBombs = 1;
        int numAttack = 1;
        int numReverse = 1;
        int numSnipe = 1;
        int numSafeCards = totalCards - numBombs - numAttack - numReverse - numSnipe;

        int i = 0;
        // Add Safe Cards
        while (i < numSafeCards)
        {
            cardsToInst.Add(safeCard);
            i++;
        }

        i = 0;
        // Add Bombs
        //while (i < numBombs)
        //{
        //    cardsToInst.Add(bombCard);
        //    i++;
        //}

        cardsToInst.Add(bombCard);
        cardsToInst.Add(reverseCard);
        cardsToInst.Add(attackCard);
        cardsToInst.Add(snipeCard);

    }
    // Randomizes the cards
    void Randomize()
    {
        for (int i = 0; i < cardsToInst.Count; i++)
        {
            Button temp = cardsToInst[i];
            int randomIndex = Random.Range(i, cardsToInst.Count);
            cardsToInst[i] = cardsToInst[randomIndex];
            cardsToInst[randomIndex] = temp;

        }
    }
    // Instatiates the cards in a grid
    void GenerateCards()
    {
        int rows = totalCards / 3;
        int columns = 3;
        int cardNumber = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Button newCard = Instantiate(cardsToInst[cardNumber], new Vector3((i + 1) * 115, (j + 1) * 150, 0), Quaternion.identity, parent);
                newCard.name = "card: " + cardNumber;
                newCard.onClick.AddListener(() => OnButtonPress(newCard));
                clones.Add(newCard);
                cardNumber++;
            }
        }
        Debug.Log(clones.Count);

        cardsToInst.Clear();
        Debug.Log(cardsToInst.Count);

    }

    void Reshuffle()
    {
        for (int i = 0; i < clones.Count; i++)
        {
            clones[i].gameObject.SetActive(false);
        }
        clones.Clear();
        Debug.Log(clones.Count);

        AddCards();

        for (int i = 0; i < cardsToInst.Count; i++)
        {
            cardsToInst[i].interactable = true;
        }
        Randomize();
        GenerateCards();
    }

    public void OnButtonPress(Button button)
    {
        Debug.Log(button.name);
        if (button.name != "Test Button")
            button.interactable = false;
        else
            Reshuffle();
    }

}

