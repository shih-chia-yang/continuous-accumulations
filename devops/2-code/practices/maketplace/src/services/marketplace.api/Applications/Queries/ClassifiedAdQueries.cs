using System.Runtime.Intrinsics.X86;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using marketplace.api.Applications.Contracts;
using marketplace.api.ViewModels;
using Microsoft.Data.SqlClient;
using marketplace.domain.AggregateModels.ClassifiedAdAggregate;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace marketplace.api.Applications.Queries
{
    public class ClassifiedAdQueries : IClassifiedAdQueries
    {

        private string _connectionString = string.Empty;
        public ClassifiedAdQueries(IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? 
                connectionString : throw new ArgumentNullException(nameof(connectionString));
        }
        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetPublishedClassifiedAds query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result= await connection.QueryAsync<ClassifiedAdListItemViewModel>(
                    @"select ClassifiedAdId as id,
                    Title as title,
                    Amount as price,
                    CurrencyCode
                    from ClassifiedAds c
                    where c.state=@state"
                    ,new {state=(int)ClassifiedAdState.Active}
                );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();
                // return MapClassifiedAdListItem(result);
                return result;
            };
        }

        public async Task<ClassifiedAdDetailsViewModel> Query(GetPublicClassifiedAd query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<ClassifiedAdDetailsViewModel>(
                    @"select c.ClassifiedAdId as id,
                    c.Title as title,
                    c.Text as description,
                    c.Amount as price,
                    c.CurrencyCode,
                    u.DisplayName as sellersdisplayname
                    from ClassifiedAds c
                    left join UserProfiles u on c.OwnerId=u.UserProfileId
                    where c.ClassifiedAdId=@id"
                    , new { id = query.ClassifiedAdId }
                );
                return result;
            }
        }

        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetOwnersClassifiedAd query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<ClassifiedAdListItemViewModel>(
                    @"select c.ClassifiedAdId as id,
                    c.Title as title,
                    c.Amount as price,
                    c.CurrencyCode
                    from ClassifiedAds c
                    where c.OwnerId=@id"
                    , new { id = query.OwnerId }
                );
                return result;
            }
        }

        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> Query(GetPendingReviewClassifiedAds query)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result= await connection.QueryAsync<ClassifiedAdListItemViewModel>(
                    @"select ClassifiedAdId as id,
                    Title as title,
                    Amount as price,
                    CurrencyCode
                    from ClassifiedAds c
                    where c.state=@state"
                    ,new {state=(int)ClassifiedAdState.PendingReview}
                );
                if (result.AsList().Count == 0)
                    throw new KeyNotFoundException();
                // return MapClassifiedAdListItem(result);
                return result;
            };
        }
    }
}