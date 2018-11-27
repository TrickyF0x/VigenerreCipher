using System;

namespace Vigenerre
{
    class Program
    {
        static void Main(string[] args)
        {
            string text, gamma = "";
            int variant = 0, method = 0;

            Console.WriteLine("Здравствуйте! Что Вы хотите сделать?");

            while (true)
            {
                gamma = "";
                variant = 0; method = 0;
                while (true)
                {
                    Console.WriteLine("1 - зашифровать / 2 - расшифровать");
                    try
                    {
                        variant = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Вводите числа!");
                    }
                    if (variant == 1 || variant == 2)
                        break;
                }

                while (true)
                {
                    Console.WriteLine("1 - обычный / 2 - самоключ");
                    try
                    {
                        method = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Вводите числа!");
                    }
                    if (method == 1 || method == 2)
                        break;
                }

                if (method == 1)
                    while (gamma.Length <= 1)
                    {
                        Console.Write("Введите гамму: ");
                        try
                        {
                            gamma = Console.ReadLine();
                            gamma = gamma.ToLower();
                        }
                        catch
                        {
                            Console.WriteLine("Введите строку!");
                        }
                    }
                else
                {
                    while (gamma.Length != 1)
                    {

                        Console.Write("Введите гамму: ");
                        try
                        {
                            gamma = Console.ReadLine();
                            gamma = gamma.ToLower();
                        }
                        catch
                        {
                            Console.WriteLine("Введите один символ!");
                        }
                    }
                }                   

                if (variant == 1)
                {
                    if (method == 1)
                    {
                        Console.Write("Введите текст для шифрования: ");
                        text = Console.ReadLine();
                        text = text.ToLower();

                        Console.WriteLine(crypt(text, gamma, 1));
                    }
                    else//crypt recur
                    {
                        Console.Write("Введите текст для шифрования: ");
                        text = Console.ReadLine();
                        text = text.ToLower();                        

                        Console.WriteLine(crypt(text, gamma, 2));
                    }
                }
                else//decrypt
                {
                    if (method == 1)
                    {
                        Console.Write("Введите текст для расшифрования: ");
                        text = Console.ReadLine();
                        text = text.ToLower();

                        Console.WriteLine(decrypt(text, gamma, 1));
                    }
                    else//decrypt 
                    {
                        Console.Write("Введите текст для расшифрования: ");
                        text = Console.ReadLine();
                        text = text.ToLower();                                           

                        Console.WriteLine(decrypt(text, gamma, 2));
                    }//decrypt
                }

                Console.WriteLine("Хотите продолжить работу? да / нет");
                if (Console.ReadLine() == "нет")
                    break;
            }
        }

        static char find_char(int index)
        {
            char[] alphabet = {'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н',
                            'о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я','.',',',' ','?'};
            return alphabet[index];

        }//char index_finder

        static int find_index(char leter)
        {
            int index;
            char[] alphabet = {'а','б','в','г','д','е','ё','ж','з','и','й','к','л','м','н',
                            'о','п','р','с','т','у','ф','х','ц','ч','ш','щ','ъ','ы','ь','э','ю','я','.',',',' ','?'};
            for (index = 0; index < 37; ++index)
                if (alphabet[index] == leter)
                    break;
            return index;
        }//index_finder

        static char[] crypt(string text, string gamma, int method)
        {
            char[] cypher_text = new char[text.Length];
            int i = 0, j = 0;

            if(method == 1)
            {
                while(i < text.Length)
                {
                    if (j == gamma.Length)
                        j = 0;

                    cypher_text[i] = find_char((find_index(text[i]) + find_index(gamma[j])) % 37);
                    
                    ++j;
                    ++i;
                }
            }
            else
            {
                cypher_text[0] = find_char((find_index(text[0]) + find_index(gamma[0])) % 37); ++i;

                while (i < text.Length)
                {                   
                    cypher_text[i] = find_char((find_index(text[i]) + find_index(cypher_text[i - 1])) % 37);
                    ++i;
                }
            }
            return cypher_text;
        }

        static char[] decrypt(string text, string gamma, int method)
        {
            char[] decrypted_text = new char[text.Length];
            int i = 0, j = 0, tmp;

            if (method == 1)
            {
                while (i < text.Length)
                {
                    if (j == gamma.Length)
                        j = 0;

                    tmp = find_index(text[i]) - find_index(gamma[j]);
                    if (tmp < 0)
                        tmp += 37;
                    decrypted_text[i] = find_char(tmp % 37);

                    ++j;
                    ++i;
                }
            }
            else
            {
                tmp = find_index(text[0]) - find_index(gamma[0]);
                if (tmp < 0)
                    tmp += 37;
                decrypted_text[0] = find_char(tmp % 37); i = 1;
                while (i < text.Length)
                {
                    tmp = find_index(text[i]) - find_index(text[i - 1]);
                    if (tmp < 0)
                        tmp += 37;
                    decrypted_text[i] = find_char(tmp % 37);
                    ++i;
                }
            }
            return decrypted_text;
        }

    }
}
