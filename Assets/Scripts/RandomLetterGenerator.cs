using System;

namespace RandomLetterGenerator
{
    public class RLgenerator
    {
        Random rand;
        string String_Set = "abcdefghijklmnopqrstuvwxyz";

        public RLgenerator(int? seed = null)
        {
            int m_seed = (!seed.HasValue) ? DateTime.Now.GetHashCode() : seed.Value;
            rand = new Random(m_seed);
        }

        public void setString_Set(string set)
        {
            String_Set = set;
        }
        public char Next()
        {
            return String_Set[rand.Next(String_Set.Length)];
        }

        public string NextPhrase(int phr_length)
        {
            string phrase = "";
            for (int i = 1; i <= phr_length; i++)
            {
                phrase += this.Next();
            }
            return phrase;
        }

        public string NextSentence(int sen_Len, int min_phr_Len, int max_phr_Len)
        {
            string sentence = "";
            for (int i = 1; i <= sen_Len; i++)
            {
                sentence += this.NextPhrase(rand.Next(min_phr_Len, max_phr_Len));
                sentence += " ";
            }
            return sentence;
        }

        public string[] GenText(int layers, int sen_Len = 5, int min_phr_Len = 2, int max_phr_Len = 7)
        {
            string[] text = new string[layers];
            for (int i = 0; i < layers; i++)
            {
                text[i] = NextSentence(sen_Len * (i + 1), min_phr_Len, max_phr_Len);
            }
            return text;
        }
    }
}