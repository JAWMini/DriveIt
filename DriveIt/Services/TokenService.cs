using Microsoft.AspNetCore.DataProtection;

namespace DriveIt.Services
{
    public class TokenService
    {
        private readonly IDataProtector _protector;

        public TokenService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("CarRentalOfferProtector");
        }

        public string GenerateToken(Guid offerId, int offerExpirationInMinutes)
        {
            var data = $"{offerId}|{DateTime.UtcNow}|{offerExpirationInMinutes}";
            var token = _protector.Protect(data);
            return token;
        }

        public bool ValidateToken(string token, out Guid offerId)
        {
            offerId = Guid.Empty;
            try
            {
                var data = _protector.Unprotect(token);
                var parts = data.Split('|');
                offerId = Guid.Parse(parts[0]);
                var timeStamp = DateTime.Parse(parts[1]);
                var offerExpirationInMinutes = int.Parse(parts[2]);

                if ((DateTime.UtcNow - timeStamp).TotalMinutes > offerExpirationInMinutes)
                {
                    // Token has expired
                    return false;
                }
                return true;
            }
            catch
            {
                // Token is invalid or has expired
                return false;
            }
        }
    }
}
