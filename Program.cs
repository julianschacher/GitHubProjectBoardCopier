using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit;

namespace GitHubProjectBoardCopier
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Please enter your personal access token to access the project boards.");
            var token = Console.ReadLine().Trim();
            var client = new GitHubClient(new ProductHeaderValue("GitHubProjectBoardCopier"));
            var basicAuth = new Credentials(token);
            client.Credentials = basicAuth;

            var projectBoardCopier = new ProjectBoardCopier(client);

            Console.WriteLine("Please enter the repository owner (see URL).");
            var repoOwner = Console.ReadLine().Trim();
            Console.WriteLine("Please enter the repository name (see URL).");
            var repoName = Console.ReadLine().Trim();
            Console.WriteLine("Please enter the project board number (see URL).");
            var projectNumber = 0;
            if (!Int32.TryParse(Console.ReadLine().Trim(), out projectNumber))
                throw new ArgumentException("The project board number is not a number.");

            await projectBoardCopier.GetProjectBoardContentsAsync(repoOwner, repoName, projectNumber);

            Console.WriteLine("Please enter the name for the new project board.");
            var projectName = Console.ReadLine().Trim();

            await projectBoardCopier.CreateNewProjectBoard(repoOwner, repoName, projectName);
        }
    }
}
