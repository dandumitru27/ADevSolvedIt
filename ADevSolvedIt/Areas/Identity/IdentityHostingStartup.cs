[assembly: HostingStartup(typeof(ADevSolvedIt.Areas.Identity.IdentityHostingStartup))]
namespace ADevSolvedIt.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}