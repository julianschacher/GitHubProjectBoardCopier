# GitHubProjectBoardCopier
This little program enables you to copy project boards and their cards.

## How to run the program

### Step 1 - Install .NET Core

To be able to run the program, you need to install the .NET Core Runtime on your system.
.NET Core is free and open source and you can download it right here for your OS: https://www.microsoft.com/net/download/

### Step 2 - Get GitHubProjectBoardCopier

You can find the latest compiled version of this program by going to the Releases section.
![Code](https://i.imgur.com/UI8O8E1.png)
And then downloading the zipped release.
![Releases](https://i.imgur.com/FTRnppi.png)

### Step 3 - Run GitHubProjectBoardCopier

After all the preparations are done, you can start to use the program.
First you need to unzip the zip you just downloaded and navigate in your terminal into the unzipped folder.

Then just start the program with the following command:
```
dotnet GitHubProjectBoardCopier.dll
```

Now you should see this:
![enter personal access token](https://i.imgur.com/1l2IuQb.png)
Here the program asks you to enter a personal access token.
The program needs this token to be able to create (and retrieve) project boards in your name.

You need to generate a token with the `repo` scope selected. If you don't know how to do that, here's an explanation by GitHub: https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/

After you got your token, just paste it into the terminal and press enter. You should see the main menu now:
![main menu](https://i.imgur.com/pnhgtDP.png)

Since you most likely want to copy a project board select `[0]` by just pressing `0`.

With this keypress, a method gets called, which will retrieve contents of a project board.

Therefore the program asks you now to enter the owner of the repository, the project board, you want to copy, is in.
So let's say you want to copy one of the project boards shown in the image below, then the owner you need to enter is `algorithm-archivists`.
![algorithm-archive Projects](https://i.imgur.com/MM1VMVu.png)
After you entered the owner, the program also needs to know the name of the repository, the project board is in.
In our example the name is `algorithm-archive`.
![algorithm-archive Projects](https://i.imgur.com/XOxjSiu.png)
The last thing the program needs to know, to be able to copy the project board of your choice, is the project board number.
You can retrieve the number by clicking on your target project board and looking at the URL. The number at the end is the number you need to enter.
In our example the number is `7`.
![algorithm-archive project 7](https://i.imgur.com/7VZ1VVE.png)
Now the program will download the contents of the project board.

After the program done, it will print `Got project board contents.`.
The terminal looks like the following, after doing the example steps:
![Got project board contents.](https://i.imgur.com/rDBL9Nf.png)

At this point you have two possibilities:
- You can copy the project board to another repository.
In this case you need to press the `n` key and you need to enter the owner and the name of the target repository, just like you did before.
- You can copy the project board to the same repository.
To do that, you just need to press the `y` key.

With the selection of your choice, a method gets called, which will create a new project by using the stored project board contents.
This method wants to know(, after it may already asked for the owner and name of the target repository) the name for the new project board copy.

Once you entered the name and the program successfully created a new project board, it prints `Created new project board.`.

If you made it until here, you just successfully copied a project board with its cards! Congratulations!

Lastly program gives you one final info, followed by a menu.
The info shows you how many requests to the GitHub API you have left (since the program needs requests to work) and when the requests reset.
The menu gives you the option to either exit the program or continue to the main menu.
