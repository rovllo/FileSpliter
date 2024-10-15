# FileSpliter
# File Splitting and Organization Program

This C# console application is designed to organize files by splitting them into multiple subfolders. It offers users various options for customizing the file organization process, making it a versatile tool for managing large collections of files.

## Core Functionality

The program allows users to:
1. Select a source folder containing files to be organized.
2. Specify the number of files to be placed in each subfolder.
3. Choose whether to move or copy files during the process.
4. Optionally randomize the order of files before splitting.
5. Optionally rename files during the organization process.

## Libraries and Namespaces Used

1. `System.IO`: Used for file and directory operations.
2. `System.Linq`: Used for LINQ operations, particularly in the randomization process.
3. `System.Runtime.InteropServices.JavaScript.JSType`: Imported but not explicitly used in the provided code.
4. `System.Numerics`: Imported but not explicitly used in the provided code.

## Key Methods and Processes

### 1. User Input Collection

Several methods are dedicated to collecting and validating user input:

- `GetFolderPath()`: Prompts for and validates the source folder path.
- `GetFileNumberPerFolder()`: Asks for and validates the number of files per subfolder.
- `MoveOrCopy()`: Determines if files should be moved or copied.
- `Randoamaizing()`: Asks if files should be randomized before splitting.
- `RenameingFile()`: Checks if files should be renamed and collects the new name pattern.

Each of these methods includes input validation and error handling, ensuring that the program receives valid data before proceeding.

### 2. File Processing (LetsGo() Method)

The `LetsGo()` method is the core of the program, handling the actual file organization process:

1. It creates a destination folder based on the source folder's name.
2. Retrieves all files from the source folder, including subfolders.
3. Optionally randomizes the file list.
4. Iterates through the files, creating subfolders as needed.
5. Moves or copies each file to its new location, potentially renaming it in the process.

### 3. File and Directory Operations

The program extensively uses the `System.IO` namespace for file and directory operations:

- `Directory.Exists()` and `Directory.CreateDirectory()` for managing folders.
- `Path` class methods for manipulating file and directory paths.
- `File.Move()` and `File.Copy()` for relocating files.

### 4. Randomization

When randomization is requested, the program uses LINQ's `OrderBy()` method with a new `Random()` instance to shuffle the file list.

### 5. Error Handling and User Experience

The program implements several user-friendly features:
- Color-coded error messages (using `Console.ForegroundColor`).
- Recursive method calls for re-prompting after invalid inputs.
- Clear instructions and confirmations throughout the process.

## Program Flow

1. The program starts by calling `GetFolderPath()`, initiating the user input collection process.
2. It sequentially calls methods to gather all necessary information from the user.
3. Once all parameters are set, it calls the `LetsGo()` method to perform the file organization.
4. After completion, it displays a success message and loops back to `GetFolderPath()`, allowing for multiple operations without restarting the program.

## Technical Considerations

- The program uses recursion in input methods to handle invalid inputs, ensuring robust user interaction.
- It employs string comparison with `StringComparison.CurrentCultureIgnoreCase` for case-insensitive user input processing.
- The file renaming process includes validation against invalid filename characters.
- The program uses modern C# features like list patterns (`[.. someList]`) for concise list manipulation.

## Potential Improvements

While the program is functional, there are areas for potential enhancement:
1. Implementing async file operations for improved performance with large file sets.
2. Adding a progress indicator for lengthy operations.
3. Implementing logging for better tracking and debugging.
4. Adding options for different sorting methods beyond randomization.
5. Implementing exception handling for potential I/O errors during file operations.

This program demonstrates effective use of C# file I/O operations, user input handling, and basic algorithm implementation to create a useful file organization tool. Its modular structure allows for easy expansion and modification to meet various file management needs.
