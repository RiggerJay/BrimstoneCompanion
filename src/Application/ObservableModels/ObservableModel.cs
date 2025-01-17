﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Runtime.CompilerServices;

namespace RedSpartan.BrimstoneCompanion.AppLayer.ObservableModels
{
    public class ObservableModel<TObservedModel> : ObservableObject
    {
        protected readonly TObservedModel _model;

        public ObservableModel(TObservedModel model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public TObservedModel GetModel()
        {
            return _model;
        }

        protected TObservedModel Model => _model;

        protected bool SetProperty<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> callback, Action action, [CallerMemberName] string? propertyName = null)
            where TModel : class
        {
            var result = SetProperty(oldValue, newValue, model, callback, propertyName);
            if (result)
            {
                action?.Invoke();
            }
            return result;
        }

        protected bool SetProperty<T>(ref T oldValue, T newValue, Action action, [CallerMemberName] string? propertyName = null)
        {
            var result = SetProperty(ref oldValue, newValue, propertyName);
            if (result)
            {
                action?.Invoke();
            }
            return result;
        }
    }
}