using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Utilities
{
    internal static class AttributeUtilities
    {
        internal static int GetCurrentMaxValue(this ObservableAttribute attribute, ObservableCharacter character) =>
            attribute.HasMaxValue ? attribute.Value : attribute.Value + character.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == attribute.Key).Select(prop => prop.Value)).Sum();

        internal static int GetCurrentValue(this ObservableAttribute attribute, ObservableCharacter character) =>
            attribute.Value + character.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == attribute.Key).Select(prop => prop.Value)).Sum();
    }
}