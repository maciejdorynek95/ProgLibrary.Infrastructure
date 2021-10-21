﻿namespace ProgLibrary.Infrastructure.Settings.JwtToken
{
    public class JwtSettings 
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }     
        public int ExpiryMinutes { get; set; }
      
    }

}
