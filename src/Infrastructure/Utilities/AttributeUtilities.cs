using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.Infrastructure.Utilities
{
    internal static class AttributeUtilities
    {
        internal static int GetCurrentMaxValue(this ObservableAttribute attribute, ObservableCharacter character) =>
            attribute.MaxValue.HasValue ? attribute.MaxValue.Value + character.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == attribute.Key).Select(prop => prop.Value)).Sum() : attribute.Value;

        internal static int GetCurrentValue(this ObservableAttribute attribute, ObservableCharacter character) =>
            attribute.Value + character.Features.SelectMany(x => x.Properties.Where(prop => prop.Key == attribute.Key).Select(prop => prop.Value)).Sum();
    }
}