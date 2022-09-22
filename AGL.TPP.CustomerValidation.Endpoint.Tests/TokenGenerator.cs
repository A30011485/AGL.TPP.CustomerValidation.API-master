namespace AGL.TPP.CustomerValidation.Endpoint.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public class TokenGenerator
    {
        public TokenGenerator()
        {
        }

        public string GetJwtToken()
        {
            var auth0Domain = $"https://secure-qtrtest.digital.agl.com.au/"; //{configuration["AGL-Identity-Tenant-Domain"]}
            var audience = SplitSemicolonSeperatedValues("https://web-qtrtest.api.agl.com.au/"); //configuration["AGL-Identity-APIAudience-Identifiers"]

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken securityToken = new JwtSecurityToken(auth0Domain,
                audience[0],
                expires: DateTime.UtcNow.AddYears(10));

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private static List<string> SplitSemicolonSeperatedValues(string commaSeperatedString)
        {
            var values = new List<string>();
            if (!string.IsNullOrEmpty(commaSeperatedString))
            {
                values = commaSeperatedString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(x => !string.IsNullOrEmpty(x)).ToList();
            }
            return values;
        }
    }
}
