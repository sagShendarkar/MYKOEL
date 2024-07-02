using System.Security.Claims;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Xml;
using Microsoft.AspNetCore.Http;

namespace MyKoel_Domain.Extensions
{
    public interface IClaimsProvider
    {
        int UserId { get; }
        int CompanyId { get; }
        int ExpiryStatus { get; }
        string DecryptData { get; }
        int GetCompanyId(); 
        // int cid { get; }
        // IEnumerable<int> AccessibleClientIds { get; }
    }
    public class ClaimsProvider : IClaimsProvider 
    { 
        private readonly IHttpContextAccessor accessor;    

        public ClaimsProvider(IHttpContextAccessor accessor) 
        { 
            this.accessor = accessor;          
        }
            // public int cid => int.TryParse(this.accessor.HttpContext.User.FindFirst("CompanyID").Value, out var bob) ? bob : 0;
        public int GetCompanyId()
        {
            return int.TryParse(accessor.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "UserId")?.Value, out var bob) ? bob : 0;
        }
        public int UserId => int.TryParse(accessor.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "UserId")?.Value, out var bob) ? bob : 0;

        // public IEnumerable<int> AccessibleClientIds => accessor.HttpContext?.User?.Claims?.Where(x => x.Type == "AccessibleClientId").Select(x => int.Parse(x.Value)).ToList() ?? new List<int>();

        public int CompanyId => int.TryParse(accessor.HttpContext?.User?.Claims?.SingleOrDefault(x => x.Type == "CompanyID")?.Value, out var bob) ? bob : 0;

        public int ExpiryStatus => throw new NotImplementedException();

        public string DecryptData => throw new NotImplementedException();

       }
    public static class ClaimsPrincipleExtensions
    { 
        public static string GetUsername(this ClaimsPrincipal user)
        {
            
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserId(this ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
        }
        public static int GetCompanyId(this ClaimsPrincipal user)
        {

            //int bc =int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            //string name=user.FindFirst(ClaimTypes.Name)?.Value;
            //return int.Parse();
            var companyID=user.FindFirst("CompanyID").Value;
            return int.Parse(companyID);
        }
 
        private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
            {
                using (Aes aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                    aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                    Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                    Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                    Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                    Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");


                    // Create decryptor object
                    ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                    byte[] cipher = Convert.FromBase64String(cipherText);

                    //Decryption will be done in a memory stream through a CryptoStream object
                    using (MemoryStream ms = new MemoryStream(cipher))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
       
        }
}