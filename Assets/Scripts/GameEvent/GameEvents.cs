using System;

public static class GameEvents
{
    public static class GameplayEvents
    {
        public static GameEvent<Item[]> CardsSpawnRequest = new();
        
        public static GameEvent<CardRequestObject> CardTap = new();
        public static GameEvent<CardRequestObject, CardRequestObject> CardRemoveRequested = new();
        
        public static GameEvent<bool> InputStatusChanged = new();
        
        public static GameEvent ResetGame = new();
        public static GameEvent StartGame = new();
        public static GameEvent<bool> GameComplete = new();
    }
    
    public static class TimerEvents
    {
        public static GameEvent<float> TimerUpdate = new();
        public static GameEvent TimerComplete = new();
    }
}
