using System;

namespace VideogamesStore.API.Features.Games.Constants;

public static class EndpointNames
{
    #region GamesEndpoints

    public const string GetGames = nameof(GetGames);
    public const string GetGame = nameof(GetGame);
    public const string PostGame = nameof(PostGame);
    public const string UpdateGame = nameof(UpdateGame);
    public const string DeleteGame = nameof(DeleteGame);
    
    #endregion

    #region ShoppingCartEndpoints

    public const string GetShoppingCart = nameof(GetShoppingCart);
    public const string UpsertShoppingCart = nameof(UpsertShoppingCart);

    #endregion
}
