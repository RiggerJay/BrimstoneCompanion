using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
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
    }
}