﻿using DataAccessLayer;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using WebAPI.UserManagement;
using SecurityLayer.Sessions;
using WebAPI.UserManagement;
using ServiceLayer.PasswordChecking.HashFunctions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using KFC.SIT.WebAPI.Utility;
using SecurityLayer.Authorization;
using SecurityLayer.Authorization.AuthorizationManagers;

namespace KFC.SIT.WebAPI.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserManagerController : ApiController
    {
        // GET api/<controller>
        [HttpGet]
        public IQueryable<UserDTO> GetAllUsers()
        {
            UserManager umManager = new UserManager();
            List<User> userList = umManager.GetAllUser();

            List<UserDTO> list = new List<UserDTO>();
            foreach (var user in userList)
            {
                list.Add(new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Category = user.Category.Value,
                    DateOfBirth = user.DateOfBirth.ToString("MMMM dd yyyy hh:mm:ss tt"),
                    CreatedAt = user.CreatedAt.ToString("MMMM dd yyyy hh:mm:ss tt")
                    

                });
            }

            return list.AsQueryable();
        }

        [HttpGet]
        [ActionName("GetContextId")]
        public HttpResponseMessage GetContextId()
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
            );
            if (securityContext == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            string updatedToken = sm.RefreshSession(securityContext.Token);
            // TODO finish this.
            return Request.CreateResponse(
                HttpStatusCode.OK, new { userid = securityContext.UserId, SITtoken = updatedToken }
            );
        }

        [HttpGet]
        [ActionName("GetUserInfoWithId")]
        public HttpResponseMessage GetUserInfoWithId(int id)
        {
            SecurityContext securityContext = SecurityContextBuilder.CreateSecurityContext(
               Request.Headers
           );
            if (securityContext == null)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            SessionManager sm = new SessionManager();
            if (!sm.ValidateSession(securityContext.Token))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }

            AuthorizationManager authorizationManager = new AuthorizationManager(
                securityContext
            );
            // TODO get this from table in database.
            List<string> requiredClaims = new List<string>()
            {
                "CanReadOwnStudentAccount"
            };

            if (securityContext.UserId != id ||
                !authorizationManager.CheckClaims(requiredClaims))
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                UserManager um = new UserManager();
                UserDTO userDTO = um.GetUserInfo(id);
                string updatedToken = sm.RefreshSession(securityContext.Token);
                return Request.CreateResponse(
                    HttpStatusCode.OK, new {User = userDTO, SITtoken = updatedToken}
                );
            }
        }


        // POST api/<controller>
        [HttpPost]
      
        public HttpResponseMessage CreateNewUSer([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            var createdUser = umManager.CreateUserAccount(userDto);
            var message = Request.CreateResponse(HttpStatusCode.OK, userDto);
  
            return message;

        }

        // PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult EditUser([FromBody] UserDTO userDto)
        {
            UserManager umManager = new UserManager();
            User foundUser = umManager.FindUserById(userDto.Id);
            foundUser.UserName = userDto.UserName;
            foundUser.FirstName = userDto.FirstName;
            foundUser.LastName = userDto.LastName;
            return Ok(umManager.UpdateUserAccount(foundUser));
        }


        [HttpDelete]    
        public IHttpActionResult DeleteUser(SSOPayload ssoPayload)
        {
            //UserManager umManager = new UserManager();
            //umManager.DeleteUserAccount(umManager.FindUserById(id));
            return Ok();
        }

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }




    }
}
