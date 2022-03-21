using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Sat.Recruitment.Test
{
    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public static string SCHEME = "TEST_SCHEME";
        public virtual ClaimsIdentity Identity { get; } = new ClaimsIdentity(new Claim[]
        {
            new Claim("Name", "test name"),
            new Claim("UserName", "test user name"),
            new Claim("Groups", ""),
            new Claim("SessionID", "test")
        }, "test");

    }
}
