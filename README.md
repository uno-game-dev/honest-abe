# honest-abe
Beat 'em up game featuring Abe Lincoln, built with Unity

## Team
- Rachel Farrell - Developer / Designer
- Ted Mader - Lead Developer / Developer
- Parker Sprouse - Developer / QA Tester
- Chris Toups - Producer / Sound Designer
- Kyle Whittington - Head of Marketing / Lead Artist
- Christian Simmers - Artist (2D) / Designer
- Breena Crump - Developer / QA Tester
- Edward Garcia - Developer / Artist
- David DiMaggio - Sound Designer / Game Designer
- Maurice Robert - Developer / Game Designer

## Using git submodules
### References:
#### git-scm
- [Submodule Guide](http://git-scm.com/book/en/v2/Git-Tools-Submodules)
- [`git submodule` manual](https://git-scm.com/docs/git-submodule)

#### Atlassian
- [Git Submodules: Core concept, workflows and tips](http://blogs.atlassian.com/2013/03/git-submodules-workflows-tips/)

### F.A.Q.
#### How do I get the submodules into my local clone of honest-abe?
If you're cloning a brand new copy of `honest-abe`, throw the `--recursive` flag into the mix and it will `init` and `update` them for you.

```bash
$ git clone --recursive https://github.com/Tlmader/honest-abe.git honest-abe
Cloning into 'honest-abe'...
...
Submodule 'Assets/Audio' (https://github.com/UNO-CSCI-4675/honest_abe_audio.git) registered for path 'Assets/Audio'
Submodule 'Assets/Models' (https://github.com/UNO-CSCI-4675/honest_abe_models.git) registered for path 'Assets/Models'
Submodule 'Assets/Sprites' (https://github.com/UNO-CSCI-4675/honest_abe_sprites.git) registered for path 'Assets/Sprites'
Cloning into 'Assets/Audio'...
...
Submodule path 'Assets/Audio': checked out '32999ebd7253beb31abc740a6483ded79a509ef3'
Cloning into 'Assets/Models'...
Submodule path 'Assets/Models': checked out 'e5fcf0e4e50944e6d72f503ed144fe218c9a953e'
Cloning into 'Assets/Sprites'...
...
Submodule path 'Assets/Sprites': checked out '463d3d74c2d830a59a12013e66eea6087a8213ec'
```

If you've already cloned `honest-abe`, a `submodule init` and `submodule update` or just an `submodule update --init` will do.

```bash
$ git submodule init
$ git submodule update
```

or

```bash
$ git submodule update --init
```

#### I want to change the submodule commit `honest-abe` is targeting.<a name="how_to_change_submodule_commit"/>

```bash
$ cd honest-abe

$ cd Assets/Audio # navigate into the submodule directory

$ git status
HEAD detached at 32999eb
nothing to commit, working directory clean

$ git checkout master # checkout the commit you want to target
Previous HEAD position was 32999eb... Merge branch 'master' of https://github.com/UNO-CSCI-4675/honest_abe_audio
Switched to branch 'master'
Your branch is up-to-date with 'origin/master'.

$ git pull --rebase
Current branch master is up to date.

$ cd ../.. # navigate into the honest-abe repo

$ git status
On branch chore/example
Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)
  (commit or discard the untracked or modified content in submodules)

    modified:   Assets/Audio (new commits)

$ git diff Assets/Audio
diff --git a/Assets/Audio b/Assets/Audio
index 32999eb..7b5fa83 160000
--- a/Assets/Audio
+++ b/Assets/Audio
@@ -1 +1 @@
-Subproject commit 32999ebd7253beb31abc740a6483ded79a509ef3
+Subproject commit 7b5fa8370a79ba9bf5000f16e2f975166b4ac59e

$ git add Assets/Audio # stage the commit target change

$ git commit -m "Update Assets/Audio to most recent master branch" # commit

$ git push # share with others
```

#### There are unstaged changes in my local submodules and `git diff` marks the current commit as `dirty`.

If `git status` shows a submodule as having unstaged changes, `git diff` will have marked the submodule commit as `dirty`:

```bash
$ git diff
diff --git a/Assets/Models b/Assets/Models
--- a/Assets/Models
+++ b/Assets/Models
@@ -1 +1 @@
-Subproject commit 06a72b7463d03ae57231085f5d3072f9d35839cb
+Subproject commit 06a72b7463d03ae57231085f5d3072f9d35839cb-dirty
```

This means your local copy of the submodule has had its contents changed somehow (`e.g. .meta file changes`). If you've changed Unit settings involving assets within a submodule, you will need to:
1. navigate to the submodule directory
2. stage and commit what changes you want to share
3. push the submodule changes upstream
4. see [I want to change the submodule commit `honest-abe` is targeting](#how_to_change_submodule_commit).

#### I cannot see a recent asset (model texture, sprite, etc.) change someone else pushed upstream in Unity.

If you are unable to see a recent texture change within your local Unity project after fetching & merging/rebasing recent honest-abe commits, and the changes are confirmed to exist on remote, feel free to delete the submodule directory (`rm -rf Assets/Audio`). After your local copy of the submodule was removed `git submodule update --init [optional submodule name]` will restore the submodule to the targeted commit.

```bash
$ cd honest-abe
$ rm -rf Assets/Audio
$ git submodule update --init Assets/Audio
```
