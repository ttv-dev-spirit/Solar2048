using Solar2048.Cards;
using UnityEngine;

namespace Tests
{
    public static class Create
    {
        public static Hand Hand() => new GameObject().AddComponent<Hand>();
        public static Card Card()=>new GameObject().AddComponent<Card>();
    }
}