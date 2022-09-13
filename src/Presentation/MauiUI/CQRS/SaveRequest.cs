using MediatR;
using RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels;

namespace RedSpartan.BrimstoneCompanion.MauiUI.CQRS
{
    public class SaveRequest<T> : IRequest<T>
    {
        public T Model { get; }

        private SaveRequest(T model)
        {
            Model = model;
        }

        public static SaveRequest<TChar> With<TChar>(TChar character) where TChar : ObservableCharacter
            => new(character);
    }
}