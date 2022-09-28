using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Utilities
{
    internal static class CharacterUtilities
    {
        internal static ObservableCharacter UpdateAttributeCurrentValues(this ObservableCharacter character, string key)
        {
            if (!character.Attributes.TryGetValue(key, out var attribute))
            {
                return character;
            }
            
            attribute.CurrentValue = attribute.GetCurrentValue(attribute.Value, character);

            return character;
        }
    }
}