using Lumia.DataContext;
using Lumia.Models;

namespace Lumia.Services
{
    public class SettingsService
    {
        private readonly LumiaDbContext _lumiaDbContext;

        public SettingsService(LumiaDbContext lumiaDbContext)
        {
            _lumiaDbContext = lumiaDbContext;
        }

        public List<Settings> GetSettings()
        {
            return _lumiaDbContext.Settings.ToList();
        }
    }
}
