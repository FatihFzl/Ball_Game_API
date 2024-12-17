using System.Collections.Concurrent;

namespace Ball_Game_API.GameCharacters
{
    public class GameCharactersManager
    {
        private readonly ConcurrentDictionary<string, Character> gameCharacters = [];

        public Character GetOrCreateCharacter(string connectionId)
        {
            var characterPositionY = gameCharacters.Count > 0 ? 600 : 0; 
            return gameCharacters.GetOrAdd(connectionId, new Character(characterPositionY));

        }

        public void RemoveCharacter(string connectionId)
        {
            gameCharacters.TryRemove(connectionId, out _);
        }

        public void ResetConnections()
        {
            gameCharacters.Clear();
        }

    }
}
