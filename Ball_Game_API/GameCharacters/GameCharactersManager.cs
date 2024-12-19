using System.Collections.Concurrent;

namespace Ball_Game_API.GameCharacters
{
    public class GameCharactersManager
    {
        private readonly ConcurrentDictionary<string, Character> gameCharacters = [];

        public Character GetOrCreateCharacter(string connectionId, string? userName = "Player")
        {
            var characterPositionY = gameCharacters.Count > 0 ? 630 : 0; 
            return gameCharacters.GetOrAdd(connectionId, new Character(characterPositionY, userName));

        }

        public List<Character> GetCharacters()
        {
            return [.. gameCharacters.Values];
        }

        public List<string> GetCharacterKeys()
        {
            return [.. gameCharacters.Keys];
        }

        public int GetCharacterCount()
        {
            return gameCharacters.Count;
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
