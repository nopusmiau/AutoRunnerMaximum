# AutoRunnerMaximum

A 2D endless runner built with Unity.

## Requirements

- [Git](https://git-scm.com/downloads)
- [Unity Hub](https://unity.com/download)
- **Unity Editor version: `6000.6.9f1`**
  - Use the exact same version as the rest of the team to avoid asset and scene conflicts.
- A code editor: Visual Studio, VS Code, or JetBrains Rider
- *(Optional, for mobile builds)* Android Build Support module (including OpenJDK and Android SDK & NDK Tools) installed through Unity Hub

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/nopusmiau/AutoRunnerMaximum.git
   cd AutoRunnerMaximum
   ```

2. **Install the correct Unity version**

   Open Unity Hub, go to *Installs > Install Editor*, and install the version listed above. If it is not in the list, use the "Archive" link to find it.

3. **Add the project to Unity Hub**

   In Unity Hub, go to *Projects > Add > Add project from disk* and select the `AutoRunnerMaximum` folder (the one that contains `Assets/`, `Packages/` and `ProjectSettings/`).

4. **Open the project**

   The first time you open it, Unity will import all assets and generate the `Library/` folder. This can take several minutes. This is normal.

5. **Open the main scene**

   Open the scene located at `Assets/Scenes/` and press Play.

## Project Settings (already configured)

These should already be set, but if you see a lot of changes in `.meta` or scene files, double-check them in *Edit > Project Settings > Editor*:

- **Version Control Mode:** `Visible Meta Files`
- **Asset Serialization Mode:** `Force Text`

## Contributing

- **Never work directly on `main`.** Create a branch for each task:

  ```bash
  git checkout -b feature/short-description
  ```

- **Commit `.meta` files.** Unity needs them to keep asset references. Never delete them manually or add them to `.gitignore`.
- **Do not commit** `Library/`, `Temp/`, `Logs/`, `UserSettings/`, `Builds/` or IDE files. They are already covered by `.gitignore`.
- **Avoid editing the same scene at the same time.** Scene and prefab merge conflicts are hard to solve. Talk to the team before editing a shared scene, and prefer using prefabs for objects so people can work in parallel.
- **Pull before you start working** and push often:

  ```bash
  git pull origin main
  ```

- Open a **Pull Request** and ask for at least one review before merging into `main`.

## Project Structure

```
GameProject2/
├── Assets/
│   ├── Scenes/
│   ├── Scripts/
│   ├── Prefabs/
│   ├── Sprites/
│   ├── Audio/
│   └── Animations/
├── Packages/
└── ProjectSettings/
```

*(Update this section as the project grows.)*

## Troubleshooting

- **The project opens with errors or a "safe mode" prompt:** check that you are using the exact Unity version listed above, then try *Assets > Reimport All*.
- **Pink or missing sprites/materials:** make sure the project finished importing and that you pulled the latest changes, including `.meta` files.
- **Unity keeps modifying files you did not touch:** verify the Project Settings listed above.