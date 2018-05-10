using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LinqToTwitter;
using CustomCells13.Objects;

namespace CustomCells13
{
    public class linq2twitter
    {


        #region Class variables
        List<objTweet> lst;
        #endregion
        #region Constructors
        public linq2twitter()
        {


        }
        public async Task<List<objTweet>> twitterAsync(string query)
        {
            try
            {

                lst = new List<objTweet>();
            var auth = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    ConsumerKey = "LdvwFKY5k5PCKGu5aWgWMs411",
                    ConsumerSecret = "ytXiOge4DoGa1H0cGyis2tBTMgINtxi2ZLQHmnTeBffraJ4tz5",
                    AccessToken = "445048624-rdMueoux3A1NKNY51UVowqXNlUWA0tKv99bC1a1U",
                    AccessTokenSecret = "4XCkCRjIZeOUGLNKYVZvFFfAoPqkzD5IFSI2PvZIowDnz"
                }
            };

            await auth.AuthorizeAsync();


            var twitterCtx = new TwitterContext(auth);

                Search searchResponse = await
                        (from search in twitterCtx.Search
                         where search.Type == SearchType.Search &&
                               search.Query == query
                         select search)
                    .SingleOrDefaultAsync();

                if (searchResponse != null && searchResponse.Statuses != null)
                    searchResponse.Statuses.ForEach(tweet =>
                    {
                        
                    lst.Add(new objTweet
                        {
                            text = tweet.Text,
                            likes = tweet.FavoriteCount.ToString()
                        });


                });

              
                return lst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        /*
        public async System.Threading.Tasks.Task<Status> twitterAsync(string query)
        {

            var auth = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore()
                {
                    ConsumerKey = "LdvwFKY5k5PCKGu5aWgWMs411",
                    ConsumerSecret = "ytXiOge4DoGa1H0cGyis2tBTMgINtxi2ZLQHmnTeBffraJ4tz5"
                    //AccessToken = "",
                    //AccessSecretToken = ""

                }
            };

            await auth.AuthorizeAsync();


            var twitterCtx = new TwitterContext(auth);


            var searchResponse =
                await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == query
                 select search)
                .SingleOrDefaultAsync();
            var tweets =
            await
                (from tw in twitterCtx.Status
                 where
                 tw.Type == StatusType.User &&
                 tw.ScreenName == "***"
                 select tw)
                .ToListAsync();

            var searchResponse =
              await
              (from search in twitterCtx.Search
               where search.Type == SearchType.Search &&
                     search.Query == query
               select search)
              .SingleOrDefaultAsync();

            if (searchResponse != null && searchResponse.Statuses != null)
                searchResponse.Statuses.ForEach(tweet =>
                    Console.WriteLine(
                        "User: {0}, Tweet: {1}",
                        tweet.User.ScreenNameResponse,
                        tweet.Text));
        }
            return ;
           
        }*/
        #endregion

        #region Public funct
        public void SearchTweets (string query){
            //if (cancellationTokenSource?.IsCancellationRequested)
            //{
           //     cancellationTokenSource.Cancel();
           // }
           // cancellationTokenSource = new CancellationTokenSource();
            //var cancellationToken = cancellationTokenSource.Token;
           /* Task.Factory.StartNew(async ()=>{
                try
                {
                    var tweets = await SearchTweetsAsync(query, cancellationTokenSource);

                    var e = new TweetsFeched(tweets);
                }
                catch (Exception ex)
                {
                    var e = new TweetsFechedFailedEventArgs(ex.Message);
                    TweetsFetchedFailed?.Invoke(this, e);
                     if (TweetsFetchedFailed== null)
                    {

                    }
                }
            }, cancellationToken);*/
        }


        #endregion
        /*
        public class TweetsFeched:EventArgs{
            public object Tweets { get; private set; }

            public TweetsFeched (object tweets){
                Tweets = tweets;
            }
        }
        public class TweetsFechedFailed : EventArgs
        {
            public object Tweets { get; private set; }

            public TweetsFechedFailed(object tweets)
            {
                Tweets = tweets;
            }
        }*/

    }
}
