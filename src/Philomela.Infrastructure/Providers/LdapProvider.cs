#pragma warning disable CA1416
using System.Collections;
using System.DirectoryServices;
using System.Reflection;
using Microsoft.Extensions.Options;
using Philomela.Application.Abstractions;
using Philomela.Application.Options;

namespace Philomela.Infrastructure.Providers
{
    /// <inheritdoc />
    public sealed class LdapProvider : ILdapProvider
    {
        private readonly DirectoryEntry _ldapConnection;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapProvider"/> class.
        /// </summary>
        /// <param name="configuration"></param>
        public LdapProvider(IOptions<LdapConnection> ldapConnection)
        {
            _ldapConnection = new DirectoryEntry
            {
                Path = ldapConnection.Value.ConnectionString ??
                       throw new ArgumentNullException(
                           $"Не удалось получить секцию {nameof(ldapConnection.Value.ConnectionString)}"),
                AuthenticationType = AuthenticationTypes.None,
            };
        }

        /// <inheritdoc />
        public async Task<List<T>> GetAllDataAsync<T>(string? filter = null, int pageSize = default)
            where T : class, new()
        {
            DirectorySearcher searcher = new() { Filter = filter, PageSize = pageSize, SearchRoot = _ldapConnection, };

            using SearchResultCollection collection = searcher.FindAll();
            return await DeserializeLdapIntoListModels<T>(collection);
        }

        private Task<List<T>> DeserializeLdapIntoListModels<T>(SearchResultCollection collection)
            where T : class, new()
        {
            List<T> list = new();

            foreach (SearchResult o in collection)
            {
                T model = new();
                foreach (PropertyInfo propertyInfo in model.GetType().GetProperties())
                {
                    string lowerPropName = propertyInfo.Name.ToLower();

                    foreach (DictionaryEntry property in o.Properties)
                    {
                        if (lowerPropName == property.Key.ToString()?.ToLower())
                        {
                            string strValue = GetFirstValueFromProperty(property.Value);
                            propertyInfo.SetValue(model, strValue);
                        }
                    }
                }

                list.Add(model);
            }

            return Task.FromResult(list);
        }

        private string GetFirstValueFromProperty(object? property)
        {
            ResultPropertyValueCollection? propertyValueCollection = property as ResultPropertyValueCollection;
            if (propertyValueCollection is null)
            {
                return string.Empty;
            }

            return propertyValueCollection[0].ToString() ?? string.Empty;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _ldapConnection.Dispose();
            GC.SuppressFinalize(this);
            _disposed = true;
        }
    }
}
