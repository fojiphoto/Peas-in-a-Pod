using System;

public static class GameEvents
{
    public static class GameplayEvents
    {
        public static GameEvent<CardRequestObject> CardTap = new();
        public static GameEvent<Item[]> CardsSpawnRequest = new();
        public static GameEvent<CardRequestObject, CardRequestObject> CardRemoveRequested = new();
        public static GameEvent ResetGame = new();
        public static GameEvent<bool> InputStatusChanged = new();
    }
}
