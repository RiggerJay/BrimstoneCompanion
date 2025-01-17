﻿namespace RedSpartan.BrimstoneCompanion.AppLayer.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveAsync(T model, string key);

        Task<IList<T>> GetAsync();

        bool Delete(string key);
    }
}