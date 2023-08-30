using System.IO;
using System.Reflection;
using System.Text;

AplicativoDecodeInCsharp();
void AplicativoDecodeInCsharp()
{
    int opcaoUm;

    do
    {
        Console.WriteLine("Aplicativo de testes Intelitrader - Por Marcelo Vesanterä.");
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("                                                          ");

        Console.WriteLine("-------------------- Escolha um teste --------------------");
        Console.WriteLine("1 - Cifra de César");
        Console.WriteLine("2 - Base64");
        Console.WriteLine("3 - Fechar programa");

        opcaoUm = Convert.ToInt32(Console.ReadLine());

        switch (opcaoUm)
        {
            case 1:
                AplicativoCifraDeCesar();
                break;
            case 2:
                AplicativoBase64();
                break;
            case 3:
                Console.WriteLine("Fechando o programa...");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("A opção digitada não existe.");
                break;
        }

        Console.WriteLine("Pressione Enter para reiniciar...");
        Console.ReadLine();
        Console.Clear();
    } while (opcaoUm != 3);
}
void AplicativoCifraDeCesar()
{
    Console.WriteLine("Escolha uma opção para a Cifra de César:");
    Console.WriteLine("1 - Criptografar");
    Console.WriteLine("2 - Descriptografar");

    int opcaoCesar = Convert.ToInt32(Console.ReadLine());

    switch (opcaoCesar)
    {
        case 1:
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Você escolheu Criptografar com Cifra de César.");

            try
            {
                Console.WriteLine("                                          ");
                Console.Write("Digite o texto a ser criptografado: ");
                string textoDigitado = Console.ReadLine();

                string textoCriptografado = CriptografarCifraDeCesar(textoDigitado);
                string nomeArquivoGerado = GravarArquivo(textoCriptografado, "CifraDeCesar_Criptografado");

                Console.WriteLine($"Arquivo {nomeArquivoGerado} criado.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case 2:
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Você escolheu Descriptografar com Cifra de César.");

            try
            {
                Console.WriteLine("                                              ");
                Console.Write("Digite o nome do arquivo a ser descriptografado: ");
                string nomeArquivo = Console.ReadLine();

                string textoDoArquivo = LerArquivo(nomeArquivo);
                string textoDescriptografado = DescriptografarCifraDeCesar(textoDoArquivo);

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Seu texto descriptografado é:");
                Console.WriteLine(textoDescriptografado);
                Console.WriteLine("                                              ");

                string nomeArquivoGerado = GravarArquivo(textoDescriptografado, "CifraDeCesar_Descriptografado");

                Console.WriteLine($"Arquivo {nomeArquivoGerado} criado.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        default:
            Console.WriteLine("A opção digitada não existe.");
            break;
    }
}
void AplicativoBase64()
{
    Console.WriteLine("Escolha uma opção para o Base64:");
    Console.WriteLine("1 - Codificar");
    Console.WriteLine("2 - Decodificar");

    int choiceBase64 = Convert.ToInt32(Console.ReadLine());

    switch (choiceBase64)
    {
        case 1:
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Você escolheu Codificar com Base64.");

            try
            {
                Console.WriteLine("                                          ");
                Console.Write("Digite o texto a ser codificado: ");
                string textoDigitado = Console.ReadLine();

                string textoCodificado = CodificarBase64(textoDigitado);
                string nomeArquivoGerado = GravarArquivo(textoCodificado, "Base64_Codificado");

                Console.WriteLine($"Arquivo {nomeArquivoGerado} criado.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        case 2:
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Você escolheu Decodificar com Base64.");

            try
            {
                Console.WriteLine("                                              ");
                Console.Write("Digite o nome do arquivo a ser decodificado: ");
                string nomeArquivo = Console.ReadLine();

                string textoDoArquivo = LerArquivo(nomeArquivo);
                string textoDecodificado = DecodificarBase64(textoDoArquivo);

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Seu texto decodificado é:");
                Console.WriteLine(textoDecodificado);
                Console.WriteLine("                                              ");

                string nomeArquivoGerado = GravarArquivo(textoDecodificado, "Base64_Decodificado");

                Console.WriteLine($"Arquivo {nomeArquivoGerado} criado.");
                Console.WriteLine("----------------------------------------------");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            break;
        default:
            Console.WriteLine("A opção digitada não existe.");
            break;
    }
}
string GetDiretorioAtual()
{
    string diretorioAtual = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}";

    return diretorioAtual;
}
string GravarArquivo(string textoDigitado, string nomeBaseArquivo)
{
    string diretorioAtual = GetDiretorioAtual();
    string[] arquivos = Directory.GetFiles($"{diretorioAtual}\\");

    List<string> arquivosFiltradosLista = new List<string>();
    foreach (var arquivo in arquivos)
    {
        if ((bool)arquivo.Contains(nomeBaseArquivo))
            arquivosFiltradosLista.Add(arquivo);
    }

    int numeroArquivo = arquivosFiltradosLista.Count() + 1;
    string nomeDoArquivo = $"{nomeBaseArquivo}_{numeroArquivo}.txt";
    StreamWriter streamWriter = new StreamWriter($"{diretorioAtual}\\{nomeDoArquivo}");

    streamWriter.WriteLine(textoDigitado);
    streamWriter.Close();

    return nomeDoArquivo;
}
string LerArquivo(string nomeArquivo)
{
    string diretorioAtual = GetDiretorioAtual();
    StreamReader streamReader = new StreamReader($"{diretorioAtual}\\{nomeArquivo}.txt");

    string texto = streamReader.ReadToEnd();
    streamReader.Close();

    return texto;
}
string CriptografarCifraDeCesar(string textoDigitado)
{
    string textoCriptografado = "";

    for (int i = 0; i < textoDigitado.Length; i++)
    {
        int letraNum = (int)textoDigitado[i];
        int letraNumCriptgrafado = letraNum + 3;

        textoCriptografado += Char.ConvertFromUtf32(letraNumCriptgrafado);
    }

    return textoCriptografado;
}
string DescriptografarCifraDeCesar(string textoDoArquivo)
{
    string textoDescriptografado = "";

    for (int i = 0; i < textoDoArquivo.Length; i++)
    {
        int letraNum = (int)textoDoArquivo[i];
        int letraNumCriptgrafado = letraNum - 3;

        textoDescriptografado += Char.ConvertFromUtf32(letraNumCriptgrafado);
    }

    return textoDescriptografado;
}
string CodificarBase64(string textoDigitado)
{
    byte[] textoEmBytes = Encoding.ASCII.GetBytes(textoDigitado);
    string textoCodificado = System.Convert.ToBase64String(textoEmBytes);

    return textoCodificado;
}
string DecodificarBase64(string textoDoArquivo)
{
    byte[] dadosEmBytes = System.Convert.FromBase64String(textoDoArquivo);
    string textoDecodificado = System.Text.ASCIIEncoding.ASCII.GetString(dadosEmBytes);

    return textoDecodificado;
}