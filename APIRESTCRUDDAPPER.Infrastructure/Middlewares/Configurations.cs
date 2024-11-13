using Duende.IdentityServer.Models;

namespace APIRESTCRUDDAPPER.Infrastructure.Middlewares
{
    public static class Config
    {
        public static IEnumerable<Client> ObtenhaClientes()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "apirestcruddapper" }
            }
        };
        }

        public static IEnumerable<ApiScope> ObtenhaEscoposAPI()
        {
            return new List<ApiScope>
        {
            new ApiScope("apirestcruddapper", "apirestcruddapper")
        };
        }

        public static IEnumerable<ApiResource> ObtenhaRecursosAPI()
        {
            return new List<ApiResource>
        {
            new ApiResource("apirestcruddapper", "apirestcruddapper")
            {
                Scopes = { "apirestcruddapper" }
            }
        };
        }

        public static IEnumerable<IdentityResource> ObtenhaRecursosIdentidade()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        }
    }
}
