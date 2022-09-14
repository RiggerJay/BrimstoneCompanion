using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RedSpartan.BrimstoneCompanion.MauiUI.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private bool _isBusy = false;
        private string _title = string.Empty;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>([NotNullIfNotNull("newValue")] ref T field, T newValue, Action action, [CallerMemberName] string? propertyName = null)
        {
            var isSuccess = SetProperty(ref field, newValue, propertyName);
            if (isSuccess)
            {
                action?.Invoke();
            }
            return isSuccess;
        }

        protected bool SetProperty<TModel, T>(T oldValue, T newValue, TModel model, Action<TModel, T> callback, Action action, [CallerMemberName] string? propertyName = null)
            where TModel : class
        {
            var isSuccess = SetProperty(oldValue, newValue, model, callback, propertyName);
            if (isSuccess)
            {
                action?.Invoke();
            }
            return isSuccess;
        }
    }
}