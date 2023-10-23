using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;


namespace LevelUp.Helpers
{
    public static class ApiHelper
    {
        public static string NullCheckUserApiKey(User user, LevelUpContext _context, IConfiguration _configuration)
        {
            string? apiKey = _configuration["LEVELUP_APICONNECTIONKEY"];
            if (apiKey == null)
            {
                //Stackframe finds and returns the location where this method was called.
                Log.Warning($"{new StackFrame(1).GetMethod().Name}: Api Key is null");
                throw new NullReferenceException("Api Key was null!");
            }
            user.Reset(_context, apiKey);
            return apiKey;
        }

    }

}


