## Usage

:warning: :warning: :warning: 
>We're using [git-lfs](https://git-lfs.github.com/) to handle the project's asset files. 
Make sure you have git-lfs installed before you start working with the repo. 

:warning: :warning: :warning: 

Clone the repo using `git clone <repo link>`. Cloning with `git lfs clone <repo link>` has been deprecated.

You should then be able to open the project (navigate to the cloned folder) in Unity and it will generate the rest of the project files.

It is recommended that you use the git client or the command line rather than the git plugin for Unity as it's been known to brick the editor and doesn't play nice with git-lfs either way.

### Accessing Builds

We have a [Github Action](https://github.com/DH2650-spel21-8/Hop-to-it/actions/workflows/main.yml) that automatically builds the game whenever you push to master. Normally you won't be able to push to master as it is a protected branch, so usually you'll trigger a build when you make a pull request that should indicate if everything is working as expected. I.e. a failing build will mark the pull request with a red x, so we know where to look for problems. Any commits added to a pending pull request will also trigger new builds.

As it stands, the action is configured to build for Windows 32/64 bit, macOSX, and Linux. If we want to suport building to WebGL for example, all we need to do is add it to the list of platforms in [here](https://github.com/DH2650-spel21-8/Hop-to-it/blob/master/.github/workflows/main.yml), under `targetPlatforms:`.

Finished builds can be found as artifacts under the completed workflow run. Example [here](https://github.com/DH2650-spel21-8/Hop-to-it/actions/runs/733262318).

Lastly, you can also trigger a build manually from the [Actions](https://github.com/DH2650-spel21-8/Hop-to-it/actions/workflows/main.yml) page by navigating to the Build Game workflow and clicking on 'Run workflow' near the top of the list of previous runs.
