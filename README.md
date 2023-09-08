# Object Template Editor for Unity

This Unity project includes a custom editor window that allows users to manage object templates. Users can create, edit, save, and instantiate object templates within the Unity Editor. The project includes features for loading and saving data to JSON files, customizing templates, and instantiating objects with specified properties.

## Features

### 1. Custom Editor Window

- The "Object Template Editor" is accessible from the Unity Editor's Window menu.
- Load, view, and edit object templates via the custom editor window.

### 2. Create and Edit Object Templates

- Users can create new object templates directly within the editor.
- Templates include properties such as name, position, rotation, and scale.
- Templates can be edited and customized in the editor window.

### 3. Save and Load Templates

- Templates are serialized to a JSON file for saving and retrieval.
- Load JSON data from an existing file to populate the editor.
- Save edited templates to the JSON file for future use.

### 4. Instantiate Object Templates

- Users can instantiate object templates in the Unity scene.
- An instantiated template creates a Canvas Hierarchy with specified properties.
- Customize properties like position, rotation, and scale for each instance.

### 5. Error Handling

- The project includes error handling mechanisms for scenarios such as missing or corrupted JSON files.
- Informative error messages are provided to assist with debugging and user feedback.

## Getting Started

1. Clone or download this repository to your local machine.

2. Open the Unity project using Unity Editor.

3. In the Unity Editor, go to `Window > Object Template Editor` to open the custom editor window.

4. Use the editor window to create, edit, save, and instantiate object templates.

## Dependencies

This project is developed using Unity and does not have external dependencies.
