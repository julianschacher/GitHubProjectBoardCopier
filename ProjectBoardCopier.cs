using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;

namespace GitHubProjectBoardCopier
{
    public class ProjectBoardCopier
    {
        private class Column
        {
            public string Name { get; set; }
            public IReadOnlyList<ProjectCard> Cards { get; set; }
        }

        public GitHubClient Client { get; set; }
        private List<Column> CurrentProjectBoardColumns { get; set; }

        public ProjectBoardCopier(GitHubClient client)
        {
            this.Client = client;
        }

        public async Task GetProjectBoardContentsAsync(string repoOwner, string repoName, int projectNumber)
        {
            var projects = await Client.Repository.Project.GetAllForRepository(repoOwner, repoName);
            var project = projects.SingleOrDefault(p => p.Number == projectNumber);
            if (project == null)
                throw new Exception("Couldn't find a project with the given number.");

            var projectId = project.Id;

            this.CurrentProjectBoardColumns = new List<Column>();

            var columns = await Client.Repository.Project.Column.GetAll(projectId);
            foreach (var column in columns)
                this.CurrentProjectBoardColumns.Add(new Column()
                {
                    Name = column.Name,
                    Cards = await Client.Repository.Project.Card.GetAll(column.Id)
                });
        }

        public async Task CreateNewProjectBoard(string repoOwner, string repoName, string projectName)
        {
            if (CurrentProjectBoardColumns == null)
                throw new Exception("No stored project.");

            var repoId = (await Client.Repository.Get(repoOwner, repoName)).Id;
            var newProject = new NewProject(projectName);
            var projectId = (await Client.Repository.Project.CreateForRepository(repoId, newProject)).Id;

            foreach (var column in CurrentProjectBoardColumns)
            {
                var newColumn = new NewProjectColumn(column.Name);
                var columnId = (await Client.Repository.Project.Column.Create(projectId, newColumn)).Id;

                foreach (var card in column.Cards)
                {
                    var newCard = new NewProjectCard(card.Note);
                    await Client.Repository.Project.Card.Create(columnId, newCard);
                }
            }

            System.Console.WriteLine("Created new project.");
        }
    }
}
