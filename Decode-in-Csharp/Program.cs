using System.IO;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

AppDecodeInCsharp();
void AppDecodeInCsharp()
{
    int optionOne;

    do
    {
        Console.WriteLine("Decode in C# App - By Marcelo Vesanterä.");
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("                                                          ");

        Console.WriteLine("-------------------- Choose on option --------------------");
        Console.WriteLine("1 - Caesar Cipher");
        Console.WriteLine("2 - Base64");
        Console.WriteLine("3 - Close app");

        optionOne = Convert.ToInt32(Console.ReadLine());

        switch (optionOne)
        {
            case 1:
                AppCaesarCipher();
                break;
            case 2:
                AppBase64();
                break;
            case 3:
                Console.WriteLine("Closing app...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("This option does not exists.");
                break;
        }

        Console.WriteLine("Press Enter to reload...");
        Console.ReadLine();
        Console.Clear();
    } while (optionOne != 3);
}
void AppCaesarCipher()
{
    Console.WriteLine("Choose one option:");
    Console.WriteLine("1 - Encrypt");
    Console.WriteLine("2 - Decrypt");

    int optionCaesar = Convert.ToInt32(Console.ReadLine());

    switch (optionCaesar)
    {
        case 1:
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("You chose Encrypt with Caesar's Cipher.");

            try
            {
                Console.WriteLine("                                          ");
                Console.Write("Enter the text to be encrypted: ");
                string inputText = Console.ReadLine();

                string encryptedText = EncryptCaesarCipher(inputText);
                string fileNameCreated = SaveFile(encryptedText, "CesarCipher_Encrypted");

                Console.WriteLine($"File {fileNameCreated} created.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case 2:
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("You chose Decrypt with Caesar's Cipher.");

            try
            {
                Console.WriteLine("                                              ");
                Console.Write("Type the name of the file to be decrypted: ");
                string fileName = Console.ReadLine();

                string fileText = ReadFile(fileName);
                string decryptedText = DecryptCaesarCipher(fileText);

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Your decrypted text is:");
                Console.WriteLine(decryptedText);
                Console.WriteLine("                                              ");

                string fileNameCreated = SaveFile(decryptedText, "CesarCipher_Decrypted");

                Console.WriteLine($"File {fileNameCreated} created.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        default:
            Console.WriteLine("The option you typed does not exist.");
            break;
    }
}
void AppBase64()
{
    Console.WriteLine("Choose an option for Base64:");
    Console.WriteLine("1 - Encode");
    Console.WriteLine("2 - Decode");

    int choiceBase64 = Convert.ToInt32(Console.ReadLine());

    switch (choiceBase64)
    {
        case 1:
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("You chose Encode with Base64.");

            try
            {
                Console.WriteLine("                                          ");
                Console.Write("Enter the text to encode: ");
                string inputText = Console.ReadLine();

                string encodedText = EncodeBase64(inputText);
                string fileNameCreated = SaveFile(encodedText, "Base64_Codificado");

                Console.WriteLine($"File {fileNameCreated} created.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case 2:
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("You chose Decode with Base64.");

            try
            {
                Console.WriteLine("                                              ");
                Console.Write("Enter the name of the file to be decoded: ");
                string fileName = Console.ReadLine();

                string fileText = ReadFile(fileName);
                string decodedText = DecodeBase64(fileText);

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Its decoded text is: ");
                Console.WriteLine(decodedText);
                Console.WriteLine("                                              ");

                string fileNameCreated = SaveFile(decodedText, "Base64_Encoded");

                Console.WriteLine($"File {fileNameCreated} created.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        default:
            Console.WriteLine("The option you typed does not exist.");
            break;
    }
}
string GetCurrentDirectory()
{
    string currentDirectory = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}";

    return currentDirectory;
}
string SaveFile(string inputText, string fileNameBase)
{
    string currentDirectory = GetCurrentDirectory();
    string[] files = Directory.GetFiles($"{currentDirectory}\\");

    List<string> filteredFilesList = new List<string>();
    foreach (var file in files)
    {
        if ((bool)file.Contains(fileNameBase))
            filteredFilesList.Add(file);
    }

    int fileNumber = filteredFilesList.Count() + 1;
    string fileName = $"{fileNameBase}_{fileNumber}.txt";
    StreamWriter streamWriter = new StreamWriter($"{currentDirectory}\\{fileName}");

    streamWriter.WriteLine(inputText);
    streamWriter.Close();

    return fileName;
}
string ReadFile(string fileName)
{
    string currentDirectory = GetCurrentDirectory();
    StreamReader streamReader = new StreamReader($"{currentDirectory}\\{fileName}.txt");

    string text = streamReader.ReadToEnd();
    streamReader.Close();

    return text;
}
string EncryptCaesarCipher(string inputText)
{
    string encryptedText = "";

    for (int i = 0; i < inputText.Length; i++)
    {
        int charNum = (int)inputText[i];
        int encryptedCharNum = charNum + 3;

        encryptedText += Char.ConvertFromUtf32(encryptedCharNum);
    }

    return encryptedText;
}
string DecryptCaesarCipher(string fileText)
{
    string decryptedText = "";

    for (int i = 0; i < fileText.Length; i++)
    {
        int charNum = (int)fileText[i];
        int decryptedCharNum = charNum - 3;

        decryptedText += Char.ConvertFromUtf32(decryptedCharNum);
    }

    return decryptedText;
}
string EncodeBase64(string inputText)
{
    byte[] textInBytes = Encoding.ASCII.GetBytes(inputText);
    string decodedText = System.Convert.ToBase64String(textInBytes);

    return decodedText;
}
string DecodeBase64(string fileText)
{
    byte[] dataInBytes = System.Convert.FromBase64String(fileText);
    string decodedText = System.Text.ASCIIEncoding.ASCII.GetString(dataInBytes);

    return decodedText;
}