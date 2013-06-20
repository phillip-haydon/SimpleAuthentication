namespace WorldDomination.Web.Authentication.Providers
{
    public abstract class BaseProvider
    {
        public IRestClientFactory RestClientFactory;

        protected abstract string DefaultScope { get; }
        protected abstract string ScopeSeparator { get; }
        protected abstract string ScopeKey { get; }

        protected string Scope { get; set; }
        protected string ClientKey { get; set; }
        protected string ClientSecret { get; set; }

        public BaseProvider(ProviderParams providerParams)
        {
            providerParams.Validate();

            RestClientFactory = new RestClientFactory();

            ClientKey = providerParams.Key;
            ClientSecret = providerParams.Secret;

            //Set the scope to default to avoid an else statement
            Scope = DefaultScope;

            //If a scope was defined, get the scopes and join them based on the provider specific separator. 
            if (providerParams.GetScopes().Length > 0)
            {
                Scope = string.Join(ScopeSeparator, providerParams.GetScopes());
            }
        }

        public string GetScope() 
        {
            if (string.IsNullOrWhiteSpace (Scope))
            {
                return string.Empty;
            }

            return ScopeKey + Scope;
        }
    }
}