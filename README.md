# Group 4 - API Expense Tracker

## Members:
* Luis Enrique Espinoza Vera
* Leonardo Alberto Herrera Rosales
* Santiago Caballero Manzaneda
* Fabian Romero Claros

## Development Team Conventions

### Branch Naming Convention

To ensure consistency and clarity in Git branch names, we will follow the convention below:

#### Category

A git branch should start with a category. Choose one of the following:

- **feature**: For adding, refactoring, or removing a feature.
- **bugfix**: For fixing a bug.
- **hotfix**: For changing code with a temporary solution and/or without following the usual process (typically due to an emergency).
- **test**: For experimenting outside of an issue/ticket.
- **refactor**: For a change in code.

#### Reference

After the category, there should be a "/" followed by the reference of the issue/ticket you are working on. If there’s no reference, use `no-ref`.

#### Description

After the reference, there should be another "/" followed by a description that sums up the purpose of this specific branch. This description should be short and written in "kebab-case."

By default, you can use the title of the issue/ticket you are working on. Just replace any special character with "-".

#### Examples

- **Adding, refactoring, or removing a feature:**  
  `git branch feature/id-42/create-new-button-component`

- **Fixing a bug:**  
  `git branch bugfix/id-342/button-overlap-form-on-mobile`

- **Quickly fixing a bug (possibly with a temporary solution):**  
  `git branch hotfix/no-ref/registration-form-not-working`

- **Experimenting outside of an issue/ticket:**  
  `git branch test/no-ref/refactor-components-with-atomic-design`

### Commit Naming Convention

For commit messages, we will combine and simplify the Angular Commit Message Guideline and the Conventional Commits guideline.

#### Category

A commit message should start with a category of change. You can use the following four categories for nearly everything:

- **feat**: For adding a new feature.
- **fix**: For fixing a bug.
- **refactor**: For changing code for performance or convenience purposes (e.g., readability).
- **chore**: For everything else (writing documentation, formatting, adding tests, cleaning up unused code, etc.).

After the category, there should be a ":" followed by the commit description.

#### Statement(s)

After the colon, the commit description should consist of short statements describing the changes.

Each statement should start with a verb in the imperative form. Statements should be separated by a ";" if there are multiple.

#### Examples

- `git commit -m 'feat: add new button component; add new button components to templates'`
- `git commit -m 'fix: add the stop directive to button component to prevent propagation'`
- `git commit -m 'refactor: rewrite button component in TypeScript'`
- `git commit -m 'chore: write button documentation'`
