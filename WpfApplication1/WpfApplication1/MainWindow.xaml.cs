using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThinkLib;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int length(string s)
        {
            int length = 0;
            try
            {
                while (true)
                {
                    char c = s[length];
                    length++;
                }
            } catch (IndexOutOfRangeException e)
            {

            }
            return length;
        }

        private void btnLength_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(length("four"), 4);
            Tester.TestEq(length("."), 1);
            Tester.TestEq(length("  "), 2);
            Tester.TestEq(length("zombies vs humans"), 17);
            Tester.TestEq(length("suh dude"), 8);
        }

        bool contains(string subs, string s)
        {
            bool contains = false;
            int i = 0;
            while (length(s) - length(subs) > 0 && i <= length(s) - length(subs))
            {
                if (subs[0] == s[i])
                {
                    contains = true;
                    for (int j = 1; j < length(subs); j++)
                    {
                        if (subs[j] != s[i + j])
                        {
                            contains = false;
                        }
                    }
                }
                if (contains)
                {
                    break;
                }
                i++;
            }
            return contains;
        }

        private void btnContains_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(contains("six", "sixty"), true);
            Tester.TestEq(contains("hi", "thirty five"), true);
            Tester.TestEq(contains("no", "banana"), false);
            Tester.TestEq(contains("and", "banana"), false);
            Tester.TestEq(contains("sixty", "six"), false);
        }

        int indexOf(string subs, string s)
        {
            bool contains = false;
            int i = 0;
            while (length(s) - length(subs) > 0 && i <= length(s) - length(subs))
            {
                if (subs[0] == s[i])
                {
                    contains = true;
                    for (int j = 1; j < length(subs); j++)
                    {
                        if (subs[j] != s[i + j])
                        {
                            contains = false;
                        }
                    }
                }
                if (contains)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        private void btnIndexOf_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(indexOf("six", "sixty"), 0);
            Tester.TestEq(indexOf("human", "Hi there, human"), 10);
            Tester.TestEq(indexOf("hello", "0123456hello2389746"), 7);
            Tester.TestEq(indexOf("na", "banana"), 2);
            Tester.TestEq(indexOf("sx", "six"), -1);
        }

        string insertSubString(string s, string x, int pos)
        {
            string result = "";
            if (pos <= length(s) && pos >= 0)
            {
                for (int i = 0; i < pos; i++)
                {
                    result += s[i];
                }
                result += x;
                for (int i = pos; i < length(s); i++)
                {
                    result += s[i];
                }
            }
            return result;
        }

        private void btnInsertSubString_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(insertSubString("xxxx", "y", 2), "xxyxx");
            Tester.TestEq(insertSubString("xxxx", "y", 10), "");
            Tester.TestEq(insertSubString("xxxx", "y", 4), "xxxxy");
            Tester.TestEq(insertSubString("xxxx", "y", 0), "yxxxx");
            Tester.TestEq(insertSubString("xxxx", "y", -2), "");
        }

        string replaceSubString(string s, string news, string olds)
        {
            string result = "";
            if (!contains(olds, s))
            {
                return "";
            }
            if (olds == news)
            {
                return s;
            }
            string toUpdate = s;
            do
            {
                result = "";
                int pos = indexOf(olds, toUpdate);
                for (int i = 0; i < pos; i++)
                {
                    result += toUpdate[i];
                }
                result += news;
                for (int i = pos + length(olds); i < length(toUpdate); i++)
                {
                    result += toUpdate[i];
                }
            } while (contains(olds, toUpdate = result));
            return result;
        }


        private void btnReplaceSubString_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(replaceSubString("Hi there, human", "zombie", "human"), "Hi there, zombie");
            Tester.TestEq(replaceSubString("I love you", "eat", "love"), "I eat you");
            Tester.TestEq(replaceSubString("Hi there, human human", "zombie", "human"), "Hi there, zombie zombie");
            Tester.TestEq(replaceSubString("Hi there, zombie", "zombie", "zombie"), "Hi there, zombie");
            Tester.TestEq(replaceSubString("Hi there, human", "zombie", "zombie"), "");
        }

        string deleteSubString(string s, string subs)
        {
            string result = "";
            if (subs == "")
            {
                return s;
            }
            if (!contains(subs, s))
            {
                return "";
            }
            int pos = indexOf(subs, s);
            for (int i = 0; i < pos; i++)
            {
                result += s[i];
            }
            for (int i = pos+length(subs); i < length(s); i++)
            {
                result += s[i];
            }
            return result;
        }

        private void btnDeleteSubString_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(deleteSubString("Hi there, human", ", human"), "Hi there");
            Tester.TestEq(deleteSubString("Hi there, human", "zombie"), "");
            Tester.TestEq(deleteSubString("Banana", "na"), "Bana");
            Tester.TestEq(deleteSubString("Hi there, human", ""), "Hi there, human");
            Tester.TestEq(deleteSubString("Sun day", " "), "Sunday");
        }

        List<string> split(string s, char c)
        {
            List<string> list = new List<string>();
            while (contains(c + "", s))
            {
                int pos = indexOf(c + "", s);
                string word = "";
                for(int i = 0; i < pos; i++)
                {
                    word += s[i];
                }
                list.Add(word);
                s = deleteSubString(s, word + c);
            }
            list.Add(s);
            return list;
        }

        private void btnSplit_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(split("x xx xxx", ' '), new List<string> { "x", "xx", "xxx" });
            Tester.TestEq(split("x xx xxx", 'y'), new List<string> { "x xx xxx" });
            Tester.TestEq(split("Happy hunting, humans", 'h'), new List<string> { "Happy ", "unting, ", "umans" });
        }

        int stringCompare(string s1, string s2)
        {
            bool swop = false;
            if (s1 == s2)
            {
                return 0;
            }
            if (length(s1) < length(s2))
            {
                swop = true;
                string temp = s1;
                s1 = s2;
                s2 = temp;
            }
            s1 = toLowerLetters(s1);
            s2 = toLowerLetters(s2);
            for(int i = 0; i < length(s2); i++)
            {
                if (s1[i] < s2[i])
                {
                    if (swop)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (s1[i] > s2[i])
                {
                    if (swop)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            if (swop)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        private void btnStringCompare_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(stringCompare("Cat", "Dog"), -1);
            Tester.TestEq(stringCompare("Dog", "Cat"), 1);
            Tester.TestEq(stringCompare("Six", "Sixty"), -1);
            Tester.TestEq(stringCompare("Cat", "Cat"), 0);
        }

        string toLowerLetters(string s)
        {
            string result = "";
            for(int i = 0; i < length(s); i++)
            {
                if (s[i] < 91 && s[i] > 64)
                {
                    result += Convert.ToChar(s[i] + 32)+"";
                }
                else
                {
                    result += s[i];
                }
            }
            return result;
        }


        private void btnToLower_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(toLowerLetters("Hi ThErE"), "hi there");
            Tester.TestEq(toLowerLetters("H6546534132KKKK"), "h6546534132kkkk");
            Tester.TestEq(toLowerLetters("BRAINS"), "brains");
            Tester.TestEq(toLowerLetters("!@#$@%%^"), "!@#$@%%^");
        }

        string toUpperLetters(string s)
        {
            string result = "";
            for (int i = 0; i < length(s); i++)
            {
                if (s[i] < 123 && s[i] > 96)
                {
                    result += Convert.ToChar(s[i] - 32) + "";
                }
                else
                {
                    result += s[i];
                }
            }
            return result;
        }

        private void btnToUpper_Click(object sender, RoutedEventArgs e)
        {
            Tester.TestEq(toUpperLetters("Hi ThErE"), "HI THERE");
            Tester.TestEq(toUpperLetters("h6546534132kkkk"), "H6546534132KKKK");
            Tester.TestEq(toUpperLetters("brains"), "BRAINS");
            Tester.TestEq(toUpperLetters("!@#$@%%^"), "!@#$@%%^");
        }
    }
}
