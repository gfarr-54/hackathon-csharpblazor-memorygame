using System;
using System.Collections.Generic;
using System.Linq;
using hackathon_csharpblazor_memorygame.Components;
using hackathon_csharpblazor_memorygame.Components.Pages;


public static class MemoryGame
{
    public static List<Card> Cards;
    private static Card firstCard;
    private static Card secondCard;
    private static int clickCount;
    public static bool ResetFlippedCards { get; set; } = false;

   

    public static void InitializeMemoryGame(int deckSize)
    {
        Cards = new List<Card>();
        for (int i = 0; i < deckSize; i++)
        {
            var card = new Card(i);
            switch (i)
            {
                case 0:
                case 8:
                    card.MatchId = 0;
                    break;
                case 1:
                case 9:
                    card.MatchId = 1;
                    break;
                case 2:
                case 10:
                    card.MatchId = 2;
                    break;
                case 3:
                case 11:
                    card.MatchId = 3;    
                    break;
                case 4:
                case 12:        
                    card.MatchId = 4;
                    break;
                case 5:
                case 13:
                    card.MatchId = 5;
                    break;
                case 6:
                case 14:
                    card.MatchId = 6;
                    break;
                case 7:
                case 15:
                    card.MatchId = 7;
                    break;
            }
            Cards.Add(card);
        }
        ShuffleCards();
        clickCount = 0;
    }

    public static int GetMatchID(int cardId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId);
        return card != null ? card.Id : -1;
    }

    public static void ShuffleCards()
    {
        Random rng = new Random();
        Cards = Cards.OrderBy(card => rng.Next()).ToList();
    }

    public static bool FlipCard(int cardId)
    {
        var card = Cards.FirstOrDefault(c => c.Id == cardId && !c.IsMatched);
        if (card == null || card.IsFlipped)
        {
            return false;
        }

        card.IsFlipped = true;
        clickCount++;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            CheckForMatch();
        }

        return true;
    }

    private static void CheckForMatch()
    {
        if (firstCard.MatchId == secondCard.MatchId)
        {
            firstCard.IsMatched = true;
            secondCard.IsMatched = true;
        }
        else
        {
            firstCard.IsFlipped = false;
            secondCard.IsFlipped = false;
            ResetFlippedCards = true;
        }

        firstCard = null;
        secondCard = null;
    }

    public static bool IsGameOver()
    {
        return Cards.All(card => card.IsMatched);
    }

    public static void PrintBoard()
    {
        foreach (var card in Cards)
        {
            Console.Write(card.IsFlipped ? $"{card.Id} " : "? ");
        }
        Console.WriteLine();
    }

    public static int GetClickCount()
    {
        return clickCount;
    }
}

public class Card
{
    public int Id { get; }
    public int MatchId { get; set; }
    public bool IsFlipped { get; set; }
    public bool IsMatched { get; set; }

    public Card(int id)
    {
        Id = id;
        IsFlipped = false;
        IsMatched = false;
    }
}

