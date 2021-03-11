using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using freebyTech.Common.Web.Exceptions;
using tivBudget.Dal.Models;
using freebyTech.Common.ExtensionMethods;
using tivBudget.Dal.Repositories;
using tivBudget.Dal.Repositories.Interfaces;
using freebyTech.Common.Web.Logging.Interfaces;

namespace tivBudget.Api.Services
{
  public class TivClaimTypes
  {
    public const string EmailIdentifier = "https://trackitsvalue.com/email";
    public const string EmailVerifiedIdentifier = "https://trackitsvalue.com/email_verified";
  }

  /// <summary>
  /// Service class that simplifies standard user operations.
  /// </summary>
  public static class UserService
  {
    private const string GENERAL_AUTH_ERROR = "Missing required authorization information.";

    public static User GetUserFromClaims(ClaimsPrincipal userPrincipal, IUserRepository userRepository, IApiRequestLogger requestLogger)
    {
      string externalId;
      string emailAddress;
      bool emailVerified;

      try
      {
        externalId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
        emailAddress = userPrincipal.FindFirst(TivClaimTypes.EmailIdentifier).Value;
        emailVerified = userPrincipal.FindFirst(TivClaimTypes.EmailVerifiedIdentifier).Value.IsTrue();
      }
      catch
      {
        throw new WebRequestException(401, GENERAL_AUTH_ERROR);
      }

      if (externalId.IsNullOrEmpty() || emailAddress.IsNullOrEmpty()) throw new WebRequestException(401, GENERAL_AUTH_ERROR);

      if (!emailVerified) throw new WebRequestException(401, "User has not verified their primary email address.");

      User user = null;

      // If we can't ensure the maximum user name from the external source as our search and as our key then we can only rely on email address. This is unllikely but possible.
      if (externalId.Length > 50)
      {
        requestLogger.LogWarn($"External User Name '{externalId}' will be truncated because of length, cannot use for ID based search and must rely on Email '{emailAddress}'.");
        externalId = externalId.VerifySize(50);
      }
      else
      {
        user = userRepository.FindByUserName(externalId);
      }

      if (user == null)
      {
        requestLogger.LogInfo($"User not found by User Name '{externalId}' searching by Email '{emailAddress}'.");
        user = userRepository.FindByEmail(emailAddress);
      }
        
      if (user == null)
      {
        user = NewUserFromExternalClaims(externalId, emailAddress);
        userRepository.Insert(user, externalId);
        requestLogger.LogInfo($"User not found by eternal information User Name '{externalId}' and Email '{emailAddress}', created a new user with ID '{user.Id}'.");
      } else if (!user.UserName.CompareNoCase(externalId) || !user.Email.CompareNoCase(emailAddress))
      {
        var changeNote =
          $"Changing User Name and/or Email to to new value(s) given by external authority. '{user.UserName}' => '{externalId}', '{user.Email}' => '{emailAddress}'";
        requestLogger.LogWarn(changeNote);
        changeNote = $"{changeNote} on {DateTime.Now}";
        if (!user.Notes.IsNullOrEmpty())
        {
          user.Notes += Environment.NewLine + changeNote;
        }
        else
        {
          user.Notes = changeNote;
        }
        user.UserName = externalId;
        user.Email = emailAddress;
        userRepository.Update(user, externalId);
      }

      return user;
    }

    private static User NewUserFromExternalClaims(string externalId, string emailAddress)
    {
      return new User()
      {
        IsNew = true,
        UserName = externalId,
        Email = emailAddress,
        Password = "",
        PasswordSalt = "",
        PasswordVersion = -1,
        IsLocked = false,
        IsEnabled = true,
        LastPasswordChangeDate = DateTime.Now,
        FailedPasswordAttemptCount = 0,
        GroupAssociation = 1
      };
    }
  }
}
