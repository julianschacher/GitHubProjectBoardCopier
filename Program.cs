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
            try
            {
                Console.WriteLine(">> Please enter your personal access token to access the project boards.");
                var token = Console.ReadLine().Trim();
                Console.Clear();

                var client = new GitHubClient(new ProductHeaderValue("GitHubProjectBoardCopier"));
                var basicAuth = new Credentials(token);
                client.Credentials = basicAuth;

                var projectBoardCopier = new ProjectBoardCopier(client);

                while (true)
                {
                    Console.WriteLine(">> What do you want to do?");
                    System.Console.WriteLine("[0] Copy a project board. (default)");
                    System.Console.WriteLine("[1] Just get the contents of a project board.");
                    System.Console.WriteLine("[2] Create a new project board.");
                    System.Console.WriteLine("[9] Exit.");
                    var keyChar = Console.ReadKey().KeyChar;

                    if (keyChar == '0')
                    {
                        await GetProjectBoardContentsAsync(projectBoardCopier);
                        await CreateNewProjectBoardAsync(projectBoardCopier);
                    }
                    else if (keyChar == '1')
                        await GetProjectBoardContentsAsync(projectBoardCopier);
                    else if (keyChar == '2')
                        await CreateNewProjectBoardAsync(projectBoardCopier);
                    else if (keyChar == '9')
                        break;

                    Console.WriteLine(">> What do you want to do?");
                    System.Console.WriteLine("[0] Continue.");
                    System.Console.WriteLine("[9] Exit.");
                    keyChar = Console.ReadKey().KeyChar;

                    if (keyChar == '9')
                        break;

                    Console.Clear();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"An error occured. Please read the following message to get some troubleshooting information and restart the program. If the message is weird or you think the program does something wrong, feel free to contact me (the creator of this program).");
                System.Console.WriteLine("Message:");
                System.Console.WriteLine(e.Message);
            }
        }

        public static async Task GetProjectBoardContentsAsync(ProjectBoardCopier projectBoardCopier)
        {
            Console.WriteLine(">> Please enter the repository owner (see URL).");
            var repoOwner = Console.ReadLine().Trim();
            Console.WriteLine(">> Please enter the repository name (see URL).");
            var repoName = Console.ReadLine().Trim();
            Console.WriteLine(">> Please enter the project board number (see URL).");
            var projectNumber = 0;
            if (!Int32.TryParse(Console.ReadLine().Trim(), out projectNumber))
                throw new ArgumentException("The project board number is not a number.");

            await projectBoardCopier.GetProjectBoardContentsAsync(repoOwner, repoName, projectNumber);
        }

        public static async Task CreateNewProjectBoardAsync(ProjectBoardCopier projectBoardCopier)
        {
            Console.WriteLine(">> Do you want to create the project board in the last used repository? [y/n]");
            var keyChar = Console.ReadKey().KeyChar;
            if (keyChar != 'y' && keyChar != 'Y')
            {
                Console.WriteLine(">> Please enter the repository owner (see URL).");
                var repoOwner = Console.ReadLine().Trim();
                Console.WriteLine(">> Please enter the repository name (see URL).");
                var repoName = Console.ReadLine().Trim();
                Console.WriteLine(">> Please enter the name for the new project board.");
                var projectName = Console.ReadLine().Trim();
                await projectBoardCopier.CreateNewProjectBoardAsync(repoOwner, repoName, projectName);
            }
            else
            {
                Console.WriteLine(">> Please enter the name for the new project board.");
                var projectName = Console.ReadLine().Trim();
                await projectBoardCopier.CreateNewProjectBoardAsync(projectName);
            }
        }
    }
}
