using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;

namespace MP_Lab1
{
    class task1
    {
        public static void Main(string[] args)
        {
	        string path = "Your path here";
	        String[] ignoredWords = {"for", "the", "a", "in", "an"};
            String upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            String lower = "abcdefghijklmnopqrstuvwxyz,.-“”;!?";
            int mostFrequentCount = 25;

            String content = File.ReadAllText(path);
            
            content += "$";

            String tmpText = "";
            int i = 0;
            
            StringCheck:

            if (content[i] == '\n' || content[i] == ' ' )
            {
                tmpText += ' ';
                i++;
                goto StringCheck;
            }

            if (content[i] == '\r')
            {
                i++;
                goto StringCheck;
            }

            if (content[i] == '$')
            {
	            tmpText += ' ';
                tmpText += '$';
                goto StringCheckEnd;
            }

            Boolean isLower = false;
            int lowerCaseIndex = 0;
                lowercaseCheck:
                if (content[i] == lower[lowerCaseIndex]) isLower = true;
                lowerCaseIndex++;
                if (lowerCaseIndex != lower.Length && isLower == false) goto lowercaseCheck;

                if (isLower)
                {
                    tmpText += content[i];
                }
                else
                {
                    int j = 0;
                    
                    getLowerCase:
                    if (content[i] != upper[j])
                    {
                        j++;
                        goto getLowerCase;
                    }
                    tmpText += lower[j];
                }

                i++;
                goto StringCheck;
                
            StringCheckEnd:
            String word = "";
            Object[,] resultMap = new Object[20000, 2];
            i = 0;
            int wordCount = 0;
            WordWCheck:
            if (tmpText[i] == ' ')
            {
	            int ignoredCounter = 0;
	            Boolean ignored = false;
	            checkIgnored:
	            if (word.Equals(ignoredWords[ignoredCounter]))
	            {
		            ignored = true;
	            }
	            else
	            {
		            ignoredCounter++;
		            if (!(ignoredCounter >= ignoredWords.Length)) goto checkIgnored;
	            }

	            if (ignored)
	            {
		            word = "";
		            i++;
		            goto WordWCheck;
	            }

	            int j = 0;
	            
	            InsertWord:
	            if (word.Equals((String) resultMap[j, 0]))
	            {
		            resultMap[j, 1] = (int) resultMap[j, 1] + 1;
		            word = "";
		            i++;
		            goto WordWCheck;
	            }

	            if (resultMap[j, 0] == null)
	            {
		            if (word.Equals(""))
		            {
			            i++;
			            word = "";
			            goto WordWCheck;
		            }
		            wordCount = j;
		            resultMap[j, 0] = word;
		            resultMap[j, 1] = 1;
		            i++;
		            word = "";
		            goto WordWCheck;
	            }

	            j++;
	            goto InsertWord;
            }

            if (tmpText[i] == '$')
            {
	            wordCount++;
	            goto PrepFrequencies;
            }
            word += tmpText[i];
            i++;
            goto WordWCheck;
            
            PrepFrequencies:
            int[] frequencies = new int[wordCount];

            i = 0;
            addFrequency:
            frequencies[i] = (int) resultMap[i, 1];
            i++;
            if (i < wordCount) goto addFrequency;
            
            int size = frequencies.Length;

            int temp;
            int write = 0;
            Outer:
            write++;
            int sort = 0;
            Inner:
            if (frequencies[sort] < frequencies[sort + 1])
            {
	            temp = frequencies[sort + 1];
	            frequencies[sort + 1] = frequencies[sort];
	            frequencies[sort] = temp;
            }
            sort++;
            if (sort < size - 1) goto Inner;
            if (write < size) goto Outer;

            i = 0;
            int currFrequency = frequencies[0];
            int max = mostFrequentCount;
            int prevFrequency;
            
            printFrequency:
            int k = 0;
            printResult:
            if ((int) resultMap[k, 1] == currFrequency)
            {
	            Console.WriteLine(resultMap[k, 0] + " - " + currFrequency);
            }
            k++;
            if (k != wordCount) goto printResult;
			
            DecrementFrequency:
            if (currFrequency == 1)
            {
	            currFrequency = 0;
	            goto end;
            }
            i++;
            prevFrequency = currFrequency;
            if (i >= frequencies.Length)
            {
	            goto Exit;
            }
            currFrequency = frequencies[i];
            if (currFrequency == prevFrequency) goto DecrementFrequency;
	        
            end:
            max--;
            if(max != 0 && currFrequency != 0) goto printFrequency;
            
            Exit:
            i = 0;
        }
    }
}
