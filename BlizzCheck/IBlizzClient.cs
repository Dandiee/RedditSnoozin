using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlizzCheck.Models;
using BlizzCheck.Models.CharProfile;
using BlizzCheck.Models.Mdi;
using BlizzCheck.Models.Pets;
using Refit;

namespace BlizzCheck
{
    public interface IRawData
    {
        [Get("/reddit/search/submission/?subreddit={subreddit}&sort=asc&sort_type=created_utc&size=1000")]
        Task<SubmitRoot> GetSubmitRoot(string subreddit, long? after = null, long? before =  null);

        [Get("/reddit/search/submission/?subreddit={subreddit}&sort=asc&sort_type=created_utc&size=1000")]
        Task<string> GetSubmitRootRaw(string subreddit, long? after = null, long? before = null);
    }

    public interface IBlizzClient
    {
        [Get("/r/subreddit/new")]
        Task<string> New();
        // https://api.pushshift.io/reddit/search/submission/?subreddit=plantclinic&sort=desc&sort_type=created_utc&size=1000

        [Get("/user/{username}/about")]
        Task<UserRoot> GetUserRoot(string userName);

        [Get("/user/{username}/about")]
        Task<string> GetUserRootRaw(string userName);

        [Get("/r/{subreddit}/comments/{article}")]
        Task<CommentBase[]> GetComments(string subreddit, string article);

        [Get("/r/{subreddit}/comments/{article}")]
        Task<string> GetCommentsRaw(string subreddit, string article);





        [Get("/profile/wow/character/{realm}/{characterName}?namespace=profile-eu&locale=en_US")]
        Task<GetCharacterProfileResponse> GetCharacterProfile(string realm, string characterName);

        [Get("/profile/wow/character/{realm}/{characterName}?namespace=profile-eu&locale=en_US")]
        Task<HttpResponseMessage> GetCharacterProfileHttpResponse(string realm, string characterName);

        [Get("/profile/wow/character/{realm}/{characterName}/encounters/dungeons?namespace=profile-eu&locale=en_US")]
        Task<GetCharacterDungeonsResponse> GetCharacterDungeons(string realm, string characterName);

        [Get("/profile/wow/character/{realm}/{characterName}/mythic-keystone-profile/season/{seasonId}?namespace=profile-eu&locale=en_US")]
        Task<GetCharacterMythicKeystoneSeasonDetails> GetCharacterMythicKeystoneSeasonDetails(string realm, string characterName, int seasonId);

        [Get("/profile/wow/character/{realm}/{characterName}/mythic-keystone-profile/season/{seasonId}?namespace=profile-eu&locale=en_US")]
        Task<string> GetCharacterMythicKeystoneSeasonDetailsHttpResponse(string realm, string characterName, int seasonId);


        [Get("/profile/wow/character/{realmSlug}/{characterName}/collections/pets?namespace=profile-eu&locale=en_US")]
        Task<GetCharacterPetsCollectionSummaryResponse> GetCharacterPetsCollectionSummary(string realmSlug, string characterName);
    }
}
