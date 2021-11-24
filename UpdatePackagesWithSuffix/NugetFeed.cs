using NuGet.Common;
using NuGet.Configuration;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UpdatePackagesWithSuffix
{
    internal class NugetFeed
    {
        private SourceRepository _repository;

        public NugetFeed(string source, string username, string passwordText)
        {
            var packageSource = new PackageSource(source);

            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(passwordText))
                packageSource.Credentials = new PackageSourceCredential(
                    source: source,
                    username: username,
                    passwordText: passwordText,
                    isPasswordClearText: true,
                    validAuthenticationTypesText: null);

            _repository = Repository.Factory.GetCoreV3(packageSource);
        }

        public async Task<string> FindLastVersionAsync(string id, string suffix)
        {           
            var resource = await _repository.GetResourceAsync<FindPackageByIdResource>();

            IEnumerable<NuGetVersion> versions = null;
            using (SourceCacheContext cache = new SourceCacheContext())
                versions = await resource.GetAllVersionsAsync(
                    id,
                    cache,
                    NullLogger.Instance,
                    CancellationToken.None);

            return versions
                .Where(v => v.Release == suffix)
                .OrderByDescending(v => v.Version)
                .FirstOrDefault()
                ?.ToFullString();
        }
    }

}
