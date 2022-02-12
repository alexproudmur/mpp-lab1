using System;
using System.IO;

namespace MP_Lab1
{
    public class task2
    {
        public static void Main(string[] args)
        {
            string path = "C:\\Users\\alexp.DESKTOP-REM2UP4\\source\\repos\\MP_Lab1\\input2.txt";
            int pageLines = 45;
            String upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String lower = "abcdefghijklmnopqrstuvwxyz ";

            String[] content =
                File.ReadAllLines(path);

            int lineCounter = 1;
            string[] lowerLines = new string[content.Length];
            string tmpText;

            ViewLine:
            tmpText = "";
            string line = content[lineCounter - 1];
            if (line.Length == 0)
            {
                goto GotoNextLine;
            }

            int i = 0;

            StringCheck:
            Boolean isLower = false;
            int lowerCaseIndex = 0;
            lowercaseCheck:

            if (content[lineCounter - 1][i] == lower[lowerCaseIndex]) isLower = true;
            lowerCaseIndex++;
            if (lowerCaseIndex != lower.Length && isLower == false) goto lowercaseCheck;

            if (isLower)
            {
                tmpText += content[lineCounter - 1][i];
            }
            else
            {
                int j = 0;

                getLowerCase:
                if (content[lineCounter - 1][i] != upper[j])
                {
                    j++;
                    if (j != upper.Length) goto getLowerCase;
                }

                if (j != upper.Length) tmpText += lower[j];
                else if (content[lineCounter - 1][i] != '.'
                         && content[lineCounter - 1][i] != ','
                         && content[lineCounter - 1][i] != '?'
                         && content[lineCounter - 1][i] != '!'
                         && content[lineCounter - 1][i] != '“'
                         && content[lineCounter - 1][i] != '”'
                         && content[lineCounter - 1][i] != ';')
                {
                    tmpText += content[lineCounter - 1][i];
                }
            }

            i++;
            if (i != line.Length) goto StringCheck;

            GotoNextLine:
            if (tmpText != null) lowerLines[lineCounter - 1] = tmpText;
            lineCounter++;
            if (lineCounter != content.Length) goto ViewLine;

            //result <word>, <frequency>, <pages>
            object[,] result = new object[20000, 4];
            int page = 1;
            int lineNumber = 1;

            ViewPage:
            int prevPage = 0;
            line = lowerLines[lineNumber - 1];
            if (line.Length == 0)
            {
                goto NextLine;
            }

            i = 0;

            String word = "";
            ViewLineOnAPage:

            if (line[i] == ' ')
            {
                int resultCounter = 0;

                ViewSameWord:
                if (word.Equals((String) result[resultCounter, 0]))
                {
                    result[resultCounter, 1] = (int) result[resultCounter, 1] + 1;
                    if ((int) result[resultCounter, 3] != page)
                    {
                        result[resultCounter, 2] = (string) result[resultCounter, 2] + "," + page;
                        result[resultCounter, 3] = page;
                    }

                    word = "";
                    goto NextSymbol;
                }

                if (result[resultCounter, 0] != null)
                {
                    resultCounter++;
                    goto ViewSameWord;

                }

                result[resultCounter, 0] = word;
                result[resultCounter, 1] = 1;
                result[resultCounter, 2] = "" + page;
                result[resultCounter, 3] = page;
                word = "";
                goto NextSymbol;
            }

            word += line[i];
            NextSymbol:
            i++;
            if (i != line.Length) goto ViewLineOnAPage;

            NextLine:
            lineNumber++;

            if (lineNumber % 45 == 0)
            {
                page++;
            }

            if (lineNumber < content.Length) goto ViewPage;

            int k = 0;
            string[] words = new string[20000];

            GetWords:
            if (result[k, 0] != null)
            {
                words[k] = (string) result[k, 0];
                k++;
                goto GetWords;
            }

            string[] tempWords = words;
            words = new string[k];

            int wordCount = 0;
            Rewrite:
            words[wordCount] = tempWords[wordCount];
            wordCount++;
            if (wordCount != k) goto Rewrite;

            string temp;
            int write = 0;
            Outer:
            write++;
            int sort = 0;
            Inner:
            if (words[sort].CompareTo(words[sort + 1]) > 0)
            {
                temp = words[sort + 1];
                words[sort + 1] = words[sort];
                words[sort] = temp;
            }

            sort++;
            if (sort < words.Length - 1) goto Inner;
            if (write < words.Length) goto Outer;

            int amount = words.Length;

            int f = 0;
            Print:

            if (f == amount)
            {
                goto Exit;
            }
            
            int l = 0;
            PrintView:

            if (f != amount && words[f].Equals((String) result[l, 0]))
            {
                Console.WriteLine(result[l, 0] + " - " + result[l, 2]);
                f++;
                goto Print;
            }

            l++;
            goto PrintView;

            Exit:
            i = 0;
        }
    }
}
