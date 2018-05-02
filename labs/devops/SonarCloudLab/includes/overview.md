
Technical debt is the set of problems in a development effort that make forward progress on customer value inefficient. Technical debt saps productivity by making code hard to understand, fragile, time-consuming to change, difficult to validate, and creates unplanned work that blocks progress. Unless they are managed, technical debt can accumulate and hurt the overall quality of the software and the productivity of the development team in the long term.

[SonarCloud](https://about.sonarcloud.io/) is the code quality cloud service provided by SonarSource.
The main features of SonarCloud are:

- 16 languages: Java, JS, C#, C/C++, Objective-C, TypeScript, Python, ABAP, PLSQL, T-SQL and more.
- Thousands of rules to track down hard-to-find bugs and quality issues thanks to powerful static code analyzers.
- Cloud CI Integrations, with Travis, VSTS, AppVeyor and more.
- Deep code analysis, to explore all source files, whether in branches or pull requests, to reach a green quality gate and promote the build.
- Fast and Scalable

### What's covered in this lab

In this lab, you will learn how to integrate Visual Studio Team Services with SonarCloud

- Setup a VSTS project and CI build to integrate with SonarCloud
- Analyze SonarCloud reports
- Integrate static analysis into the VSTS pull request process

### Prerequisites for the lab

1. You will need a **Visual Studio Team Services Account**. If you do not have one, you can sign up for free [here](https://www.visualstudio.com/products/visual-studio-team-services-vs)

1. A **Microsoft Work or School account, or a GitHub/BitBucket account**. SonarCloud supports logins using any of those identity providers.