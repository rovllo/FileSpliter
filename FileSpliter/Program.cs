// See https://aka.ms/new-console-template for more information
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

string? sourcePath;
int filesPerFolder = 500;
bool isMoving = false;
bool isRename = false;
bool isRandoam = false;
string selectedFileName = "";
Console.WriteLine("Hello!");

GetFolderPath();

void GetFolderPath()
{
	// Get the source folder path
	Console.Write("Enter the address of the folder where you want the files to be split: ");
	sourcePath = Console.ReadLine();

	if (System.String.IsNullOrEmpty(sourcePath))
	{
		GetFolderPath();
		return;
	}
	if (!Directory.Exists(sourcePath))
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("Error: Such an address does not exist, please check the address.");
		Console.ForegroundColor = ConsoleColor.White;
		GetFolderPath();
		return;
	}
	GetFileNumberPerFolder();
}

void GetFileNumberPerFolder()
{
	// Get the number of files per folder from the user
	Console.Write("How many files should be placed in each folder? ");
	string num = Console.ReadLine();
	if (System.String.IsNullOrEmpty(num))
	{
		GetFileNumberPerFolder();
		return;
	}
	if (!int.TryParse(num, out filesPerFolder))
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("Error: Please enter an integer number.");
		Console.ForegroundColor = ConsoleColor.White;
		GetFileNumberPerFolder();
		return;
	}

	MoveOrCopy();
}

void MoveOrCopy()
{
	Console.Write("Do you want the files to be deleted after splitting? (The default is no, if you want to remove them, enter Y or press N or Enter.): ");
	string input = Console.ReadLine();

	if (System.String.IsNullOrEmpty(input))
	{
		isMoving = false;
	}
	else if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
		isMoving = true;
	else if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
		isMoving = false;
	else
	{
		MoveOrCopy();
		return;
	}

	Randoamaizing();
}

void Randoamaizing()
{
	Console.Write("Do you want files to be randomly categorized? (The default is no, If you want to randomize them, enter Y or press N or Enter.): ");
	string input = Console.ReadLine();

	if (System.String.IsNullOrEmpty(input))
	{
		isRandoam = false;
	}
	else if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
		isRandoam = true;
	else if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
		isRandoam = false;
	else
	{
		Randoamaizing();
		return;
	}

	RenameingFile();
}

void RenameingFile()
{
	Console.Write("If you want the filenames to be renamed to an arbitrary name + number, enter the desired name otherwise press the Enter key: ");
	string? input = Console.ReadLine();
	if (System.String.IsNullOrEmpty(input))
	{
		isRename = false;
	}
	else
		isRename = true;

	if (isRename)
	{
		char[] invalidChars = Path.GetInvalidFileNameChars();
		if (input.Any(i => invalidChars.Contains(i)))
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"\"{input}\" is not a valid file name.");
			Console.ForegroundColor = ConsoleColor.White;
			RenameingFile();
			return;
		}
	}

	selectedFileName = input;

	LetsGo();
}

void LetsGo()
{
	// Get the destination folder path
	string destinationPath = new DirectoryInfo(sourcePath).Name;

	// Create the destination folder if it does not exist
	if (!Directory.Exists(destinationPath))
	{
		Directory.CreateDirectory(destinationPath);
	}

	// Get the files in the source folder
	List<string> files = [.. Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories)];

	if (isRandoam)
	{
		Random rand = new();
		files = [.. files.OrderBy(x => rand.Next())];
	}

	// Initialize the folder index and the file counter
	int folderIndex = 1;
	int fileCounter = 1;

	// Loop through the files
	foreach (string file in files)
	{
		// Get the file name and extension
		string fileName = "";
		string fileExtension = Path.GetExtension(file);

		if (isRename)
		{
			fileName = selectedFileName;
		}
		else
		{
			fileName = Path.GetFileName(file);
		}

		// Create a subfolder name based on the folder index
		string subfolderName = destinationPath + "_" + folderIndex;

		// Create the subfolder path by combining the destination path and the subfolder name
		string subfolderPath = Path.Combine(sourcePath, destinationPath, subfolderName);

		// Create the subfolder if it does not exist
		if (!Directory.Exists(subfolderPath))
		{
			Directory.CreateDirectory(subfolderPath);
		}

		// Create the new file name by combining the subfolder name and the file extension
		string newFileName = fileName + " " + fileCounter + fileExtension;

		// Create the new file path by combining the subfolder path and the new file name
		string newFilePath = Path.Combine(sourcePath, subfolderPath, newFileName);

		if (isMoving)
		{
			//Move the file to the new file path
			File.Move(file, newFilePath, true);
		}
		else
		{
			//Copy the file to the new file path
			File.Copy(file, newFilePath, true);
		}


		// Increment the file counter
		fileCounter++;

		// If the file counter reaches the files per folder limit, reset it and increment the folder index
		if (fileCounter == filesPerFolder + 1)
		{
			fileCounter = 1;
			folderIndex++;
		}
	}
}

// Display a message to indicate the completion of the process
Console.WriteLine("The files have been divided and renamed successfully.");
GetFolderPath();
Console.ReadKey();