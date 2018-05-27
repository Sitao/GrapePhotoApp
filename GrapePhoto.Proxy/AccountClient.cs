﻿using System;
using System.Collections.Generic;
using System.Text;
using GrapePhoto.Web.Models.Account;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GrapePhoto.Web.Models;
using RestSharp.Serializers.Newtonsoft.Json;
using System.Linq;

namespace GrapePhoto.Proxy
{
    public static class AccountAPI
    {
        public static string SignIn => $"/user/login";
        public static string SignUp => $"/user/register";

        public static string UpdateUser => $"/user/update";
        public static string SearchUsers => $"/user/search";

        public static string Following => $"/user/follow";
        public static string Followers => $"/user/followee";
    }

    public class AccountClient : IAccountClient
    {
     
        private string _baseUri;
        private IRestClient _client;
        public AccountClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new RestClient(_baseUri);
        }

        public List<User> GetAllFollowersUsersByUserId(string userId)
        {
            var request = new RestSharp.RestRequest(AccountAPI.Followers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };
            request.AddJsonBody(new
            {
               UserId = userId
            });

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
                return users;
            }
            else
            {
                return new List<User>();
            }
        }

        public List<User> GetAllFollowingUsersByUserId(string userId)
        {
            //callAPI get all following users;

            var followingUsers = new List<User>();

            followingUsers.Add(new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Txxni",
                AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
            });

            return followingUsers;

            //var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            //{
            //    JsonSerializer = new NewtonsoftJsonSerializer()
            //};
            //var user = new User { UserName = userId };
            //request.AddJsonBody(new
            //{
            //    UserId = userId
            //});

            //IRestResponse response = _client.Post(request);
            //var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            //if (json.success)
            //{
            //    var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
            //    return users;
            //}
            //else
            //{
            //    return new List<User>();
            //}
        }

        public List<User> GetAllFollowingUsersByUserName(string userid)
        {
      
            //callAPI get all following users;

            var followingUsers = new List<User>();

            followingUsers.Add(new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Txxni",
                AvatarPicUrl = "https://scontent-sin6-2.cdninstagram.com/vp/3086c8b2f4efef61ad662a821c60e09d/5B8153DF/t51.2885-19/s150x150/29403266_193822707890209_7859898672618668032_n.jpg",
            });

            return followingUsers;

        }

        public User GetUserByUserId(string userId)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SearchUsers)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            var user = new User { UserName = userId };
            request.AddJsonBody(new
            {
                Key = "UserId",
                Value = userId
            });
             
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(json.result.ToString());
                return users.First();
            }
            else
            {
                return null;
            }
        }
 
        public SignInResult SignIn(SignInViewModel user)
        {
            var request = new RestSharp.RestRequest(AccountAPI.SignIn)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddJsonBody(user);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            return new SignInResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error
            };
        }

        public SignUpResult SignUp(SignUpViewModel signUpViewModel)
        {

            var request = new RestSharp.RestRequest(AccountAPI.SignUp)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };

            request.AddJsonBody(signUpViewModel);
            var user = new User();
            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            if (json.success)
            {
                user = new User()
                {
                    UserName = signUpViewModel.UserName,
                    PasswordHash = signUpViewModel.Password,
                    Email = signUpViewModel.Email,
                    UserId = signUpViewModel.UserId
                }; 
              
            }
          
            return new SignUpResult()
            {
                Succeed = json.success,
                ErrorMessage = json.error,
                user = user
            };
        }

        public User UpdateProfile(User user)
        {
            var request = new RestSharp.RestRequest(AccountAPI.UpdateUser)
            {
                JsonSerializer = new NewtonsoftJsonSerializer()
            };
            request.AddJsonBody(user);

            IRestResponse response = _client.Post(request);
            var json = JsonConvert.DeserializeObject<GenericAPIResponse>(response.Content);
            
            return user;
        }
    }
}
